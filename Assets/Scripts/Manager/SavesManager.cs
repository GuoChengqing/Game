using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string[] GetSaveNamesWithOutPath()
    {
        string[] saveNames = GetSaveNames();
        string[] savesNamesWithOutPath = new string[saveNames.Length];

        for (int i = 0; i < saveNames.Length; i++)
        {
            savesNamesWithOutPath[i] = Path.GetFileNameWithoutExtension(saveNames[i]);
        }

        return savesNamesWithOutPath;
    }

    public static string[] GetSaveNames()
    {
        string savePath = Application.dataPath + "/Saves/";
        string[] saveFiles = Directory.GetFiles(savePath);
        List<string> saveList = new List<string>();
        
        foreach (string saveFile in saveFiles)
        {
            if (!saveFile.EndsWith(".meta"))
            {
                saveList.Add(saveFile);
            }
        }

        return saveList.ToArray();
    }

    public static void LoadSave(string saveName)
    {
        string savePath = Application.dataPath + "/Saves/" + saveName;

        ReadSave(savePath);
    }

    public static void ReadSave(string savePath)
    {
        using (var filestream = File.Open(savePath, FileMode.Open))
            using (var binarystream = new BinaryReader(filestream))
        {

        }
    }
}
