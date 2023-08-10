using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AnimalContainerHandler : MonoBehaviour
{
    [FormerlySerializedAs("container")] [FormerlySerializedAs("_container")] [SerializeField]
    private AnimalContainerSO containerSo;

    private int _currentAnimalNumber;

    public AnimalContainerSO ContainerSo => containerSo;

    private void Awake()
    {
        if (ContainerSo == null)
            containerSo = FindObjectOfType<AnimalContainerSO>();
    }

    private void Start()
    {
        _currentAnimalNumber = 0;
        SetAnimalAsNumber();
        GameManager.Instance.animalHandler.OnDead += RemoveFromActiveList;
    }

    private void RemoveFromActiveList()
    {
        if (ContainerSo.animals.Count == 0) return;
        ContainerSo.animals.Remove(containerSo.currentAnimal);
        ContainerSo.inactiveAnimals.Add(containerSo.currentAnimal);
    }

    public Animal SetAnimalAsNumber()
    {
        ContainerSo.currentAnimal = ContainerSo.animals[_currentAnimalNumber];
        return ContainerSo.currentAnimal;
    }

    public Animal Next(int side)
    {
        _currentAnimalNumber += side;
        if (_currentAnimalNumber < 0)
            _currentAnimalNumber = ContainerSo.animals.Count - 1;
        if (_currentAnimalNumber >= ContainerSo.animals.Count)
            _currentAnimalNumber = 0;

        return SetAnimalAsNumber();
    }

    public void NewAnimal(Text text)
    {
        var newAnimal = GameManager.Instance.animalSpawner.SpawnEgg();
        newAnimal.SetAnimalName(text.text);
        ContainerSo.animals.Add(newAnimal);
        _currentAnimalNumber = ContainerSo.animals.Count - 1;
        SetAnimalAsNumber();
    }
}
