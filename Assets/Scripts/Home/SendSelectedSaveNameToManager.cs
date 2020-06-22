using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendSelectedSaveNameToManager : MonoBehaviour
{
    public ChooseSaveWindowManager chooseSaveWindowManager;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Send);
    }

    private void Send()
    {
        string saveName = transform.Find("Text").GetComponent<Text>().text;

        chooseSaveWindowManager.SetSeletedSaveName(saveName);
    }
}
