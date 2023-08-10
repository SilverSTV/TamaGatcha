using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/Animal Types List")]
public class AnimalTypesListSO : ScriptableObject
{
    [FormerlySerializedAs("AnimalTypes")] public AnimalTypeSO[] animalTypes;
}
