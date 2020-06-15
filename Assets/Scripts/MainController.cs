using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal static void CalculateAttack(GameObject enemy, GameObject player)
    {
        Character _enemy = enemy.GetComponent<Character>();
        Character _player = player.GetComponent<Character>();

        while (_enemy.health > 0 && _player.health > 0)
        {
            Attack(_enemy, _player);
            Attack(_player, _enemy);
        }

        if (_player.health == 0)
        {
            Destroy(player);
            Debug.Log("Game Over");
        } else
        {
            Destroy(enemy);
            Debug.Log("Good Job");
        }

    }

    private static void Attack(Character playerA, Character playerB)
    {
        playerB.health -= (playerA.attack - playerB.armor > 0 ? 0 : playerA.attack - playerB.armor);
    }


}



