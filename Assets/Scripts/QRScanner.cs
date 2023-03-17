using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRScanner : MonoBehaviour
{ 
    private WebCamTexture webcamTexture;
    private string QrCode = string.Empty;
    private bool scanningForQR;
    
    private void OnEnable()
    {
        var renderer = GetComponent<RawImage>();
        renderer.transform.localScale = new Vector3(-1, 1, -1);
        webcamTexture = new WebCamTexture(WebCamTexture.devices[1].name);
        renderer.texture = webcamTexture;
        if(!scanningForQR) StartCoroutine(GetQRCode());
    }

    private void Update()
    {
        if(!scanningForQR)
        {
            QrCode = string.Empty;
            StartCoroutine(GetQRCode());
        }
    }

    IEnumerator GetQRCode()
    {
        scanningForQR = true;
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        if (ProgramController.instance.AttemptLogin(QrCode))
                        {
                            scanningForQR = false;
                            webcamTexture.Stop();
                            ProgramController.instance.SwitchFromQR(true);
                            break;
                        }

                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
    }
}
