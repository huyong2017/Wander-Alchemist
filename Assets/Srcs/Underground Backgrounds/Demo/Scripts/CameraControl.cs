using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float speed = 9;
    public float maxY = 40;

    public ScrollingLayerGroup[] levels;

	
    void Update () {
        float y = transform.position.y;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            y += Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            y -= Time.deltaTime * speed;
        }

        if (y > 0 && y < maxY)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateGroup(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateGroup(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateGroup(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateGroup(3);
        }
    }

    void ActivateGroup(int n)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(i == n);
        }
    }
}
