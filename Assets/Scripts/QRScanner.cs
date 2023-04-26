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

    private Texture2D croppedText;
    private void OnEnable()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        { 
            Debug.Log(devices[i].name);
        }

        var rect = GetComponent<Rect>();
        scannerArea = GetComponent<RawImage>();
        scannerArea.transform.localScale = new Vector3(-1, 1, -1);
        
        
        backGround.transform.localScale = new Vector3(-1, 1, -1);
        
        
        webCamTextureBackGround = new WebCamTexture(WebCamTexture.devices[1].name);
        backGround.texture = webCamTextureBackGround;
        
       
        
        
        /*This works
        var rect = GetComponent<Rect>();
        scannerArea = GetComponent<RawImage>();
        scannerArea.transform.localScale = new Vector3(-1, 1, -1);
        webcamTexture = new WebCamTexture(WebCamTexture.devices[1].name);
        scannerArea.texture = webcamTexture;
        
        backGround.transform.localScale = new Vector3(-1, 1, -1);
        webCamTextureBackGround = webcamTexture;
        backGround.texture = webCamTextureBackGround;
         */
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
        //webcamTexture.Play();
       
        
        Debug.LogWarning($"Height: {webCamTextureBackGround.height}, Width: {webCamTextureBackGround.width}");
        
        webCamTextureBackGround.Play();
        var camwidth = webCamTextureBackGround.width;
        var camheight = webCamTextureBackGround.height;
        
        
        Color[] pix = webCamTextureBackGround.GetPixels((int) camwidth / 4, (int) camheight / 4, camwidth / 2,camheight / 2);
        croppedText = new Texture2D(camwidth / 2, camheight / 2);
        croppedText.SetPixels(pix);
        
        scannerArea.texture = croppedText;
        
        var snap = new Texture2D(croppedText.width, croppedText.height, croppedText.format, false);
        //var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        //var snap = croppedText;
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(croppedText.GetPixels32());
                
                if (snap.format != croppedText.format) {
                    Debug.LogError("Texture format mismatch.");
                    break;
                }
                
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), croppedText.width, croppedText.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                
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
        if(webCamTextureBackGround != null)webCamTextureBackGround.Stop();
    }
}
