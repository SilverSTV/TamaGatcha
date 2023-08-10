using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI daysText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private RectTransform currentFoodPanel;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private RectTransform deathGroup;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private RectTransform foodGroup;


    [FormerlySerializedAs("NamingGroup")] [SerializeField]
    private RectTransform namingGroup;


    private Animal _currentAnimal;
    private AnimalContainerHandler _animalContainerHandler;
    private FoodHandler _foodHandler;


    private void Start()
    {
        GameManager.Instance.timeManager.OnTimeTick += Tick;
        GameManager.Instance.animalHandler.OnDead += InvokeDead;
        _currentAnimal = GameManager.Instance.animalContainerHandler.ContainerSo.currentAnimal;
        _animalContainerHandler = GameManager.Instance.animalContainerHandler;
        _foodHandler = GameManager.Instance.foodHandler;
    }

    private void Update()
    {
        nameText.text = _currentAnimal.animalName;
        currentFoodPanel.localScale = new Vector3(1, _currentAnimal.FoodPercentP);
        ageText.text = _currentAnimal.AgeP.ToString();
        if (_currentAnimal != _animalContainerHandler.ContainerSo.currentAnimal)
            _currentAnimal = _animalContainerHandler.SetAnimalAsNumber();
    }

    private void Tick()
    {
        daysText.text = GameManager.Instance.timeManager.currentDay.ToString();
        stateText.text = _currentAnimal.CurrentStateP.ToString();
    }

    public void ToggleGroup(RectTransform group)
    {
        if (!_currentAnimal.IsDeadP || group.gameObject.activeInHierarchy)
        {
            group.gameObject.SetActive(!group.gameObject.activeInHierarchy);
        }
    }

    private void InvokeDead()
    {
        ToggleGroup(deathGroup);
    }

    public void ChangeAnimalButton(int side)
    {
        _animalContainerHandler.Next(side);
        GameManager.Instance.animalHandler.SetCurrentAnimal();
    }
}
