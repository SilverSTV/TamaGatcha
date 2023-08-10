using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AnimalHandler : MonoBehaviour
{
    public Animal currentAnimal;
    public UnityAction OnDead;

    private SpriteRenderer _spriteRenderer;
    private LifeStage _currentStage;
    

    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (gameObject.GetComponent<SpriteRenderer>() == null)
                Debug.Log("Animal's SpriteRenderer missing");
            return null;
        }
        set
        {
            if (value == null)
                Debug.Log("Animal's SpriteRenderer missing");
            else
            {
                _spriteRenderer = value;
            }
        }
    }

    private void Awake()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.Instance.timeManager.OnTimeTick += Change;
        GameManager.Instance.foodHandler.OnFeed += Feed;
        SetCurrentAnimal();
        currentAnimal.AnimalUpdate();
    }

    private void Update()
    {
        _currentStage = currentAnimal.AnimalTypeSoP.LifeStages[currentAnimal.StageNumberP];

        
        CheckSprite();
        if(currentAnimal != GameManager.Instance.animalContainerHandler.ContainerSo.currentAnimal)
            SetCurrentAnimal();
    }

    private void CheckChanges()
    {
        currentAnimal.CheckAge();
        currentAnimal.CheckStage();
    }
    
    private void Change()
    {
        currentAnimal.ChangeDay();
        CheckChanges();
    }

    public void ChangeAnimal(Animal changingAnimal)
    {
        currentAnimal.SleepingDateP = GameManager.Instance.timeManager.currentDay;
        currentAnimal = changingAnimal;
        currentAnimal.AnimalUpdate();
    }
    
    private void CheckSprite()
    {
        if (_spriteRenderer.sprite != _currentStage.stageSprite)
            _spriteRenderer.sprite = _currentStage.stageSprite;
    }

    public void SetCurrentAnimal()
    {
        currentAnimal = GameManager.Instance.animalContainerHandler.ContainerSo.currentAnimal;
    }

    public void Feed(FoodItem item)
    {
        currentAnimal.ChangeFood((int)item.FoodType.consumptionCount, true);
    }
}
