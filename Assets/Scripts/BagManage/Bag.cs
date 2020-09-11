using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Bag : ScriptableObject
{
    public List<GoodsItem> itemList = new List<GoodsItem>();
    public List<EquationItem> equationList = new List<EquationItem>();
    public List<EnergyItem> energyItems = new List<EnergyItem>();
    public List<Prop> propsItems = new List<Prop>();
}
