using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Savedata : Singleton<Savedata>
{
    private Dictionary<string, object> saveData = new Dictionary<string, object>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        loadAll();
    }

    private void OnApplicationQuit()
    {
        saveAll();
    }


    public void saveAll()
    {
        string filePath = Application.persistentDataPath + "/savefile.dat";
#if UNITY_EDITOR
        filePath = Application.dataPath + "/savefile.dat";
#endif

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
        bf.Serialize(file, saveData);
        file.Close();
        file.Dispose();
    }

    public void loadAll()
    {
        string filePath = Application.persistentDataPath + "/savefile.dat";
#if UNITY_EDITOR
        filePath = Application.dataPath + "/savefile.dat";
#endif
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            saveData = (Dictionary<string,object>)bf.Deserialize(file);
            file.Close();
            file.Dispose();
        } else
        {
            saveAll();
        }
    }

    public void save(string path, System.Object data)
    {
        if (data.GetType().IsSerializable)
        {
            if (saveData.ContainsKey(path))
            {
                saveData.Remove(path);
            }
            saveData.Add(path,data);
        } else
        {
            Debug.LogError("You tried to save a piece of data that is not serializeable!");
        }
    }

    public System.Object get(string path)
    {
        if (saveData.ContainsKey(path))
        {
            return saveData[path];
        }
        else
        {
            return null;
        }
    }

    public int getInt(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(int))
            {
                return (int)saveData[path];
            } else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public float getFloat(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(float))
            {
                return (float)saveData[path];
            }
            else
            {
                return 0f;
            }
        }
        else
        {
            return 0f;
        }
    }

    public string getString(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(string))
            {
                return (string)saveData[path];
            }
            else
            {
                return "null";
            }
        }
        else
        {
            return "null";
        }
    }

    public int getInt(string path, int defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(int))
            {
                return (int)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }

    public float getFloat(string path, float defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(float))
            {
                return (float)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }

    public string getString(string path, string defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(string))
            {
                return (string)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }
}
