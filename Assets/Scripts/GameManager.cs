using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    public TimeManager timeManager;
    public AnimalContainerHandler animalContainerHandler;
    public AnimalHandler animalHandler;
    public AnimalSpawner animalSpawner;
    public InterfaceManager interfaceManager;
    public FoodHandler foodHandler;
    private void Awake()
    {
        Instance = this;
    }

    
}
