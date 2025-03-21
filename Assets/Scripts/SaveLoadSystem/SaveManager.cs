using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace SaveLoadSystem
{
    public static class SaveManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string directory = "/SaveGames/";
        public const string fileNameSuffix = ".save";

        private static bool debugSaving = true;  // Whether the saving and loading should output extra messages
        public const bool copyPathToClipboard = true;  // Whether the path to the save file should be copied to your clipboard when the game saves
        public static bool currentlySaving = false;  // If the game is saving right now
        public static bool loadingGameFromFile = false;  // Whether the game is loading from a file or starting a new one
        public static bool gameInitialized = false;  // If the game has finished loading, whether that is from a file or a new game

        public static UnityAction OnLoadGameStart;
        public static UnityAction OnLoadGameFinish;


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

            if (debugSaving) Debug.Log("Game Saved to " + dir + file);
            
            return true;  // Success
        }


        public static void NewGame(string _fileName)
        {
            OnLoadGameStart?.Invoke();
            loadingGameFromFile = false;
            OnLoadGameFinish?.Invoke();
            gameInitialized = true;

            if (debugSaving) Debug.Log("New Game Started");
        }


        public static void LoadGame(string _fileName)
        {
            OnLoadGameStart?.Invoke();
            string fullPath = Application.persistentDataPath + directory + _fileName + fileNameSuffix;
            SaveData tempData = new SaveData();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);

                loadingGameFromFile = true;

                if (debugSaving) Debug.Log("Game Loaded from " + fullPath);
            }

            else
            {
                Debug.LogError("Save file at " + fullPath + " does not exist");

                loadingGameFromFile = false;
            }

            CurrentSaveData = tempData;
        }


        public static void DeleteSave(string _fileName)
        {
            string fullPath = Application.persistentDataPath + directory + _fileName + fileNameSuffix;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

                if (debugSaving) Debug.Log("File Deleted at " + fullPath);
            }
        }
    }
}