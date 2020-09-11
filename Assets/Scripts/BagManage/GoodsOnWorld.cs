using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoodsOnWorld : MonoBehaviour
{
    public GoodsItem thisItem;
    public Bag playerBag;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        AddNewItem();
    //    }
    //}

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    AddNewItem();
        //}
    }


    public void AddNewItem()
    {
        if (!playerBag.itemList.Contains(thisItem))
        {
            if (!BagManager.isFull())
            {
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i]==null)
                    {
                        thisItem.itemHeld = 1;
                        playerBag.itemList[i] = thisItem;
                        return;
                    }
                }
            }   
        }
        else
        {
            thisItem.itemHeld += 1;
        }
    }
}
