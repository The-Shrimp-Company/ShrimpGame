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


    public void Awake()
    {
        instance = this;
    }


    void Update()
    {
        totalTime += Time.deltaTime / secondsInADay;

        year = 1 + Mathf.FloorToInt(totalTime / 365.25f);
        day = 1 + Mathf.FloorToInt(totalTime);
        hour = Mathf.FloorToInt(totalTime * 24 % 24);
        minute = Mathf.FloorToInt(totalTime * 1440 % 60);
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
