using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempConstant
{
    public const string defaultMapInfoDirectory = "/Data/defaultMap/";
    public const string cacheMapInfoDirectory = "/Data/Cache/Map/";
    public const string defaultPlayerInfoFile = "/Data/defaultPlayerInfo.bin";
    public const string playerPrefab = "Prefabs/player";

    public const int NullType = 0;
    public const int WallType = 1;
    public const int TrackType = 2;
    public const int EnemyType = 3;
    public const int StartPointType = 4;
    public const int EndPointType = 5;
    public const int HealPointType = 6;

    public const float GridWidth = 2.5f;
    public const int BorderLeftAndBottomIndex = -11;
    public const int BorderRightAndAboveIndex = 11;
    public const int HalfMapWidth = 10;
}
