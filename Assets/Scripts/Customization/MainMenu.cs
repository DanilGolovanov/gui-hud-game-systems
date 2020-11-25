using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Public variables
    public string LoadScene = "GameScene";
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public GameObject IWantToDisableThis;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider soundFXSlider;
    #endregion

    public void Start()
    {
        LoadPlayerPrefs();
        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            Screen.fullScreen = false;

        }
        else
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }

        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 5);//dont have magic numbers
            QualitySettings.SetQualityLevel(5);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LoadScene);
    }

    #region Change settings 

    //This changes the screen from fullscreen to windowed
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetSoundFXVolume(float value)
    {
        mixer.SetFloat("SoundFXVol", value);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", value);
    }

    #endregion

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }

    #region Save and load player prefs

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        //PlayerPrefs.SetInt("quality", qualityDropdown.value);
        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        float musicVol;
        if (mixer.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }

        float soundFXVol;
        if (mixer.GetFloat("SoundFXVol", out soundFXVol))
        {
            PlayerPrefs.SetFloat("SoundFXVol", soundFXVol);
        }

        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        //load quality
        if (PlayerPrefs.HasKey("quality"))
        {
            int quality = PlayerPrefs.GetInt("quality");
            qualityDropdown.value = quality;
            if (QualitySettings.GetQualityLevel() != quality)
            {
                ChangeQuality(quality);
            }
        }

        //load full screen
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                fullscreenToggle.isOn = false;
            }
            else
            {
                fullscreenToggle.isOn = true;
            }
        }

        //load audio sliders
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicVol;
            mixer.SetFloat("MusicVol", musicVol);
        }

        if (PlayerPrefs.HasKey("SoundFXVol"))
        {
            float soundFXVol = PlayerPrefs.GetFloat("SoundFXVol");
            soundFXSlider.value = soundFXVol;
            mixer.SetFloat("SoundFXVol", soundFXVol);
        }       
    }

    #endregion

    //public void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 10, 100, 90), "Testing box");
    //    if (GUI.Button(new Rect(20, 40, 80, 20), "Press me"))
    //    {
    //        IWantToDisableThis.SetActive(false);
    //        Debug.Log("Press me button got pressed");
    //    }
    //    if (GUI.Button(new Rect(20, 70, 80, 20), "Press me 2"))
    //    {
    //        Debug.Log("Press me 2 button got pressed");
    //        QuitGame();
    //    }
    //}
}