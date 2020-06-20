using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save: MonoBehaviour
{
    Player player;
    string name;

    public Save(string name)
    {
        this.name = name;
        Load();
        
    }

    public void Store()
    {

    }

    public void Load()
    {
        using (var filestream = File.Open(Application.dataPath + "/Saves/" + name + ".bin", FileMode.Open))
            using (var binarystream = new BinaryReader(filestream))
        {
            LoadPlayerAttribute(binarystream);
        }
    }

    private void LoadPlayerAttribute(BinaryReader binarystream)
    {
    }
}
