using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    [FormerlySerializedAs("animalTypesList")] [FormerlySerializedAs("_animalTypesList")] [SerializeField] private AnimalTypesListSO animalTypesListSo;
    private void Awake()
    {
        GameManager.Instance.animalSpawner = this;
    }

    public Animal SpawnEgg()
    {
        return new Animal(ChooseType());
    }

    private AnimalTypeSO ChooseType()
    {
        int typeNumber = Random.Range(0, animalTypesListSo.animalTypes.Length);
        return animalTypesListSo.animalTypes[typeNumber];
    }
    
}
