using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void Init()
    {
        // Sees if save folder exists
        if (!Directory.Exists(Application.persistentDataPath + "/Saves/"))
        {
            // Creates save folder
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
        }
    }

    public static void Save(string saveString, string path)
    {
        File.WriteAllText(path, saveString);
    }

    public static string Load(string path)
    {
        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
