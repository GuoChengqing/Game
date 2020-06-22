using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character1
{
    public int maxHealth;
    public int currentLevel;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Load(string file)
    {
        using (var filestream = File.Open(file, FileMode.Open))
            using (var binarystream = new BinaryReader(filestream))
        {
            currentHealth = binarystream.ReadInt32();
            maxHealth = binarystream.ReadInt32();
            armor = binarystream.ReadInt32();
            attack = binarystream.ReadInt32();
            currentLevel = binarystream.ReadInt32();
        }
    }

    internal void SetDestination(Vector3 point)
    {
        agent.destination = point;
    }
}
