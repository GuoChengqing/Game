using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public float _deviation = 2f;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        if(!FindPlayer())
        {
            Debug.Log("Not found Player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CollideWithPlayer())
        {
            CalculateAttack(_player);
        }
    }

    private void CalculateAttack(GameObject playerObject)
    {
        Character _enemy = GetComponent<Character>();
        Character _player = playerObject.GetComponent<Character>();

        while (_enemy.health > 0 && _player.health > 0)
        {
            Attack(_enemy, _player);
            Attack(_player, _enemy);
        }

        if (_player.health <= 0)
        {
            Destroy(playerObject);
            Debug.Log("Game Over");
        }
        if (_enemy.health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Good Job");
        }
    }

    private void Attack(Character playerA, Character playerB)
    {
        playerB.health -= ((playerA.attack - playerB.armor) > 0 ? playerA.attack - playerB.armor : 0);
    }

    private bool CollideWithPlayer()
    {
        return (transform.position.x < _player.transform.position.x + _deviation &&
            transform.position.x > _player.transform.position.x - _deviation &&
            transform.position.z < _player.transform.position.z + _deviation &&
            transform.position.z > _player.transform.position.z - _deviation);
    }

    private bool FindPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
            return false;

        return true;
    }
}
