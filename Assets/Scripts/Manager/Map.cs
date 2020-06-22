using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    private string cacheMapPath;
    private int[,] gridType = new int[21, 21];
    private struct Point
    {
        public int x;
        public int y;
    };

    private Point startPoint;

    private GameObject[] prefabs;
    public GameObject wallPrefab;
    public GameObject trackPrefab;
    public GameObject enemyPrefab;
    public GameObject startPointPrefab;
    public GameObject endPointPrefab;


    void Awake()
    {
        cacheMapPath = Application.dataPath + TempConstant.cacheMapInfoDirectory;
        
        wallPrefab = Resources.Load("Prefabs/wall", typeof(GameObject)) as GameObject;
        trackPrefab = Resources.Load("Prefabs/track", typeof(GameObject)) as GameObject;
        enemyPrefab = Resources.Load("Prefabs/enemy", typeof(GameObject)) as GameObject;
        startPointPrefab = Resources.Load("Prefabs/startPoint", typeof(GameObject)) as GameObject;
        endPointPrefab = Resources.Load("Prefabs/endPoint", typeof(GameObject)) as GameObject;

        prefabs = new GameObject[5] { wallPrefab, trackPrefab, enemyPrefab, startPointPrefab, endPointPrefab };

    }
    internal void Load(string defaultMapInfoDirectory)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(defaultMapInfoDirectory);

        if (!directoryInfo.Exists)
        {
            throw new DirectoryNotFoundException(
                "defaultMap directory does not exist or could not be found: "
                + Application.dataPath + defaultMapInfoDirectory);
        }

        FileInfo[] files = directoryInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if (!file.Extension.Equals(".meta"))
            {
                string temppath = Path.Combine(cacheMapPath, file.Name);
                file.CopyTo(temppath, true);
            }
        }
    }

    internal void GenerateMap(int currentLevel)
    {
        using (var filestream = File.Open(cacheMapPath + "map" + currentLevel + ".bin", FileMode.Open))
        using (var binaryStream = new BinaryReader(filestream))
        {
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 21; y++)
                {
                    gridType[x, y] = binaryStream.ReadInt32();

                    if (gridType[x, y] != TempConstant.NullType)
                    {
                        Instantiate(prefabs[gridType[x, y] - 1], new Vector3((x - 10) * TempConstant.GridWidth, 0, (y - 10) * TempConstant.GridWidth), Quaternion.identity).transform.SetParent(this.transform);

                        if (gridType[x, y] == TempConstant.StartPointType)
                        {
                            startPoint.x = x;
                            startPoint.y = y;
                        }
                    }
                }
            }
        }
    }

    public void GenerateBorder()
    {

        float borderLeftAndBottomValue = TempConstant.BorderLeftAndBottomIndex * TempConstant.GridWidth;
        float borderRightAndAboveValue = -borderLeftAndBottomValue;

        for (int i = TempConstant.BorderLeftAndBottomIndex + 1; i < TempConstant.BorderRightAndAboveIndex; i++)
        {
            float PositionValue = i * TempConstant.GridWidth;

            Instantiate(wallPrefab, new Vector3(PositionValue, 0, borderLeftAndBottomValue), Quaternion.identity).transform.SetParent(this.transform);
            Instantiate(wallPrefab, new Vector3(PositionValue, 0, borderRightAndAboveValue), Quaternion.identity).transform.SetParent(this.transform);
            Instantiate(wallPrefab, new Vector3(borderLeftAndBottomValue, 0, PositionValue), Quaternion.identity).transform.SetParent(this.transform);
            Instantiate(wallPrefab, new Vector3(borderRightAndAboveValue, 0, PositionValue), Quaternion.identity).transform.SetParent(this.transform);
        }

        Instantiate(wallPrefab, new Vector3(borderLeftAndBottomValue, 0, borderLeftAndBottomValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(wallPrefab, new Vector3(borderLeftAndBottomValue, 0, borderRightAndAboveValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(wallPrefab, new Vector3(borderRightAndAboveValue, 0, borderRightAndAboveValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(wallPrefab, new Vector3(borderRightAndAboveValue, 0, borderLeftAndBottomValue), Quaternion.identity).transform.SetParent(this.transform);
    }
}
