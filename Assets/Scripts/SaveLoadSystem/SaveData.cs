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


        public ShrimpStats[] stats;
    }





    [System.Serializable]
    public class ShrimpSaveData
    {
        public int colour = 1;
        [SerializeField] private float f = 5.1f;
    }
}