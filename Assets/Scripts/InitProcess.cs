using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InitProcess : MonoBehaviour
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = Instantiate(wall, new Vector3(0, 0.1f, 0), Quaternion.identity);
        //Instantiate(wall, new Vector3(1, 0.1f, 0), Quaternion.identity);
        //Instantiate(wall, new Vector3(2, 0.1f, 0), Quaternion.identity);
        //Instantiate(wall, new Vector3(4, 0.1f, 0), Quaternion.identity);
        //Instantiate(wall, new Vector3(6, 0.1f, 5), Quaternion.identity);
        GameObjectUtility.SetNavMeshArea(g, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
