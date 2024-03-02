using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView; 
public class WebViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasWebViewPrefab webViewPrefab;
    public Button button;
    public string url;

    private void Start()
    {
        webViewPrefab.enabled = false;
    }
    public async void LoadView(string url)
    {
        await webViewPrefab.WaitUntilInitialized();
        webViewPrefab.WebView.LoadUrl(url);
    }    
    public async void LoadProfileView()
    {
        await webViewPrefab.WaitUntilInitialized();
        webViewPrefab.WebView.LoadUrl(TestGallery.instance.imageURL);
    }


}
