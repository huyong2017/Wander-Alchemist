using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGrass01 : MonoBehaviour
{
    public bool isShow;

    public float showTime;
    private float showTimer;

    public string info;

    public GameObject spark;
    public GameObject[] sparkPlace;

    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
        showTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkAndShow();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isShow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isShow = false ;
        }
    }

    private void checkAndShow()
    {
        

    }
}
