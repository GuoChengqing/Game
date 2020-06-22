using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSaveWindowManager : MonoBehaviour
{
    private Animator animator;
    public Text saveName;
    public GameObject savesPanel;
    public GameObject saveListItem;
    public StartProcess startProcess;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (saveName == null)
            saveName = transform.Find("SaveName").GetComponent<Text>();

        if (savesPanel == null)
            savesPanel = transform.Find("SaveList").Find("Panel").gameObject;

        GenerateSaves();
    }

    private void GenerateSaves()
    {
        string[] savenames = SavesManager.GetSaveNamesWithOutPath();
        GenerateSaveItem(savenames);
    }

    private void GenerateSaveItem(string[] savenames)
    {

        int i = 0;
        foreach (string filename in savenames)
        {
            GameObject saveItemInstantiate = Instantiate(saveListItem, new Vector3(0, 0, 0), Quaternion.identity);
            saveItemInstantiate.GetComponent<RectTransform>().SetParent(savesPanel.transform, false);
            saveItemInstantiate.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -23 - (46 * i++));
            saveItemInstantiate.GetComponent<SendSelectedSaveNameToManager>().chooseSaveWindowManager = this;
            saveItemInstantiate.transform.Find("Text").GetComponent<Text>().text = filename;
        }
    }

    public void SwitchWindow()
    {
        animator.SetBool("isOn", !animator.GetBool("isOn"));
    }

    public void TurnOnWindow()
    {
        animator.SetBool("isOn", true);
    }

    public void TurnOffWindow()
    {
        animator.SetBool("isOn", false);
    }

    public void SetSeletedSaveName(string saveName)
    {
        this.saveName.text = saveName;
    }

    public void LoadGame()
    {
        startProcess.LoadSave(saveName.text);
    }
}
