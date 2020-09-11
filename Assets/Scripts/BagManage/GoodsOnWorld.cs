using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoodsOnWorld : MonoBehaviour
{
    public GoodsItem thisItem;
    public Bag playerBag;
    public Vector2 position;
    public int step;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        position = this.transform.position;
        step = 2;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, position.y + step), 0.01f);
        if (this.transform.position.y <= position.y-0.2)
        {
            step = 2;
        }
        if(this.transform.position.y>=position.y+0.2)
        {
            step = -2;
        }
        
    }


    public void AddNewItem()
    {
        if (!playerBag.itemList.Contains(thisItem))
        {
            //if (!BagManager.isFull())
            //{
            //    for (int i = 0; i < playerBag.itemList.Count; i++)
            //    {
            //        if (playerBag.itemList[i]==null)
            //        {
            //            thisItem.itemHeld = 1;
            //            playerBag.itemList[i] = thisItem;
            //            return;
            //        }
            //    }
            //}   
            playerBag.itemList.Add(thisItem);
            thisItem.itemHeld = 5;
        }
        else
        {
            thisItem.itemHeld += 5;
        }
    }
}
