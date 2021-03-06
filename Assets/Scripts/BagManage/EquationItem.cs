﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equation", menuName = "Equation/New Equation")]

public class EquationItem : ScriptableObject
{
    public string EquationName;
    public Sprite EquationImage;
    public int Equationid;
    public List<GoodsItem> goods;
    public List<GoodsItem> product;
    public List<int> goodsnum;
    public List<int> productnum;
    public int FirstEnergy;
    public int SecondEnergy;
    public List<int> productid;

    public List<int> Energy;
    [TextArea]
    public string itemInfo;
}

