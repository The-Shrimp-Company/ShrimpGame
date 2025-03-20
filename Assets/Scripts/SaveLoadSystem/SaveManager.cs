using System.IO;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class SaveManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string directory = "/SaveGames/";
        public const string fileName = "File1";
        public const string fileNameSuffix = ".save";

        public const bool copyPathToClipboard = true;


        public static bool SaveGame(string _fileName)
        {
            string dir = Application.persistentDataPath + directory;
            string file = _fileName + fileNameSuffix;

            if (!Directory.Exists(dir))  // If the directory does not exist
                Directory.CreateDirectory(dir);  // Create this directory

            string json = JsonUtility.ToJson(CurrentSaveData, true);  // Convert the save to json format

            File.WriteAllText(dir + file, json);  // Write the save to the file

            if (copyPathToClipboard)
                GUIUtility.systemCopyBuffer = dir + file;  // Copies the path to your clipboard

            return true;  // Success
        }


        public static bool LoadGame(string _fileName)
        {
            string fullPath = Application.persistentDataPath + directory + _fileName + fileNameSuffix;
            SaveData tempData = new SaveData();
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
        }
    }
}