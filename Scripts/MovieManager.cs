using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coherence.Toolkit;
using Vuplex.WebView;
public class MovieManager : MonoBehaviour
{
    public bool IsPlaying = false ;
    public Button startMovieButton;
    public Button exitMovieButton;
    public GameObject theatreCam;
    public static MovieManager instance;
    public CanvasWebViewPrefab webView;
    [Header("urls")]
    public string movieUrl;
    public string staticUrl;

    private void Awake()
    {
        instance = this;
    }
    public async void StartMovie()
    {
        await webView.WaitUntilInitialized();
        webView.WebView.LoadUrl(movieUrl);
        GetComponent<CoherenceSync>().SendCommand<MovieManager>(nameof(OnPlaying),Coherence.MessageTarget.All,true);

    }    
    public async void StopMovie()
    {
        await webView.WaitUntilInitialized();
        webView.WebView.LoadUrl(staticUrl);
        GetComponent<CoherenceSync>().SendCommand<MovieManager>(nameof(OnPlaying),Coherence.MessageTarget.All,false);

    }
    public void OnPlaying(bool toggle)
    {
        IsPlaying = toggle;
    }

}
