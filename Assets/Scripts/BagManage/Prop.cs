using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PropItem", menuName = "Inventory/New PropItem")]
public class Prop :ScriptableObject
{
    public string propName;
    public Sprite propImage;
    public int propid;
    public EquationItem equation;
    public int Energyid;


    [TextArea]
    public string itemInfo;
}
