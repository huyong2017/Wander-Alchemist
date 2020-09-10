using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatingSpeed = 0.01f;
    public float floatFreq = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, floatingSpeed * Mathf.Sin(Time.time * floatFreq), 0));
    }
}
