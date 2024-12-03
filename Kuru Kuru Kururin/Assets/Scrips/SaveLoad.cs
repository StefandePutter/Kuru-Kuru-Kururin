using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public class SaveLoad
{
    // static makes sure there is only 1 _saveData that everything can talk to
    private static SaveData _saveData = new SaveData();

    // this tells the system the struct can be stored and reconstruct later
    [System.Serializable]
    public struct SaveData
    {
        public int LevelsCleared;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.dataPath + "/save" + ".save";
        return saveFile;
    }

    // save data to json
    public static void SaveLevel(int level)
    {
        // only change _saveData if the level cleared is higher then the already beaten level
        if (level > _saveData.LevelsCleared)
        {
            _saveData.LevelsCleared = level;
        }

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData));
    }

    // Reset Data back to 0
    public static void ResetData()
    {
        _saveData.LevelsCleared = 0;

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData));
    }

    // returns int of levels beaten
    public static int LoadLevel()
    {
        if (!File.Exists(SaveFileName()))
        {
            return 0;
        }

        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        return _saveData.LevelsCleared;
    }
}