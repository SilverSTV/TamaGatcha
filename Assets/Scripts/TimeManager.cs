using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    private float _timer;

    [SerializeField] private float timeToTick;
    [SerializeField] private float timeScale = 1;
    [SerializeField] private float timeScaleAFK = 10;
    [FormerlySerializedAs("enableSaveLoadTime")] [SerializeField] private bool disableSaveLoadTime;
 
    public UnityAction OnTimeTick;
    public int currentDay;

    private void Awake()
    {
        LoadTime();
    }

    private void Start()
    {
        Tick();
    }

    private void Update()
    {
        _timer += Time.deltaTime * timeScale;
        if (_timer >= timeToTick)
        {
            Tick();
        }
    }

    private void Tick()
    {
        OnTimeTick.Invoke();
        currentDay++;
        _timer = 0;
    }

    private void OnApplicationQuit()
    {
        string closeDate = DateTime.Now.ToString("g");
        PlayerPrefs.SetString("inputDate", closeDate);
        PlayerPrefs.SetString("currentDay", currentDay.ToString());
    }

    private void LoadTime()
    {
        if(disableSaveLoadTime)
            return;
        string inputDate = PlayerPrefs.GetString("inputDate");;
        currentDay = Convert.ToInt32(PlayerPrefs.GetString("currentDay", "0"));
        DateTime date = Convert.ToDateTime(inputDate);
        var dateNow = DateTime.Now;
        var differenceInSeconds  = (dateNow - date);
        currentDay += (int)(differenceInSeconds.TotalSeconds / timeScaleAFK);
    }
}
