using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTest : MonoBehaviour
{
    public float autosaveTime = 15.0f;
    private float autosaveTimer = 0f;

    void Start()
    {
        LoadGame("Autosave");
    }


    private void OnApplicationPause()  // Autosave when game is suspended
    {
        SaveGame("Autosave");
    }


    private void OnApplicationQuit()  // Autosave when game is quit
    {
        SaveGame("Autosave");
    }


    void Update()
    {
        autosaveTimer += Time.deltaTime;
        if ( autosaveTimer > autosaveTime )  // Autosave after a certain amount of time
        {
            autosaveTimer = 0;
            SaveGame("Autosave");
        }

        Debug.Log(GameSettings.settings.cameraSensitivity);
    }

    public void SaveGame(string _fileName)
    {
        SaveManager.currentlySaving = true;
        CopyDataToSaveData();
        SaveManager.SaveGame(_fileName);
        SaveManager.currentlySaving = false;
    }

    public void LoadGame(string _fileName)
    {
        SaveManager.LoadGame(_fileName);
        
        if (SaveManager.loadingGameFromFile)
            CopyDataFromSaveData(SaveManager.CurrentSaveData);

        SaveManager.OnLoadGameFinish?.Invoke();
    }

    private void CopyDataToSaveData()
    {
        SaveData d = new SaveData();


        if (ShrimpManager.instance.allShrimp.Count != 0)
        {
            List<ShrimpStats> stats = new List<ShrimpStats>();
            foreach(Shrimp s in ShrimpManager.instance.allShrimp)
            {
                stats.Add(s.stats);
            }
            d.stats = stats.ToArray();
        }

        d.money = Money.instance.money;

        Transform player = GameObject.Find("Player").transform;
        d.playerPosition = player.position;
        d.playerRotation = player.rotation;

        d.playerStats = PlayerStats.stats;
        d.gameSettings = GameSettings.settings;



        SaveManager.CurrentSaveData = d;
    }

    private void CopyDataFromSaveData(SaveData d)
    {
        Money.instance.AddMoney(d.money - Money.instance.money);

        Transform player = GameObject.Find("Player").transform;
        player.position = d.playerPosition;
        player.rotation = d.playerRotation;
        Debug.Log(d.playerPosition);
        Debug.Log(d.playerStats.totalMoney);
        Debug.Log(d.gameSettings.cameraSensitivity);
        PlayerStats.stats = d.playerStats;
        GameSettings.settings = d.gameSettings;
    }
}
