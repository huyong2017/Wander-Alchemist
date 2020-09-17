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
    public GameObject bagCamera;
    public GameObject GBag;
    public GameObject outBag;
    private GList goodsItemList;
    private GList equationItemList;
    private GLoader prop;
    private GLoader mainEnergy;
    private GComponent chooseEquation;
    private GTextField GoodsInfo;
    private GProgressBar ActProgress;
    private GProgressBar energyprogress1;
    private GProgressBar energyprogress2;
    private GProgressBar energyprogress3;
    private float ActTime;
    private bool isAct;
    private EquationItem reactItem;


    private GButton button1;
    private GButton button2;
    private GButton button3;

    public EnergyItem energy1;
    public EnergyItem energy2;
    public EnergyItem energy3;

    private int propid;


    private void Start()
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

        button1 = Bag.GetChild("button1").asButton;
        button2 = Bag.GetChild("button2").asButton;
        button3 = Bag.GetChild("button3").asButton;

        energyprogress1 = Bag.GetChild("energyprogress1").asProgress;
        energyprogress2 = Bag.GetChild("energyprogress2").asProgress;
        energyprogress3 = Bag.GetChild("energyprogress3").asProgress;
        BagInitiate();
        GBag.SetActive(false);
        bagCamera.SetActive(false);
    }

    private void OnEnable()
    {
        Bag = GBag.GetComponent<UIPanel>().ui;
        goodsItemList = Bag.GetChild("GoodsItemList").asList;
        GoodsInfo = Bag.GetChild("Information").asTextField;
        prop = Bag.GetChild("prop").asLoader;
        mainEnergy = Bag.GetChild("mainEnergy").asLoader;
        chooseEquation = Bag.GetChild("chooseEquation").asCom;
        ActProgress = Bag.GetChild("ActProgress").asProgress;
        ActProgress.value = 0;
        equationItemList = Bag.GetChild("equationItemList").asList;

        button1 = Bag.GetChild("button1").asButton;
        button2 = Bag.GetChild("button2").asButton;
        button3 = Bag.GetChild("button3").asButton;

        energyprogress1 = Bag.GetChild("energyprogress1").asProgress;
        energyprogress2 = Bag.GetChild("energyprogress2").asProgress;
        energyprogress3 = Bag.GetChild("energyprogress3").asProgress;
        BagInitiate();
    }
    private void BagInitiate()
    {
        goodsItemList.RemoveChildrenToPool();
        equationItemList.RemoveChildrenToPool();

        energyprogress1.value = energy1.num;
        energyprogress2.value = energy2.num;
        energyprogress3.value = energy3.num;
        foreach (var item in myBag.itemList)
        {
            if (item != null)
            {
                GButton gButton = goodsItemList.AddItemFromPool().asButton;
                gButton.GetChild("number").asTextField.text = item.itemHeld.ToString();
                gButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "goods" + item.Itemid);
                gButton.GetChild("id").asTextField.text = item.Itemid.ToString();
                gButton.GetChild("title").asTextField.text = item.itemInfo;
                gButton.onClick.Add(() => { ClickItem(gButton); });
            }
        }
        for (int i = 0; i < 30-myBag.itemList.Count; i++)
        {
            GButton gButton = goodsItemList.AddItemFromPool().asButton;
            gButton.GetChild("number").asTextField.text = "";
            gButton.GetChild("icon").asLoader.url = "";
            gButton.GetChild("id").asTextField.text = "";
            gButton.GetChild("title").asTextField.text = "";
            gButton.onClick.Add(() => { ClickItem(gButton); });
        }

        foreach (var item in myBag.equationList)
        {
            if (item != null)
            {
                GComponent gComponent = equationItemList.AddItemFromPool().asCom;
                gComponent.GetChild("id").asTextField.text = item.Equationid.ToString();
                gComponent.GetChild("Equation").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "reaction"+item.Equationid);
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

        if (button1.selected)
        {
            prop.url = UIPackage.GetItemURL("NewBagPackage", "prop1");
            mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy1");
        }
        else if (button2.selected)
        {
            prop.url = UIPackage.GetItemURL("NewBagPackage", "prop2");
            mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy2");
        }
        else if (button3.selected)
        {
            prop.url = UIPackage.GetItemURL("NewBagPackage", "prop3");
            mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy3");
        }
        else
        {
            prop.url = UIPackage.GetItemURL("NewBagPackage", "prop1");
            mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy1");
            propid = 1;
            button1.selected = true;
        }

        if (myBag.propsItems[0].equation!=null)
        {
            EquationItem equation = myBag.propsItems[0].equation;
            chooseEquation.GetChild("equation").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "reaction" + equation.Equationid);
            chooseEquation.GetChild("FirstEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + equation.FirstEnergy);
            chooseEquation.GetChild("SecondEnergy").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "energy" + equation.SecondEnergy);
            chooseEquation.GetChild("id").asTextField.text = equation.Equationid.ToString();
            chooseEquation.GetChild("sign").asTextField.text = "";
            string id = equation.Equationid.ToString();
            chooseEquation.GetChild("actBtn").asButton.onTouchBegin.Add(() => { EquationActBegin(id); });
            chooseEquation.GetChild("actBtn").asButton.onTouchEnd.Add(() => { EquationActEnd(id); });
        }

        button1.onClick.Add(() => { changeProp(1); });
        button2.onClick.Add(() => { changeProp(2); });
        button3.onClick.Add(() => { changeProp(3); });
    }

    private void changeProp(int id)
    {
        switch (id)
        {
            case 1:
                prop.url = UIPackage.GetItemURL("NewBagPackage", "prop" + id);
                mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy" + id);
                propid = 1;
                button2.selected = false;
                button3.selected = false;
                break;
            case 2:
                prop.url = UIPackage.GetItemURL("NewBagPackage", "prop" + id);
                mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy" + id);
                propid = 2;
                button1.selected = false;
                button3.selected = false;
                break;
            case 3:
                prop.url = UIPackage.GetItemURL("NewBagPackage", "prop" + id);
                mainEnergy.url = UIPackage.GetItemURL("NewBagPackage", "energy" + id);
                propid = 3;
                button1.selected = false;
                button2.selected = false;
                break;
            default:
                break;
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
        foreach (var item in myBag.propsItems)
        {
            item.equation = myBag.equationList[int.Parse(id)-1];
        }
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
        if (BagManager.instance.checkGoods(reactItem.goods,reactItem.goodsnum))
        {
            isAct = true;
        }
        
        Debug.Log("11");
    }

    private void EquationActEnd(string id)
    {
        ActProgress.TweenValue(0, 0.5f);
        isAct = false;
        Debug.Log("11");
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
                BagManager.instance.Reaction(reactItem,propid);
            }
        }
    }
}

