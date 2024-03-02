using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinnedMeshRenderer _renderer;
    public Image colorToPick;
    [SerializeField] Material _material;
    public Color _color;
    public TMPro.TMP_Text _text;
    private void Awake()
    {
        _material = Resources.Load<Material>("playerMaterial");
    }
    public void ChangeColor()
    {
       _color = colorToPick.color;
       _material.color = _color;
       _text.color = _color;
    }
}
