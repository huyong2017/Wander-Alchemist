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
    private float ActTime;
    private Boolean isAct;
    private EquationItem reactItem;

    void Start()
    {
        

        //for (int i = 0; i < goodsItemList.numItems - 10; i++)
        //{
        //    GButton button = goodsItemList.GetChildAt(i).asButton;

        //    button.onClick.Add(()=> { ClickItem(button); });
        //}
    }

    private void Awake()
    {
        Bag = GetComponent<UIPanel>().ui;
        goodsItemList = Bag.GetChild("GoodsItemList").asList;
        GoodsInfo = Bag.GetChild("Information").asTextField;
        prop = Bag.GetChild("prop").asLoader;
        mainEnergy = Bag.GetChild("mainEnergy").asLoader;
        chooseEquation = Bag.GetChild("chooseEquation").asCom;
        ActProgress = Bag.GetChild("ActProgress").asProgress;
        ActProgress.value = 0;
        equationItemList = Bag.GetChild("equationItemList").asList;
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
                gComponent.GetChild("Equation").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "reaction1");
                gComponent.GetChild("FirstEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + item.FirstEnergy);
                gComponent.GetChild("SecondEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + item.SecondEnergy);
                gComponent.GetChild("information").asTextField.text = item.itemInfo;
                string productid = "";
                foreach (var itemid in item.productid)
                {
                    productid += productid + itemid + " ";
                }
                gComponent.GetChild("productid").asTextField.text = productid;
                gComponent.GetChild("UseBtn").asButton.onClick.Add(() => { UseEquation(gComponent); });

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
        chooseEquation.GetChild("equation").asLoader.url = gComponent.GetChild("Equation").asLoader.url;
        chooseEquation.GetChild("FirstEnergy").asLoader.url = gComponent.GetChild("FirstEnergy").asLoader.url;
        chooseEquation.GetChild("SecondEnergy").asLoader.url = gComponent.GetChild("SecondEnergy").asLoader.url;
        chooseEquation.GetChild("id").asTextField.text = gComponent.GetChild("id").asTextField.text;
        chooseEquation.GetChild("sign").asTextField.text = "";
        string id = chooseEquation.GetChild("id").asTextField.text;
        chooseEquation.GetChild("actBtn").asButton.onTouchBegin.Add(() => { EquationActBegin(id); });
        chooseEquation.GetChild("actBtn").asButton.onTouchEnd.Add(() => { EquationActEnd(id); });
    }

    private void EquationActBegin(string id)
    {
        ActTime = 0;
        foreach (var item in myBag.equationList)
        {
            if (item.Equationid ==int.Parse(id))
            {
                reactItem = item;
                break;
            }
        }
        isAct = true;
        Debug.Log("11");
        //float actTime = 0;
        //actTime
        //gComponent.
    }

    private void EquationActEnd(string id)
    {
        ActProgress.TweenValue(0, 0.5f);
        isAct = false;
        Debug.Log("11");
        //float actTime = 0;
        //actTime
        //gComponent.
    }

    IEnumerator Act()
    {
        for (int i = 1; i <= 100; i++)
        {
            ActProgress.value = i;
            if (ActProgress.value == 100)
            {
                BagManager.instance.Reaction(1);
                ActProgress.value = 0;
            }
            yield return new WaitForSeconds(500);
        }    
    }
  
    // Update is called once per frame
    void Update()
    {
        if (isAct)
        {
            ActTime = ActTime + Time.deltaTime;
            ActProgress.TweenValue(ActTime / 0.5*100, 0.05f);
            if (ActProgress.value>=100)
            {
                ActProgress.value = 0;
                ActTime = 0;
                BagManager.instance.Reaction(reactItem);
            }
        }
    }
}

