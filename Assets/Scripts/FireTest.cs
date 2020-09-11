using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "exam")
        {
            UIManager.instance.CollisionDetection(true);
        }
    }
}
