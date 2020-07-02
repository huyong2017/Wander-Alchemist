using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointWithCursor : MonoBehaviour
{
    public float CameraBias;//镜头偏移量大小
    private float initx;
    private float inity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.mousePosition.x - this.transform.position.x - (0.5f * Screen.width);
        float deltaY = Input.mousePosition.y - this.transform.position.y - (0.5f * Screen.height);
        Vector3 bias = new Vector3(CameraBias * deltaX, CameraBias % deltaY, 0);

        Debug.Log(this.GetComponent<CinemachineVirtualCamera>().Description);
    }
}
