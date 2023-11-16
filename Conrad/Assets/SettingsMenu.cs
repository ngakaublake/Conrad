using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{

    Resolution[] Resolutions;
    public TMPro.TMP_Dropdown ResDropDown;

    private void Start()
    {
        Resolutions = Screen.resolutions;

        ResDropDown.ClearOptions();

        List<string> Options = new List<string>();

        int CurrentResIndex = 0;

        for (int i = 0; i < Resolutions.Length; i++)
        {
            string Res = Resolutions[i].width + " x " + Resolutions[i].height + " @ " + Resolutions[i].refreshRate + "hz"; 
            Options.Add(Res);

            if (Resolutions[i].width == Screen.width && Resolutions[i].height == Screen.height)
            {
                CurrentResIndex = i;
            }

        }

        ResDropDown.AddOptions(Options);
        ResDropDown.value = CurrentResIndex;
        ResDropDown.RefreshShownValue();
    }
    public AudioMixer AudioMixer;

    public void SetVolume(float _Volume)
    {
        AudioMixer.SetFloat("MasterVolume", _Volume);
    }

    public void SetScreenMode(bool _isFullScreen)
    {
        Screen.fullScreen = _isFullScreen;
    }

    public void SetRes(int _ResIndex)
    {
        Resolution Res = Resolutions[_ResIndex];
        Screen.SetResolution(Res.width, Res.height, Screen.fullScreen);
    }

    public void GoBack(float _Volume)
    {
        SceneManager.LoadScene("MainMenu");
    }


}
