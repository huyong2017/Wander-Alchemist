using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class MyBag : MonoBehaviour
{
    // Start is called before the first frame update

    public Bag myBag;
    private GComponent Bag;
    private GList goodsItemList;
    private GList equationItemList;
    private GLoader prop;
    private GLoader mainEnergy;
    private GComponent chooseEquation;
    private GTextField GoodsInfo;
    private GProgressBar ActProgress;

    void Start()
    {
        Bag = GetComponent<UIPanel>().ui;
        goodsItemList = Bag.GetChild("GoodsItemList").asList;
        GoodsInfo = Bag.GetChild("Information").asTextField;
        prop = Bag.GetChild("prop").asLoader;
        mainEnergy = Bag.GetChild("mainEnergy").asLoader;
        chooseEquation = Bag.GetChild("chooseEquation").asCom;
        ActProgress = Bag.GetChild("ActProgress").asProgress;
        equationItemList = Bag.GetChild("equationItemList").asList;


        //for (int i = 0; i < goodsItemList.numItems - 10; i++)
        //{
        //    GButton button = goodsItemList.GetChildAt(i).asButton;
            
        //    button.onClick.Add(()=> { ClickItem(button); });
        //}
    }

    private void Awake()
    {
        BagInitiate();
    }
    //private void RenderListItem(int index,GObject obj)
    //{
    //    GButton button = obj.asButton;
    //    for (int i = 0; i < myBag.itemList.Count; i++)
    //    {
    //        if (myBag.itemList[i]!=null)
    //        {
    //            button.icon = myBag.itemList[i].itemImage;
    //            button.
    //            button.id = myBag.itemList[i].Itemid;
    //            button.title = index.ToString();
    //        }
    //    }  
    //}

    private void BagInitiate()
    {
        goodsItemList.RemoveChildrenToPool();
        equationItemList.RemoveChildrenToPool();

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
            else
            {
                GButton gButton = goodsItemList.AddItemFromPool().asButton;
                gButton.GetChild("number").asTextField.text = "";
            }
        }

        foreach (var item in myBag.equationList)
        {
            if (item != null)
            {
                GComponent gComponent = equationItemList.AddItemFromPool().asCom;
                gComponent.GetChild("id").asTextField.text = item.Equationid.ToString();
                gComponent.GetChild("equation").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "equation" + item.Equationid);
                gComponent.GetChild("FirstEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + item.FirstEnergy);
                gComponent.GetChild("SecondEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + item.SecondEnergy);
                gComponent.GetChild("information").asTextField.text = item.itemInfo;
                string productid = "";
                foreach (var itemid in item.productid)
                {
                    productid += productid + " ";
                }
                gComponent.GetChild("productid").asTextField.text = productid;

            }
        }
    }
    private void ClickItem(GButton button)
    {
        Debug.Log(button.GetChild("title").asTextField.text);
        GoodsInfo.text = button.GetChild("title").asTextField.text;
    }

    private void ChooseProp(int propid)
    {
        prop.url = UIPackage.GetItemURL("NewBagPackage", "prop"+propid);
        mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy" + propid);
    }

    private void UseEquation(GComponent gComponent)
    {
        chooseEquation.GetChild("equation").asLoader.url = gComponent.GetChild("equation").asLoader.url;
        chooseEquation.GetChild("FirstEnergy").asLoader.url = gComponent.GetChild("FirstEnergy").asLoader.url;
        chooseEquation.GetChild("SecondEnergy").asLoader.url = gComponent.GetChild("SecondEnergy").asLoader.url;
        chooseEquation.GetChild("id").asTextField.text = gComponent.GetChild("id").asTextField.text;
        string id = chooseEquation.GetChild("id").asTextField.text;
        chooseEquation.GetChild("actBtn").asButton.onClick.Add(() => { EquationAct(id); });
    }

    private void EquationAct(string id)
    {
        Debug.Log("11");
        //float actTime = 0;
        //actTime
        //gComponent.
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
