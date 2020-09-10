using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static BagManager instance;
    public Bag myBag;
    public GameObject GBag;
    public GComponent mainUI;



    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

    }

    public static bool isFull()
    {
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            if (instance.myBag.itemList[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
