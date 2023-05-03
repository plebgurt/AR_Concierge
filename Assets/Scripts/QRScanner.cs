using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QRScanner : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private WebCamTexture webCamTextureBackGround;
    private string QrCode = string.Empty;
    private bool scanningForQR;
    private RawImage scannerArea;
    [SerializeField] private RawImage backGround;
    private Texture2D scanZoneTexture2D;


    private void OnEnable()
    {
        //scannerArea = GetComponent<RawImage>();
        //scannerArea.transform.localScale = new Vector3(-1, 1, -1);
        backGround.transform.localScale = new Vector3(-1, 1, -1);
        webCamTextureBackGround = new WebCamTexture(WebCamTexture.devices[1].name, 2560, 1600);
        backGround.texture = webCamTextureBackGround;
    }

    private void Update()
    {
        if (!scanningForQR)
        {
            StartCoroutine(GetQRCode());
        }
    }

    IEnumerator GetQRCode()
    {
        scanningForQR = true;
        IBarcodeReader barCodeReader = new BarcodeReader();
        webCamTextureBackGround.Play();
        var camwidth = webCamTextureBackGround.width;
        var camheight = webCamTextureBackGround.height;
        

        while (string.IsNullOrEmpty(QrCode))
        {
            if (scanZoneTexture2D != null)
            {
                Destroy(scanZoneTexture2D);
                scanZoneTexture2D = null;
            }
            
            scanZoneTexture2D = new Texture2D(512, 512);
            Color[] pix = webCamTextureBackGround.GetPixels(camwidth / 2 - scanZoneTexture2D.width / 2, camheight / 2 - scanZoneTexture2D.height / 2, scanZoneTexture2D.width, scanZoneTexture2D.height);
            scanZoneTexture2D.SetPixels(pix);
            scanZoneTexture2D.Apply();
            //scannerArea.texture = croppedText;
            var snap = new Texture2D(scanZoneTexture2D.width, scanZoneTexture2D.height, TextureFormat.ARGB32, false);
            try
            {
                snap.SetPixels32(scanZoneTexture2D.GetPixels32());
              
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), scanZoneTexture2D.width, scanZoneTexture2D.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        if (ProgramController.instance.AttemptLogin(QrCode))
                        {
                            scanningForQR = false;
                            break;
                        }
                    }
                }
                Destroy(snap);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error in QRScanner: " + ex.Message);
            }

            yield return null;
        }

        yield return null;
    }

    private void OnDisable()
    {
        QrCode = string.Empty;
        if (scanZoneTexture2D != null)
        {
            Destroy(scanZoneTexture2D);
            scanZoneTexture2D = null;
        }
        if (webcamTexture != null) webcamTexture.Stop();
        if (webCamTextureBackGround != null) webCamTextureBackGround.Stop();
    }
}
