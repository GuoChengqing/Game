using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavesEditWindowController : MonoBehaviour
{
    //public Character Character;

    public int currentHealth;
    public int maxHealth;
    public int armor;
    public int attack;
    public int currentLevel;

    public InputField inputField;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateSavesList();

        currentHealth = 80;
        maxHealth = 100;
        armor = 5;
        attack = 10;
        currentLevel = 1;

        inputField = transform.Find("SaveFileName").GetComponent<InputField>();
        text = transform.Find("SaveFileName").Find("Text").GetComponent<Text>();

        inputField.SetTextWithoutNotify("hallo Kitty");
        //text.text = "Hallo Kitty";
    }

    //private void GenerateSavesList()
    //{
    //    string[] savespathArray = new SaveManager().GetAllSavesPath();

    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        using (var filestream = File.Create(Application.dataPath + "/Saves/" + text.text + ".bin"))
            using (var binarystream = new BinaryWriter(filestream))
        {
            binarystream.Write(currentHealth);
            binarystream.Write(maxHealth);
            binarystream.Write(armor);
            binarystream.Write(attack);
            binarystream.Write(currentLevel);
        }
    }
}
