using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    public int targetFrameRate = 60;

    void Start()
    {
        
    }

    void Update()
    {
        if (targetFrameRate != Application.targetFrameRate) Application.targetFrameRate = targetFrameRate;
    }
}
