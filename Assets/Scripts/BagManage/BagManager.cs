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
    public GameObject bagCamera;
    public GameObject outBag;
    public GComponent mainUI;
    private GComponent Bag;
    private GList goodsItemList;
    private GTextField GoodsInfo;
    private GProgressBar energyprogress1;
    private GProgressBar energyprogress2;
    private GProgressBar energyprogress3;
    public EnergyItem energy1;
    public EnergyItem energy2;
    public EnergyItem energy3;



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
        energyprogress1 = Bag.GetChild("energyprogress1").asProgress;
        energyprogress2 = Bag.GetChild("energyprogress2").asProgress;
        energyprogress3 = Bag.GetChild("energyprogress3").asProgress;
        energy1.num = 0;
        energy2.num = 0;
        energy3.num = 0;
        UIManager.instance.setPower(0, energy1.num);
        UIManager.instance.setPower(2, energy2.num);
        UIManager.instance.setPower(3, energy3.num);
        
    }

    public void Reaction(EquationItem item,int propid)
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

            for (int i = 0; i < Energy.Count; i++)
            {
                if (Energy[i] == propid)
                {
                    if (i == 0)
                    {
                        addEnergy(propid, 10);

                    }
                    if (i == 1)
                    {
                        addEnergy(propid, 5);
                    }
                }
            }
        }  
        UpdateBag();
        //Debug.Log(Energy[0] + Energy[1]);
    }

    private void addEnergy(int propid,int num)
    {
        switch (propid)
        {
            case 1:
                energy1.num += num;      
                if (energy1.num>=100)
                {
                    energy1.num = 100;
                }
                UIManager.instance.setPower(0, energy1.num / 100);
                break;
            case 2:
                energy2.num += num;
                if (energy2.num >= 100)
                {
                    energy2.num = 100;
                }
                UIManager.instance.setPower(2, energy2.num / 100);
                break;
            case 3:
                energy3.num += num;
                if (energy3.num >= 100)
                {
                    energy3.num = 100;
                }
                UIManager.instance.setPower(3, energy3.num / 100);
                break;
            default:
                break;
        }
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
                gButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("NewBagPackage", "goods" + item.Itemid);
                gButton.GetChild("id").asTextField.text = item.Itemid.ToString();
                gButton.GetChild("title").asTextField.text = item.itemInfo;
                gButton.onClick.Add(() => { ClickItem(gButton); });
            }
        }
        for (int i = 0; i < 30 - myBag.itemList.Count; i++)
        {
            GButton gButton = goodsItemList.AddItemFromPool().asButton;
            gButton.GetChild("number").asTextField.text = "";
            gButton.GetChild("icon").asLoader.url = "";
            gButton.GetChild("id").asTextField.text = "";
            gButton.GetChild("title").asTextField.text = "";
            gButton.onClick.Add(() => { ClickItem(gButton); });
        }
        energyprogress1.TweenValue(energy1.num, 0.1f);
        energyprogress2.TweenValue(energy2.num, 0.1f);
        energyprogress3.TweenValue(energy3.num, 0.1f);
    }

    private void ClickItem(GButton button)
    {
        Debug.Log(button.GetChild("title").asTextField.text);
        GoodsInfo.text = button.GetChild("title").asTextField.text;
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (GBag.active)
            {
                GBag.SetActive(false);
                bagCamera.SetActive(false);
                outBag.SetActive(true);
                UIManager.instance.setPower(0, energy1.num);
                UIManager.instance.setPower(2, energy2.num);
                UIManager.instance.setPower(3, energy3.num);
            }
            else
            {
                GBag.SetActive(true);
                bagCamera.SetActive(true);               
                outBag.SetActive(false);

            }
        }
    }
}
