using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string[] saveNameList;
    public Player player;

    // Start is called before the first frame update
    void Awake()
    {
        saveNameList = GetAllSavesPath();

        using (var filestream = File.Open(saveNameList[0], FileMode.Open))
        using (var binarystream = new BinaryReader(filestream))
        {
            player.currentHealth = binarystream.ReadInt32();
            player.maxHealth = binarystream.ReadInt32();
            player.armor = binarystream.ReadInt32();
            player.attack = binarystream.ReadInt32();
            player.currentLevel = binarystream.ReadInt32();
        }
    }

    public string[] GetAllSavesPath()
    {
        return Directory.GetFiles(Application.dataPath + "/Saves/");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
