using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTest : MonoBehaviour
{
    public float autosaveTime = 3.0f;
    private float autosaveTimer = 0f;

    void Start()
    {
        LoadGame();
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
        SaveManager.Save("Autosave");
    }

    public void LoadGame()
    {
        //SaveManager.Save("Autosave");
    }
}
