using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointWithCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //根据鼠标的位置进行视角的偏移
        this.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
}
