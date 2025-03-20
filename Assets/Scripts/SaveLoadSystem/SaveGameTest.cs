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
        LoadGame();
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }


    void Update()
    {
        autosaveTimer += Time.deltaTime;
        if ( autosaveTimer > autosaveTime )
        {
            autosaveTimer = 0;
            SaveGame();
        }
    }

    public void SaveGame()
    {
        CopyDataToSaveData();
        SaveManager.SaveGame("Autosave");
    }

    public void LoadGame()
    {
        SaveManager.LoadGame("Autosave");
        
        if (SaveManager.loadingGameFromFile)
            CopyDataFromSaveData(SaveManager.CurrentSaveData);
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


        SaveManager.CurrentSaveData = d;
    }

    private void CopyDataFromSaveData(SaveData d)
    {
        Money.instance.AddMoney(d.money - Money.instance.money);

        Transform player = GameObject.Find("Player").transform;
        player.position = d.playerPosition;
        player.rotation = d.playerRotation;
    }
}
