using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLogo : MonoBehaviour
{
    public RectTransform rt;
    public float speed = 100;
    void Update()
    {
        Vector3 angles = rt.eulerAngles;
        angles.z = angles.z -  speed * Time.deltaTime; // + rotationSpeed for right button
        rt.eulerAngles = angles;
    }
}
