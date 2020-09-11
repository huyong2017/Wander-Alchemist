using System;
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

    public void Reaction(EquationItem item)
    {
        List<int> Energy = item.Energy;
        List<GoodsItem> goods = item.goods;
        List<GoodsItem> products = item.product;
        List<int> goodsnum = item.goodsnum;
        List<int> productnum = item.productnum;
        Debug.Log(Energy[0] + Energy[1]);
    }

    public Boolean checkGoods(List<GoodsItem> goods, List<int> goodsnum)
    {
        for (int i = 0; i < goods.Count; i++)
        {
            if (goods[i].itemHeld<goodsnum[i])
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
