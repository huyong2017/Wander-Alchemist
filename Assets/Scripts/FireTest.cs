using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    public GameObject CampFire;
    private float time;
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "campFire")
        {
            time += Time.deltaTime;
            if (time > 3.0f)
            {
                CampFire.SetActive(false);
            }
        }
    }
}
