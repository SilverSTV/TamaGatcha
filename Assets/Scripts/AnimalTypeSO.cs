using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class LifeStage
{
    public AnimalLifeStage stage;
    public float timeToChange;
    public float foodCoef = 1f;
    public Sprite stageSprite;
}

[CreateAssetMenu(menuName = "Data/Animal Type")]
public class AnimalTypeSO : ScriptableObject
{
    [SerializeField] private List<LifeStage> lifeStages;
    
    public string typeName;
    public Sprite typeIcon;
    public int maxAge;
    [FormerlySerializedAs("maxFoodCount")] public float maxFoodAmount;
    public FoodCategory foodCategory;
    public float maxHealthCount;
    public float maxHappinessCount;
    
    public List<LifeStage> LifeStages
    {
        get => lifeStages;
        set => lifeStages = value;
    }
    
}
/*
 Содержит информацию о параметрах каждой характеристики для каждого типа питомца.
 Характеристики:
 1.Наименовательные
 - Название типа
 - Иконка типа
 2.Возрастные
 - Максимальный возраст
 - Список стадий роста
 3.Количественные
 - Количество еды
 - Количество ХП
 - Количество счастья
 */
