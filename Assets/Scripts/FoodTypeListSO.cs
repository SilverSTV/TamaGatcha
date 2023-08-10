using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New food types list", menuName = "Data/Food/Food types list")]
public class FoodTypeListSO : ScriptableObject
{
    public List<FoodTypeSO> foodTypesList;
}
