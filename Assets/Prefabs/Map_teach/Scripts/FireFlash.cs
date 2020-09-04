using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireFlash : MonoBehaviour
{
    Light2D mlight;
    void Start()
    {
        mlight = this.GetComponent<Light2D>();
    }

    float weight;
    void Update()
    {
        weight =  0.2f * Mathf.Sin(10 * Time.time) + 1;
        mlight.intensity = weight;
    }
}
