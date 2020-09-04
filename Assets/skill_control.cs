using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_control : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Prefabs;
    
    void Start()
    {
        Prefabs.GetComponent<ParticleSystem>().Stop();
    }


    // Update is called once per frame
    void Update()
    {
        Prefabs.GetComponent<ParticleSystem>().Stop();
        if (Input.GetKey(KeyCode.F))
        {
            Prefabs.GetComponent<ParticleSystem>().Play();
        }
    }
}
