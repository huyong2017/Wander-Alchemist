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
    private GComponent Bag;
    private GList goodsItemList;
    private GTextField GoodsInfo;



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
        Bag = GBag.GetComponent<UIPanel>().ui;
        goodsItemList = Bag.GetChild("GoodsItemList").asList;
        GoodsInfo = Bag.GetChild("Information").asTextField;
    }

    public void Reaction(EquationItem item)
    {
        List<int> Energy = item.Energy;
        List<GoodsItem> goods = item.goods;
        List<GoodsItem> products = item.product;
        List<int> goodsnum = item.goodsnum;
        List<int> productnum = item.productnum;
        if (checkGoods(goods, goodsnum))
        {
            for (int i = 0; i < goods.Count; i++)
            {
                goods[i].itemHeld -= goodsnum[i];
                if (goods[i].itemHeld==0)
                {
                    myBag.itemList.Remove(goods[i]);
                }
            }
            for (int i = 0; i < products.Count; i++)
            {
                if (myBag.itemList.Contains(products[i]))
                {
                    products[i].itemHeld += productnum[i];
                }
                else
                {
                    myBag.itemList.Add(products[i]);
                    products[i].itemHeld = productnum[i];
                }
            }
        }
        UpdateBag();
        //Debug.Log(Energy[0] + Energy[1]);
    }

    public bool checkGoods(List<GoodsItem> goods, List<int> goodsnum)
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

    public void UpdateBag()
    {
        goodsItemList.RemoveChildrenToPool();

        foreach (var item in myBag.itemList)
        {
            if (item != null)
            {
                GButton gButton = goodsItemList.AddItemFromPool().asButton;
                gButton.GetChild("number").asTextField.text = item.itemHeld.ToString();
                gButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "Goods" + item.Itemid);
                gButton.GetChild("id").asTextField.text = item.Itemid.ToString();
                gButton.GetChild("title").asTextField.text = item.itemInfo;
                gButton.onClick.Add(() => { ClickItem(gButton); });
            }
        }
        for (int i = 0; i < 30 - myBag.itemList.Count; i++)
        {
            GButton gButton = goodsItemList.AddItemFromPool().asButton;
            gButton.GetChild("number").asTextField.text = "";
        }
    }

    private void ClickItem(GButton button)
    {
        Debug.Log(button.GetChild("title").asTextField.text);
        GoodsInfo.text = button.GetChild("title").asTextField.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
