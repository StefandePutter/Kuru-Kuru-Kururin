using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public class SaveLoad
{
    private static SaveData _saveData = new SaveData();

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

    public static void SaveLevel(int level)
    {
        if (level > _saveData.LevelsCleared)
        {
            _saveData.LevelsCleared = level;
        }

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData));
    }

    public static void ResetData()
    {
        _saveData.LevelsCleared = 0;

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData));
    }

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