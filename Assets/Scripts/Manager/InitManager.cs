using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    public GameObject _wallPrefab;

    private int[,] types = new int[21, 21];

    private int currentLevel = 1;
    private const int BorderWidth = 23;
    private const float ObjectSize = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBorder();
        //LoadAndGenerateMap();
    }


    private void LoadAndGenerateMap()
    {
        using (var filestream = File.Open(Application.dataPath + "/Data/map" + currentLevel + ".bin", FileMode.Open))
            using (var binaryStream = new BinaryReader(filestream))
        {
            for (int x = 0; x < BorderWidth - 2; x++)
            {
                for (int y = 0; y < BorderWidth - 2; y++)
                {
                    types[x, y] = binaryStream.ReadInt32();
                    if (types[x, y] == 1)
                    {
                        Instantiate(_wallPrefab, new Vector3((x - 11) * 2.5f, 0, (y - 11) * 2.5f), Quaternion.identity);
                    }
                }
            }
        }
    }

    private void GenerateBorder()
    {
        int minIndex = -(BorderWidth - 1) / 2;
        int maxIndex = -minIndex;
        float minPositionValue = minIndex * ObjectSize;
        float maxPositionValue = -minPositionValue;

        for (int i = minIndex + 1; i < maxIndex; i++)
        {
            float PositionValue = i * ObjectSize;

            Instantiate(_wallPrefab, new Vector3(PositionValue, 0, minPositionValue), Quaternion.identity);
            Instantiate(_wallPrefab, new Vector3(PositionValue, 0, maxPositionValue), Quaternion.identity);
            Instantiate(_wallPrefab, new Vector3(minPositionValue, 0, PositionValue), Quaternion.identity);
            Instantiate(_wallPrefab, new Vector3(maxPositionValue, 0, PositionValue), Quaternion.identity);
        }

        Instantiate(_wallPrefab, new Vector3(minPositionValue, 0, minPositionValue), Quaternion.identity);
        Instantiate(_wallPrefab, new Vector3(minPositionValue, 0, maxPositionValue), Quaternion.identity);
        Instantiate(_wallPrefab, new Vector3(maxPositionValue, 0, maxPositionValue), Quaternion.identity);
        Instantiate(_wallPrefab, new Vector3(maxPositionValue, 0, minPositionValue), Quaternion.identity);
    }
}
