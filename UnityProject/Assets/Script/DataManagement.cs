using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    public delegate void DataEventHandler(Data data);
    public static event DataEventHandler DataLoaded;
    public static void FireEvent(DataEventHandler _event, Data data)
    {
        if (_event != null)
            _event(data);
    }

    private static Data _data { get; set; }

    private static BinaryFormatter bf = new BinaryFormatter();
    private static FileStream file;

    private static string path = Application.persistentDataPath + "/Data.dat";

    public static void Save()
    {
        if (File.Exists(path))
            file = File.Open(path, FileMode.Open);
        else
            file = File.Create(path);

        PopulateData();

        bf.Serialize(file, _data);

        file.Close();
    }

    public static void Load()
    {
        if (!File.Exists(path))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        file = File.Open(path, FileMode.Open);

        _data = (Data)bf.Deserialize(file);

        file.Close();

        FireEvent(DataLoaded, _data);
    }

    private static void PopulateData()
    {
        _data = new Data(ScoreSystem.HighScore);
    }
}

[Serializable]
public class Data
{
    public int highscore { get; private set; }

    public Data(int highscore)
    {
        this.highscore = highscore;
    }
}
