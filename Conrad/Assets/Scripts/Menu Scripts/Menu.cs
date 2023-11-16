using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("BunkerLevel");
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene("Settings Menu");
    }

    public void OnSurvivalPress()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
