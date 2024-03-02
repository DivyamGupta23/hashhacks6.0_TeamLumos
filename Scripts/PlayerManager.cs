using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    public CameraManager cameraManager;
    public PlayerLocomotion playerLocomotion;
    public string playerName;
    public Button button;
    //public Transform theatreSpawn;
    //public Transform appleSpawn;
    //public Transform adsSpawn;
    //public Transform nikeSpawn;
    //public Transform cafeSpawn;
    //public Transform lobbySpawn;
    public Transform mapSpawn;
    public Transform theatreSpawn;
    public Transform roomSpawn;
    public Transform mallSpawn;
    public GameObject galleryUI;
    public GameObject aiUI;
    public Button galleryButton;
    public TestGallery galleryHandler;
    public CanvasWebViewPrefab canvasWebViewPrefab;
    public string profileUrl;
    public string geminiUrl;
    public GameObject startMovieButton;
    //public void Teleport(Transform spawn)
    //{
    //    Fades.instance.FadeIn(2, () => { transform.position = spawn.position; });

    //    Fades.instance.FadeOut(2);
    //}

    void FadeOutt()
    {
        Fades.instance.FadeOut();
    }
    public void TeleportFade( Transform spawnPos)
    {
        Fades.instance.FadeIn(0.01f);
        Teleport(spawnPos);
        Invoke("FadeOutt", 2.8f);
    }
    void Teleport(Transform spawnPos)
    {
        //yield return new WaitForSeconds(1.2f);
        transform.position = spawnPos.position;
        // Debug.Log("hello");
    }
    private void Start()
    {
        // button = GameObject.Find("tesstButton").GetComponent<Button>();
        // spawn = GameObject.Find("spawntest").transform;
        mallSpawn = GameObject.Find("mallSpawn").transform;
        mapSpawn = GameObject.Find("mapSpawn").transform;
        theatreSpawn = GameObject.Find("theatreSpawn").transform;
        roomSpawn = GameObject.Find("roomSpawn").transform;
        //adsSpawn = GameObject.Find("adsSpawn").transform;
        //nikeSpawn = GameObject.Find("nikeSpawn").transform;
        //cafeSpawn = GameObject.Find("cafeSpawn").transform;
        //lobbySpawn = GameObject.Find("lobbySpawn").transform;
        //playerName = GameObject.Find("selfChat").GetComponent<NetworkChat>().playerNameInput.text;
        // playerName = PlayerPrefs.GetString("PlayerName", "Unkown");

        PlayerPrefs.SetInt("setUpComplete", 0);
        cameraManager = FindObjectOfType<CameraManager>();
        galleryUI = GameObject.Find("galeryUI");
        playerLocomotion = GetComponent<PlayerLocomotion>();

        inputManager = GetComponent<InputManager>();

        playerLocomotion.cameraObject = cameraManager.cameraTransform;
        galleryButton = GameObject.Find("profileButton").GetComponent<Button>();
        startMovieButton = GameObject.Find("playTheatreButton");
        galleryUI = GameObject.Find("galeryUI");
        aiUI = GameObject.Find("aiChat");

        canvasWebViewPrefab = FindObjectOfType<CanvasWebViewPrefab>();
        cameraManager.inputManager = inputManager;

        cameraManager.targetTransform = transform;
        galleryUI.SetActive(false);
        aiUI.SetActive(false);
        galleryButton.gameObject.SetActive(false);
        TestGallery.instance.StartUploadName();
        galleryButton.onClick.AddListener(LoadWebView);

    }


    private void Update()
    {
        Application.targetFrameRate = 70;


        inputManager.HandleAllInputs();

    }
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();

    }

    private void LateUpdate()
    {

        cameraManager.HandleAllCameraMovement();

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Spawntrigger-theatre"))
        //{
        //    Teleport(theatreSpawn);
        //}
        //else if (other.CompareTag("Spawntrigger-apple"))
        //{
        //    Teleport(appleSpawn);
        //}
        //else if (other.CompareTag("Spawntrigger-nike"))
        //{
        //    Teleport(nikeSpawn);
        //}
        //else if (other.CompareTag("Spawntrigger-ads"))
        //{
        //    Teleport(adsSpawn);
        //}
        //else if (other.CompareTag("Spawntrigger-cafe"))
        //{
        //    Teleport(cafeSpawn);
        //}

        if (other.CompareTag("Player"))
        {
            try
            {
                //TestGallery.instance.playerName = other.GetComponent<PlayerManager>().playerName;
                string name = other.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text;
                string tempName = string.Concat(name.Where(c => !char.IsWhiteSpace(c))).ToLower();
                profileUrl = $"https://twis.in/shop/apis/snudata/{tempName}/index.php";

                galleryButton.gameObject.SetActive(true);

                Debug.Log(name + "correct");
                Debug.Log(profileUrl);

            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                Debug.Log(galleryButton + "not working");

            }
        }
        else if (other.CompareTag("enterMall"))
        {
            TeleportFade(mallSpawn);
        }      
        else if (other.CompareTag("exitMall"))
        {
            TeleportFade(mapSpawn);
        }         
        else if (other.CompareTag("theatreSpawn"))
        {
            TeleportFade(theatreSpawn);
        }          
        else if (other.CompareTag("roomSpawn"))
        {
            TeleportFade(roomSpawn);
        }          
        else if (other.CompareTag("screen"))
        {
            startMovieButton.SetActive(true);
        }  
        else if (other.CompareTag("robot"))
        {
            aiUI.SetActive(true);
        }        
        else if (other.CompareTag("gemini"))
        {
            LoadWebViewGemini();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            galleryButton.gameObject.SetActive(false);
            startMovieButton.SetActive(false);
            galleryUI.SetActive(false);
            aiUI.SetActive(false);
        }
        catch (System.Exception) { }
    }

    public async void LoadWebView()
    {
        galleryUI.SetActive(true);
        canvasWebViewPrefab.enabled = true; 
        await canvasWebViewPrefab.WaitUntilInitialized();
        canvasWebViewPrefab.WebView.LoadUrl(profileUrl);
    }  
    public async void LoadWebViewGemini()
    {
        galleryUI.SetActive(true);
        canvasWebViewPrefab.enabled = true; 
        await canvasWebViewPrefab.WaitUntilInitialized();
        canvasWebViewPrefab.WebView.LoadUrl(geminiUrl);
    }




}
