using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
public class LoadMaterial : MonoBehaviour
{
    public Material _mat;
    public SkinnedMeshRenderer _meshRenderer;
    [OnValueSynced(nameof(OnColorChanged))] public Color color;
    public CoherenceSync _sync;
    private void Start()
    {
        try
        {
            ChangeColor();
            _sync.SendCommand<LoadMaterial>(nameof(ChangeColor), Coherence.MessageTarget.Other);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load the material:"+ e);
        }
    }

    [Command]
    public void ChangeColor()
    {
        _mat = Resources.Load<Material>("playerMaterial");
        color = _mat.color;
        _meshRenderer.materials[2].color = _mat.color;
    }
    public void OnColorChanged(Color color) 
    {
        _meshRenderer.materials[2].color = color;
    }
}
