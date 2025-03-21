using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField] float secondsInADay;

    private float totalTime;
    public int year;
    public int day;
    public float hour;
    public int minute;

    private int prevYear;
    private int prevDay;


    public void Awake()
    {
        instance = this;
        totalTime = 365;

        year = Mathf.FloorToInt(totalTime / 365);
        day = Mathf.FloorToInt(totalTime) - 364;
        prevYear = year;
        prevDay = day;
    }


    void Update()
    {
        totalTime += Time.deltaTime / secondsInADay;

        year = Mathf.FloorToInt(totalTime / 365);
        day = Mathf.FloorToInt(totalTime) - 364;
        hour = Mathf.FloorToInt(totalTime * 24 % 24);
        minute = Mathf.FloorToInt(totalTime * 1440 % 60);

        if (year != prevYear) NewYear();
        if (day != prevDay) NewDay();

        PlayerStats.stats.totalPlaytime += Time.deltaTime;
    }

    private void NewYear()
    {
        prevYear = year;
    }


    private void NewDay()
    {
        prevDay = day;
        EconomyManager.instance.DailyUpdate();
    }


    public float GetTotalTime()
    {
        return totalTime;
    }


    public int GetShrimpAge(float birthTime)
    {
        return Mathf.FloorToInt(totalTime - birthTime);
    }


    public float CalculateBirthTimeFromAge(float age)
    {
        return totalTime - age;
    }
}
