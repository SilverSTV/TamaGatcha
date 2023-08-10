using System;
using UnityEngine;
using UnityEngine.Serialization;


public enum AnimalState
{
    Fine,
    Hungry,
    Sick,
    Dead
}

public enum AnimalLifeStage
{
    Egg,
    Baby,
    Junior,
    Teen,
    Young,
    Adult,
    Old,
    Ancient
}

[Serializable]
public class Animal
{
    #region Fields

    private const float FOOD_CONSUMPTION = 0.2f;

    [FormerlySerializedAs("foodPercentP")] [FormerlySerializedAs("_foodPercentP")] [SerializeField]
    private float foodPercent;

    [FormerlySerializedAs("ageP")] [FormerlySerializedAs("_ageP")] [SerializeField]
    private int age;

    [FormerlySerializedAs("daysOfLifeP")] [FormerlySerializedAs("_daysOfLifeP")] [SerializeField]
    private int daysOfLife;

    [FormerlySerializedAs("sleepingDateP")] [FormerlySerializedAs("_sleepingDateP")] [SerializeField]
    private int sleepingDate;

    [FormerlySerializedAs("_spawnDate")] [FormerlySerializedAs("_spawnDateP")] [SerializeField]
    private int spawnDate;

    [FormerlySerializedAs("animalType")] [FormerlySerializedAs("_animalType")] [SerializeField]
    private AnimalTypeSO animalTypeSo;

    //[SerializeField] private bool isMale;
    [SerializeField] protected float foodConsumptionP;

    [SerializeField] private int stageNumber;

    [SerializeField] private bool isDead;


    public string animalName;
    [FormerlySerializedAs("foodCount")] public float foodAmount;
    public float healthCount;
    public float happiness;

    #endregion

    #region Properties

    public int StageNumberP
    {
        get => stageNumber > AnimalTypeSoP.LifeStages.Count - 1 ? AnimalTypeSoP.LifeStages.Count - 1 : stageNumber;
        set => stageNumber = stageNumber > AnimalTypeSoP.LifeStages.Count - 1 ? AnimalTypeSoP.LifeStages.Count - 1 : value;
    }

    public float FoodPercentP => foodPercent;

    public int AgeP => age;

    public int SleepingDateP
    {
        set => sleepingDate = value;
    }

    public AnimalTypeSO AnimalTypeSoP => animalTypeSo;

    public AnimalState CurrentStateP { get; private set; }

    public bool IsDeadP => isDead;

    #endregion

    public Animal(AnimalTypeSO animalTypeSo)
    {
        this.animalTypeSo = animalTypeSo;
        spawnDate = GameManager.Instance.timeManager.currentDay;
        foodAmount = animalTypeSo.maxFoodAmount;
    }

    public virtual void ChangeDay()
    {
        if(isDead) return;
        Grow();
        ChangeFood(FOOD_CONSUMPTION, false);
        ChangeHealthState();
    }

//need constant -1 bearing in mind that has Egg Stage;
    public virtual void Grow()
    {
        daysOfLife++;
        if (daysOfLife % 100 == 0)
        {
            age = AgeP + 1;

            if (AgeP >= AnimalTypeSoP.LifeStages[stageNumber].timeToChange)
            {
                StageNumberP++;
            }
        }
    }

    public void ChangeFood(float foodConsumeCount, bool isEating)
    {
        var foodCoefficient = AnimalTypeSoP.LifeStages[StageNumberP].foodCoef;
        if (isEating)
        {
            var food = foodAmount + foodConsumeCount;
            foodAmount = food > AnimalTypeSoP.maxFoodAmount ? AnimalTypeSoP.maxFoodAmount : food;
        }
        else
        {
            var food = foodAmount - foodConsumeCount * foodCoefficient;
            foodAmount = food < 0 ? 0 : food;
        }

        var animalTypeMaxFoodCount = foodAmount / AnimalTypeSoP.maxFoodAmount;
        foodPercent = animalTypeMaxFoodCount < 1 ? animalTypeMaxFoodCount : 1f;
    }

    private void ChangeHealthState()
    {
        if (foodPercent > 0.7f)
            CurrentStateP = AnimalState.Fine;
        else if (foodPercent > 0.3f)
            CurrentStateP = AnimalState.Hungry;
        else if (foodPercent > 0)
            CurrentStateP = AnimalState.Sick;
        else
        {
            CurrentStateP = AnimalState.Dead;
            GameManager.Instance.animalHandler.OnDead.Invoke();
            isDead = true;
        }
    }

    public void AnimalUpdate()
    {
        var timeManagerCurrentDay = GameManager.Instance.timeManager.currentDay;
        var difference = timeManagerCurrentDay - sleepingDate;
        if (difference == 0)
            return;
        for (int i = 0; i < difference; i++)
        {
            ChangeDay();
        }
    }

    public void CheckAge()
    {
        age = daysOfLife / 100;
    }

    public void CheckStage()
    {
        int stageNum = 0;

        for (int i = 0; i < AnimalTypeSoP.LifeStages.Count; i++)
        {
            if (AgeP >= AnimalTypeSoP.LifeStages[i].timeToChange)
            {
                stageNum = i;
            }
            else
            {
                stageNum = i - 1;
                break;
            }
        }

        StageNumberP = stageNum < 0 ? 0 : stageNum;
    }

    public void SetAnimalName(string name)
    {
        animalName = name;
    }
    
}
