using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    #region Save
    public static void SaveByJson(string saveFileName,object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath,saveFileName);

        try
        {
            File.WriteAllText(path, json);

            #if UNITY_EDITOR
            Debug.Log($"Sussessfully saved data to {path}.");
            #endif
        }
        catch(System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.LogError($"Faild to save data to {path}.\n{exception}");
            #endif
        }

    }
    #endregion
    #region Load
    public static T LoadFromJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);

            return data;
        }
        catch (System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.LogError($"Faild to load data to {path}.\n{exception}");
            #endif

            return default;
        }
    }
    #endregion
    #region Delete
    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.LogError($"Faild to delete to {path}.\n{exception}");
            #endif
        }
    }
    #endregion
}
