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
    private RawImage renderer;

    private void OnEnable()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        { 
            Debug.Log(devices[i].name);
        }

        
        renderer = GetComponent<RawImage>();
        renderer.transform.localScale = new Vector3(-1, 1, -1);
        webcamTexture = new WebCamTexture(WebCamTexture.devices[0].name);
        renderer.texture = webcamTexture;
    }
    
    private void Update()
    {
        if(!scanningForQR)
        {
            StartCoroutine(GetQRCode());
        }
    }

    IEnumerator GetQRCode()
    {
        Debug.LogWarning("I am now scanning");
        
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
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning("Error in QRScanner: "+ ex.Message); }
            yield return null;
        }

        yield return null;
    }

    private void OnDisable()
    {
        QrCode = string.Empty;
        if(webcamTexture != null) webcamTexture.Stop();
    }
}
