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
    //private void Start()
    //{
      
    //    startMovieButton.onClick.AddListener(StartMovie);
    //    exitMovieButton.onClick.AddListener(StopMovie);
    //}
    public async void StartMovie()
    {
        theatreCam.SetActive(true);
        await webView.WaitUntilInitialized();
        webView.WebView.LoadUrl(movieUrl);

    }    
    public async void StopMovie()
    {
        theatreCam.SetActive(false);
        await webView.WaitUntilInitialized();
        webView.WebView.LoadUrl(staticUrl);


    }
    public void OnPlaying(bool toggle)
    {
        IsPlaying = toggle;
    }

}
