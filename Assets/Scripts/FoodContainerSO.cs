using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[Serializable]
public class FoodItem
{
    [SerializeField] private FoodTypeSO foodType;
    [SerializeField] private int amount;

    
    public FoodTypeSO FoodType => foodType;

    public int Amount => amount;

    public FoodItem(FoodTypeSO foodType, int amount)
    {
        this.foodType = foodType;
        this.amount = amount;
    }

    public void ResetItem()
    {
        foodType = null;
        amount = 0;
    }

    public void AddItemCount(int count)
    {
        amount += count;
    }
}

[Serializable]
[CreateAssetMenu(fileName = "New food container",menuName = "Data/Food/Food container")]
public class FoodContainerSO : ScriptableObject
{
    public List<FoodItem> container;
}
