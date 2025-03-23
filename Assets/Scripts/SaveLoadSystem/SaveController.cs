using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public float autosaveTime = 15.0f;
    private float autosaveTimer = 0f;
    [SerializeField] bool loadPlayerPosition = true;

    private ShelfSpawn shelfSpawn;

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

        //Debug.Log(GameSettings.settings.cameraSensitivity);
    }

    public void SaveGame(string _fileName)
    {
        if (!SaveManager.gameInitialized ||  // If the game hasn't loaded yet
            SaveManager.currentlySaving)     // If the game is already saving
            return;

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

        SaveManager.gameInitialized = true;
        SaveManager.OnLoadGameFinish?.Invoke();
    }



    private void CopyDataToSaveData()  // Save
    {
        SaveData d = new SaveData();


        //if (ShrimpManager.instance && ShrimpManager.instance.allShrimp.Count != 0)
        //{
        //    List<ShrimpStats> stats = new List<ShrimpStats>();
        //    foreach(Shrimp s in ShrimpManager.instance.allShrimp)
        //    {
        //        stats.Add(s.stats);
        //    }
        //    d.stats = stats.ToArray();
        //}



        d.money = Money.instance.money;

        Transform player = GameObject.Find("Player").transform;
        d.playerPosition = player.position;
        d.playerRotation = player.rotation;

        d.playerStats = PlayerStats.stats;
        d.gameSettings = GameSettings.settings;

        d.versionNumber = Application.version;


        if (GeneManager.instance)
        {
            d.globalGenes = GeneManager.instance.GetGlobalGeneArray();
        }


        if (shelfSpawn == null) shelfSpawn = (ShelfSpawn)FindObjectOfType(typeof(ShelfSpawn));
        if (shelfSpawn == null) Debug.LogWarning("Save Controller could not find Shelf Spawn");
        else
        {
            List<ShelfSaveData> shelfList = new List<ShelfSaveData>();
            foreach (Shelf shelf in shelfSpawn._shelves)
            {
                ShelfSaveData shelfSave = new ShelfSaveData();

                if (shelf != null && shelf.gameObject.activeSelf)
                {
                    int index = 0;
                    List<TankSocketSaveData> socketList = new List<TankSocketSaveData>();
                    foreach (TankSocket socket in shelf._tanks)
                    {
                        if (socket.tank != null && socket.tank.gameObject.activeInHierarchy)
                        {
                            TankSocketSaveData socketSave = new TankSocketSaveData();
                            TankSaveData tankSave = new TankSaveData();
                            socketSave.tank = tankSave;
                            socketSave.socketNumber = index;

                            List<ShrimpStats> shrimpInTank = new List<ShrimpStats>();
                            foreach (Shrimp s in socket.tank.shrimpInTank)
                            {
                                shrimpInTank.Add(s.stats);
                            }

                            tankSave.shrimp = shrimpInTank.ToArray();
                            tankSave.tankName = socket.tank.tankName;
                            tankSave.destinationTank = socket.tank.destinationTank;
                            tankSave.openTank = socket.tank.openTank;
                            tankSave.openTankPrice = socket.tank.openTankPrice;

                            socketList.Add(socketSave);
                        }

                        index++;
                    }

                    shelfSave.tanks = socketList.ToArray();
                }

                shelfList.Add(shelfSave);
            }
            d.shelves = shelfList.ToArray();
        }




        SaveManager.CurrentSaveData = d;
    }



    private void CopyDataFromSaveData(SaveData d)  // Load
    {
        Money.instance.SetMoney(d.money);

        Transform player = GameObject.Find("Player").transform;
        if (loadPlayerPosition) player.position = d.playerPosition;
        if (loadPlayerPosition) player.rotation = d.playerRotation;
        PlayerStats.stats = d.playerStats;
        GameSettings.settings = d.gameSettings;


        if (shelfSpawn == null) shelfSpawn = (ShelfSpawn)FindObjectOfType(typeof(ShelfSpawn));
        if (shelfSpawn == null) Debug.LogWarning("Save Controller could not find Shelf Spawn");
        else
        {

        }
    }
}
