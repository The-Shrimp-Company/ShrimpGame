using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData
    {
        public float money;
        public Vector3 playerPosition;
        public Quaternion playerRotation;


        public Stats playerStats;
        public Settings gameSettings;

        public ShelfSaveData[] shelves;

        public GlobalGene[] globalGenes;

        public string versionNumber;
        public string fileIntegrityCheck;
    }





    [System.Serializable]
    public class ShelfSaveData
    {
        public TankSocketSaveData[] tanks;
    }

    [System.Serializable]
    public class TankSocketSaveData
    {
        public int socketNumber;
        public TankTypes type;
        public TankSaveData tank = null;
    }

    [System.Serializable]
    public class TankSaveData
    {
        public ShrimpStats[] shrimp;
        public string tankName;
        public bool destinationTank;
        public bool openTank;
        public float openTankPrice;
    }
}