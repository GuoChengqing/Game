using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayManager : MonoBehaviour
{
    public Map map;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickToMove();

        if (map.GotoNewGrid(player.transform.position))
        {
            Handle(player, map.GetCurrentPositionType());
        }
    }

    private void Handle(Player player, int type)
    {
        if (IsEnemy(type))
        {
            GameObject enemy = map.GetEnemy(type);
            DoAttackCalculation(player, enemy.GetComponent<Enemy>());
            map.SetCurrentPlayerPointType(TempConstant.NullType);
        }

        if (type == TempConstant.TrackType)
        {
            player.currentHealth -= 10;
        }

        if (type == TempConstant.HealPointType)
        {
            player.currentHealth += 10;
        }

        if (type == TempConstant.StartPointType)
        {
            if (player.currentLevel != 1)
            {
                map.SaveMapInfo(player.currentLevel);

                player.currentLevel -= 1;
                map.DestoryMap();
                map.GenerateMap(player.currentLevel);
                map.SetPlayerToStartPointPosition(player);
            }
        }

        if (type == TempConstant.EndPointType)
        {
            map.SaveMapInfo(player.currentLevel);

            player.currentLevel += 1;
            map.DestoryMap();

            map.GenerateMap(player.currentLevel);
            map.SetPlayerToStartPointPosition(player);
        }
    }

    private bool IsEnemy(int type)
    {
        return type >= 10;
    }

    private bool ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                player.SetDestination(hit.point);
            }

            return true;
        }

        return false;
    }

    private void DoAttackCalculation(Player player, Enemy enemy)
    {
        while (enemy.currentHealth > 0 && player.currentHealth > 0)
        {
            Attack(enemy, player);
            Attack(player, enemy);
        }

        if (player.currentHealth <= 0)
        {
            Destroy(player.gameObject);
            Debug.Log("Game Over");
        }
        if (enemy.currentHealth <= 0)
        {
            enemy.gameObject.SetActive(false);
            Debug.Log("Good Job");
        }
    }

    private void Attack(Character1 player1, Character1 player2)
    {
        player2.currentHealth -= (player1.attack - player2.armor);
    }
}
