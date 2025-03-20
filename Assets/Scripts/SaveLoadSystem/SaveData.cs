using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData
    {
        public int i = 1;
        [SerializeField] private float f = 5.1f;
        public bool b = true;
        public Vector3 v = new Vector3(0, 10, 99.9f);
    }





    [System.Serializable]
    public class ShrimpSaveData
    {
        public int i = 1;
        [SerializeField] private float f = 5.1f;
    }
}