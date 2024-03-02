using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;
    using TMPro;
public class UIManager : MonoBehaviour
{
  
    public RectTransform rectTransform;
    public float duration = 0.05f;
    public float old_X_Pos;
    public float old_Y_Pos;    

    public float new_X_Pos;
    public float new_Y_Pos;

    public void SlideOut()
    {
        rectTransform.DOAnchorPos(new Vector2(new_X_Pos, new_Y_Pos), duration, false);
    }   
    public void SlideIn()
    {
        rectTransform.DOAnchorPos(new Vector2(old_X_Pos, old_Y_Pos), duration, false);
    }


}
