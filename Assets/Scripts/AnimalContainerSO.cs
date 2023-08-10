using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/Animal Container")]
[Serializable]
public class AnimalContainerSO : ScriptableObject
{
    public List<Animal> animals;
    [FormerlySerializedAs("CurrentAnimal")] public Animal currentAnimal;

    public List<Animal> inactiveAnimals;
}
