using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Map : MonoBehaviour
{
    private string cacheMapPath;
    private int[,] gridType = new int[21, 21];

    private Helper.GridPoint startPoint;
    private Helper.GridPoint playerCurrentPoint;

    private GameObject[] prefabs;
    public GameObject wallPrefab;
    public GameObject trackPrefab;
    public GameObject enemyPrefab;
    public GameObject startPointPrefab;
    public GameObject endPointPrefab;
    public GameObject healPrefab;

    private List<GameObject> gameObjects = new List<GameObject>();

    void Awake()
    {
        cacheMapPath = Application.dataPath + TempConstant.cacheMapInfoDirectory;

        wallPrefab = Resources.Load("Prefabs/wall", typeof(GameObject)) as GameObject;
        trackPrefab = Resources.Load("Prefabs/track", typeof(GameObject)) as GameObject;
        enemyPrefab = Resources.Load("Prefabs/enemy", typeof(GameObject)) as GameObject;
        startPointPrefab = Resources.Load("Prefabs/startPoint", typeof(GameObject)) as GameObject;
        endPointPrefab = Resources.Load("Prefabs/endPoint", typeof(GameObject)) as GameObject;
        healPrefab = Resources.Load("Prefabs/healPoint", typeof(GameObject)) as GameObject;

        prefabs = new GameObject[6] { wallPrefab, trackPrefab, enemyPrefab, startPointPrefab, endPointPrefab, healPrefab };
    }

    internal void SaveMapInfo(int currentLevel)
    {
        using (var filestream = File.Create(Application.dataPath + TempConstant.cacheMapInfoDirectory + "map" + currentLevel + ".bin"))
        using (var binarystream = new BinaryWriter(filestream))
        {
            foreach (int type in gridType)
            {
                binarystream.Write(type);
            }
        }
    }

    internal void SetCurrentPlayerPointType(int type)
    {
        gridType[playerCurrentPoint.x, playerCurrentPoint.y] = type;
    }

    internal GameObject GetEnemy(int type)
    {
        return gameObjects[type - 11];
    }

    internal void DestoryMap()
    {
        GameObject[] mapObject = GameObject.FindGameObjectsWithTag("Map");

        for (int i = 0; i < mapObject.Length; i++)
        {
            Destroy(mapObject[i]);
        }

        gameObjects.Clear();
        Debug.Log(gameObjects.Count + "...");
    }

    internal void SetPlayerToStartPointPosition(Player player)
    {
        player.transform.position = Helper.GridPointToPosition(startPoint, 0.6f);
        playerCurrentPoint = startPoint;
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
        Debug.Log("currentLevel: " + currentLevel);

        using (var filestream = File.Open(cacheMapPath + "map" + currentLevel + ".bin", FileMode.Open))
        using (var binaryStream = new BinaryReader(filestream))
        {
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 21; y++)
                {
                    gridType[x, y] = binaryStream.ReadInt32();

                    if (IsEnemyType(gridType[x, y]))
                    {
                        gameObjects.Add(Instantiate(enemyPrefab, new Vector3((x - 10) * TempConstant.GridWidth, 0, (y - 10) * TempConstant.GridWidth), Quaternion.identity));
                        gridType[x, y] = 10 + gameObjects.Count;
                    }

                    else if (gridType[x, y] != TempConstant.NullType)
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

    private bool IsEnemyType(int type)
    {
        return (type == TempConstant.EnemyType) || (type > 10);
    }

    public void GenerateBorder()
    {
        GameObject borderPrefab = Resources.Load("Prefabs/border", typeof(GameObject)) as GameObject;

        float borderLeftAndBottomValue = TempConstant.BorderLeftAndBottomIndex * TempConstant.GridWidth;
        float borderRightAndAboveValue = -borderLeftAndBottomValue;

        for (int i = TempConstant.BorderLeftAndBottomIndex + 1; i < TempConstant.BorderRightAndAboveIndex; i++)
        {
            float PositionValue = i * TempConstant.GridWidth;

            Instantiate(borderPrefab, new Vector3(PositionValue, 0, borderLeftAndBottomValue), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(PositionValue, 0, borderRightAndAboveValue), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(borderLeftAndBottomValue, 0, PositionValue), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(borderRightAndAboveValue, 0, PositionValue), Quaternion.identity);
        }

        Instantiate(borderPrefab, new Vector3(borderLeftAndBottomValue, 0, borderLeftAndBottomValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(borderPrefab, new Vector3(borderLeftAndBottomValue, 0, borderRightAndAboveValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(borderPrefab, new Vector3(borderRightAndAboveValue, 0, borderRightAndAboveValue), Quaternion.identity).transform.SetParent(this.transform);
        Instantiate(borderPrefab, new Vector3(borderRightAndAboveValue, 0, borderLeftAndBottomValue), Quaternion.identity).transform.SetParent(this.transform);
    }

    internal bool GotoNewGrid(Vector3 position)
    {
        Helper.GridPoint gridPoint = Helper.PositionToGridPoint(position);

        if (Helper.IsEqual(gridPoint, playerCurrentPoint))
            return false;

        playerCurrentPoint = gridPoint;
        return true;
    }
    internal void SetCurrentPlayerPosition(Vector3 position)
    {
        playerCurrentPoint = Helper.PositionToGridPoint(position);
    }

    internal int GetCurrentPositionType()
    {
        return gridType[playerCurrentPoint.x, playerCurrentPoint.y];
    }
}
