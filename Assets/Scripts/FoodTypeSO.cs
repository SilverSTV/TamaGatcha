using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodCategory
{
    Plant,
    Meat,
    Universal
}

[CreateAssetMenu (fileName = "New food type",menuName = "Data/Food/Food Type")]
public class FoodTypeSO : ScriptableObject
{
    public FoodCategory category;
    public string typeName;
    public float consumptionCount;
    public Sprite icon;
}
