using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Fades : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public static Fades instance;

    private void Awake()
    {
        instance = this;
    }
    public void FadeIn(float sec)
    {
        fadeCanvasGroup.alpha = 0;
        fadeCanvasGroup.DOFade(1, sec);

    }    
    public void FadeOut()
    {
        fadeCanvasGroup.alpha = 1;
        fadeCanvasGroup.DOFade(0, 2);
    }
    //public void TeleportFade(Transform selfPos, Transform spawnPos)
    //{
    //    FadeIn();
    //    StartCoroutine(Teleport(selfPos, spawnPos));
    //    Invoke("FadeOut",2.8f);
    //}
    // IEnumerator Teleport(Transform selfPos, Transform spawnPos)
    //{
    //    yield return new WaitForSeconds(1.2f);
    //    selfPos.Translate(spawnPos.position);
    //   // Debug.Log("hello");
    //}
}
