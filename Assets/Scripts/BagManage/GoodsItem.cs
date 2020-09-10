using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GoodsItem", menuName = "Inventory/New GoodsItem")]
public class GoodsItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    public int Itemid;


    [TextArea]
    public string itemInfo;
}
