using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character1
{
    public int maxHealth;
    public int currentLevel;

    public Player(int currentHealth, int maxHealth, int armor, int attack, int currentLevel)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.armor = armor;
        this.attack = attack;
        this.currentLevel = currentLevel;
    }
}
