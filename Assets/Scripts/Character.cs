using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    public int armor;
    public int attack;
    public bool _isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_isPlayer)
        {
            if (FirstPlayTheGame())
            {
                GeneratePlayerPrefs();
                Debug.Log("Init PlayerPrefs.");
            }

            health = PlayerPrefs.GetInt(Constants.Key_Of_Health);
            armor = PlayerPrefs.GetInt(Constants.Key_Of_Armor);
            attack = PlayerPrefs.GetInt(Constants.Key_Of_Attack);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void save()
    {
        PlayerPrefs.SetInt(Constants.Key_Of_Health, health);
        PlayerPrefs.SetInt(Constants.Key_Of_Armor, armor);
        PlayerPrefs.SetInt(Constants.Key_Of_Attack, attack);

        PlayerPrefs.Save();
    }

    private void GeneratePlayerPrefs()
    {
        PlayerPrefs.SetInt(Constants.Key_Of_Health, Constants.InitalHealth);
        PlayerPrefs.SetInt(Constants.Key_Of_Armor, Constants.InitalArmor);
        PlayerPrefs.SetInt(Constants.Key_Of_Attack, Constants.InitalAttack);

        PlayerPrefs.Save();
    }

    private bool FirstPlayTheGame()
    {
        return !PlayerPrefs.HasKey(Constants.Key_Of_Health);
    }
}
