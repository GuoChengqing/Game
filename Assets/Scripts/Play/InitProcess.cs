using System;
using UnityEngine;

public class InitProcess : MonoBehaviour
{
    private const string NotNewGameSignal = "s_";
    public PlayManager playManager;

    void Start()
    {
        playManager = new GameObject("Play Manager").AddComponent<PlayManager>();

        if (IsNewGame())
            LoadDefaultData();
        else
            LoadSaveData();

        playManager.map.GenerateBorder();
        playManager.map.GenerateMap(playManager.player.currentLevel);
        playManager.map.SetCurrentPlayerPosition(playManager.player.transform.position);

        Destroy(this.gameObject);
    }

    private bool IsChangeLevel()
    {
        return Message.message.StartsWith("l_");
    }

    private void LoadDefaultData()
    {
        Map map = new GameObject("Map").AddComponent<Map>();
        playManager.GetComponent<PlayManager>().map = map;
        map.Load(Application.dataPath + TempConstant.defaultMapInfoDirectory);

        GameObject playerPrefab = Resources.Load(TempConstant.playerPrefab, typeof(GameObject)) as GameObject;
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Player player = playerObject.AddComponent<Player>();
        playerObject.transform.SetParent(map.transform);
        playManager.GetComponent<PlayManager>().player = player;
        player.Load(Application.dataPath + TempConstant.defaultPlayerInfoFile);

        //PlayerInfo playerInfo = new PlayerInfo();
        //playerInfo.Load(TempConstant.defaultPlayerInfoFile);

        //MapInfo mapInfo = new MapInfo();
        //mapInfo.Load(Application.dataPath + TempConstant.defaultMapInfoDirectory);

        //GameObject player = playerInfo.GeneratePlayer();
        //GameObject map = mapInfo.generateMap();
    }

    private void LoadSaveData()
    {
        throw new NotImplementedException();
    }

    private bool IsNewGame()
    {
        return !Message.GetMessage().StartsWith(NotNewGameSignal);
    }

    //private void LoadDefaultMap()
    //{
    //    string cacheMapPath = cachePath + "/Map/";

    //    DirectoryInfo directoryInfo = new DirectoryInfo(defaultMapPath);

    //    if (!directoryInfo.Exists)
    //    {
    //        throw new DirectoryNotFoundException(
    //            "defaultMap directory does not exist or could not be found: "
    //            + defaultMapPath);
    //    }

    //    FileInfo[] files = directoryInfo.GetFiles();
    //    foreach (FileInfo file in files)
    //    {
    //        if (!file.Extension.Equals(".meta"))
    //        {
    //            string temppath = Path.Combine(cacheMapPath, file.Name);
    //            file.CopyTo(temppath, true);
    //        }

    //    }
    //}

    //private void LoadDefaultPlayerAttribute()
    //{
    //    Player player = new Player();
    //    player.Load(Application.dataPath + "/Data/defaultPlayer/playerData.bin");
    //    Debug.Log(player.attack);
    //}
}
