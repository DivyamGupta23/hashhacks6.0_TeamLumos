using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coherence.Toolkit;
public class MovieManager : MonoBehaviour
{
    [OnValueSynced(nameof(OnPlaying))]
    public bool IsPlaying = false ;
    public Button startMovieButton;
    public Button exitMovieButton;
    public GameObject theatreCam;
    public Collider theatreEntranceCollider;
    public static MovieManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void OnPlaying()
    {
        IsPlaying = true;
        theatreEntranceCollider.isTrigger = false;
    }

}
