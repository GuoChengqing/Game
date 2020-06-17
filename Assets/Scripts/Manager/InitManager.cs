using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private const int NullType = 0;
    private const int StartPointType = 4;

    private GameObject[] _generatePrefabs;
    public GameObject _wallPrefab;
    public GameObject _TrackPrefab;
    public GameObject _EnemyPrefab;
    public GameObject _StartPointPrefab;
    public GameObject _EndPointPrefab;
    public GameObject _Player;
    private int[,] _types = new int[21, 21];

    private static int _currentLevel = 1;
    private const int BorderWidth = 23;
    private const float GridWidth = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        _generatePrefabs = new GameObject[5] { _wallPrefab, _TrackPrefab, _EnemyPrefab, _StartPointPrefab, _EndPointPrefab };
        GenerateBorder();
        LoadAndGenerateMap();
    }

    private void LoadAndGenerateMap()
    {
        using (var filestream = File.Open(Application.dataPath + "/Data/map" + _currentLevel + ".bin", FileMode.Open))
        using (var binaryStream = new BinaryReader(filestream))
        {
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 21; y++)
                {
                    _types[x, y] = binaryStream.ReadInt32();

                    if (_types[x, y] != NullType)
                    {
                        Instantiate(_generatePrefabs[_types[x, y] - 1], new Vector3((x - 10) * GridWidth, 0, (y - 10) * GridWidth), Quaternion.identity);

                        if (_types[x, y] == StartPointType)
                        {
                            Instantiate(_Player, new Vector3((x - 10) * GridWidth, 0, (y - 10) * GridWidth), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    private void GenerateBorder()
    {
        int minIndex = -(BorderWidth - 1) / 2;
        int maxIndex = -minIndex;
        float minPositionValue = minIndex * GridWidth;
        float maxPositionValue = -minPositionValue;

        for (int i = minIndex + 1; i < maxIndex; i++)
        {
            float PositionValue = i * GridWidth;

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
