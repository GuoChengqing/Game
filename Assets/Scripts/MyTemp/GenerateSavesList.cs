using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateSavesList : MonoBehaviour
{
    public GameObject saveItem;
    private string[] filenames;

    // Start is called before the first frame update
    void Start()
    {
        GetFileNames();
        GenerateSaveItem();
    }

    private void GenerateSaveItem()
    {
        foreach(string filename in filenames)
        {
            GameObject saveItemInstantiate = Instantiate(saveItem, new Vector3(0, 0, 0), Quaternion.identity);
            saveItemInstantiate.transform.Find("Text").GetComponent<Text>().text = filename;
        }
    }

    private void GetFileNames()
    {
        filenames = gameObject.AddComponent<SaveManager>().GetAllSavesPath();
        //string[] filepathes = new SaveManager().GetAllSavesPath();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
