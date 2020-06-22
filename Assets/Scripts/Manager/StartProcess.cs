using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartProcess : MonoBehaviour
{
    public void NewGame()
    {
        Message.SendMessage("New Game");
        LoadGame();
    }

    public void LoadSave(string filename)
    {
        Message.SendMessage("_" + filename);
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }
}
