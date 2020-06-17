using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour
{
    private const float GridWidth = 2.5f;
    private const int MapMaxIndex = 10;
    private const int NullType = 0;

    private static readonly string[] Type = {"Generate Null", "Generate Wall", "Generate Track", "Generate Enemy", "Generate Start Point", "Generate End Point" };

    private GameObject[] _generatePrefabs;
    public GameObject _WallPrefab;
    public GameObject _TrackPrefab;
    public GameObject _EnemyPrefab;
    public GameObject _StartPointPrefab;
    public GameObject _EndPointPrefab;

    private Plane _plane;
    public GameObject _levelObject;
    public Text _TypeField;

    private static int _currentLevel = 1;
    private int _currentType = 1;

    private int[,] _types = new int[21, 21];

    // Start is called before the first frame update
    void Start()
    {
        _plane = new Plane(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0));
        _generatePrefabs = new GameObject[5] {_WallPrefab, _TrackPrefab, _EnemyPrefab, _StartPointPrefab , _EndPointPrefab};
        LoadAndGenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitObjectFromClickedGrid();
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentType = (_currentType + 1) % Type.Length;
            SetType(Type[_currentType]);
        }

        else if (Input.GetKey(KeyCode.Return))
        {
            EventSystem.current.SetSelectedGameObject(_levelObject);
            SetLevel(_levelObject.GetComponent<InputField>().text);
        }
        
        else if (Input.GetKey(KeyCode.S))
        {
            SaveMap();
        }
    }

    private void InitObjectFromClickedGrid()
    {
        Vector3 hitpoint = GetHitPoint();

        if (IsHitted(hitpoint))
        {
            int column, row;
            hitpoint = SetPointToCenterOfGrid(hitpoint, out column, out row);

            if (_types[column, row] != _currentType)
            {
                _types[column, row] = _currentType;
                DestoryObject(hitpoint);

                if (_currentType != NullType)
                {
                    Instantiate(_generatePrefabs[_currentType - 1], hitpoint, Quaternion.identity);
                }
            }
        }
    }

    private void DestoryObject(Vector3 hitpoint)
    {
        List<GameObject> _objectsInScene = new List<GameObject>();

        foreach (GameObject _gameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (_gameObject.transform.position.x == hitpoint.x && _gameObject.transform.position.z == hitpoint.z)
            {
                Destroy(_gameObject);
                return;
            }
        }
    }

    private Vector3 SetPointToCenterOfGrid(Vector3 hitpoint, out int column, out int row)
    {
        hitpoint.x = hitpoint.x > 0 ? hitpoint.x + GridWidth / 2f : hitpoint.x - GridWidth / 2f;
        hitpoint.z = hitpoint.z > 0 ? hitpoint.z + GridWidth / 2f : hitpoint.z - GridWidth / 2f;

        column = (int) (hitpoint.x / GridWidth) + MapMaxIndex;
        row = (int) (hitpoint.z / GridWidth) + MapMaxIndex;

        hitpoint.x = (column - 10) * GridWidth;
        hitpoint.z = (row - 10) * GridWidth;

        return hitpoint;
    }

    private bool IsHitted(Vector3 hitpoint)
    {
        return hitpoint != Vector3.one && Math.Abs(hitpoint.x) < MapMaxIndex * GridWidth + GridWidth / 2 
            && Math.Abs(hitpoint.z) < MapMaxIndex * GridWidth + GridWidth / 2;
    }

    private Vector3 GetHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter;
        if (_plane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.one;
    }

    void SetLevel(string levelStr)
    {
        _currentLevel = Int32.Parse(levelStr);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetType(string typeStr)
    {
        _TypeField.text = typeStr;
    }

    void SaveMap ()
    {
        using (var filestream = File.Create(Application.dataPath + "/Data/map" + _currentLevel + ".bin"))
            using (var binarystream = new BinaryWriter(filestream))
        {
            foreach (var type in _types)
            {
                binarystream.Write(type);
            }
        }
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
                    }
                }
            }
        }
    }
}
