using UnityEngine;
using System.Collections;

public class BackTiles : MonoBehaviour {

    public float speed = 10;
    public float minX;

	// Update is called once per frame
	void Update () {
        float x = transform.position.x - Time.deltaTime * speed;
        if (x < minX)
        {
            x = 0;
        }

        transform.position = new Vector3(x, Camera.main.transform.position.y * 0.75f, transform.position.z);   	
	}
}
