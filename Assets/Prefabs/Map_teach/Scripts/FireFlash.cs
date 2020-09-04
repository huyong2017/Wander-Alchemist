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
        weight =  0.5f * Mathf.Sin(10 * Time.time) + 1;
        mlight.intensity = weight;
        this.transform.Translate(new Vector3(0.05f * Mathf.Sin(Time.time),0,0));
    }
}
