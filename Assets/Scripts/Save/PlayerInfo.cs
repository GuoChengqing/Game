using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInfo : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private int armor;
    private int attack;
    private int currentLevel;

    public void Load(string file)
    {
        using (var filestream = File.Open(Application.dataPath + "/Resources/" + file, FileMode.Open))
        using (var binarystream = new BinaryReader(filestream))
        {
            currentHealth = binarystream.ReadInt32();
            maxHealth = binarystream.ReadInt32();
            armor = binarystream.ReadInt32();
            attack = binarystream.ReadInt32();
            currentLevel = binarystream.ReadInt32();
        }
    }

    public GameObject GeneratePlayer()
    {
        GameObject player = Instantiate(Resources.Load("Prefabs/player", typeof(GameObject))) as GameObject;

        return player;
    }
}
