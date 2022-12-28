using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour
{
    public static HomeScreenManager Instance;
    public bool IsLevelButtonInText, IsGameOverOrWin, IsGamePause;
    public GameObject homescree, levelsscreen, gameplay, lossscreen, winscreen, pausescreen;
    public GameObject MusicObject, NextButton;
    public Text LevelText;
    public Sprite[] OnOff, Levels;
    public GameObject[] LevelButton, LevelsPrefabs;    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (DDOL.SoundVolume == 1)
        {
            MusicObject.GetComponent<Image>().sprite = OnOff[0];
        }
        else
        {
            MusicObject.GetComponent<Image>().sprite = OnOff[1];
        }

        if (IsLevelButtonInText)
        {
            for (int i = 0; i < LevelButton.Length; i++)
            {
                if (DDOL.Level >= i)
                {
                    LevelButton[i].GetComponent<Image>().sprite = Levels[0];
                    LevelButton[i].transform.GetChild(0).gameObject.SetActive(true);
                    LevelButton[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    LevelButton[i].GetComponent<Image>().sprite = Levels[1];
                    LevelButton[i].transform.GetChild(0).gameObject.SetActive(false);
                    LevelButton[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < LevelButton.Length; i++)
            {
                if (DDOL.Level >= i)
                {
                    LevelButton[i].GetComponent<Image>().sprite = Levels[i];
                    LevelButton[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    LevelButton[i].GetComponent<Image>().sprite = Levels[i + 5];
                    LevelButton[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }
    public void SoundOnOff()
    {
        DDOL.Instance.ButtonClick();
        if (DDOL.SoundVolume == 1)
        {
            DDOL.SoundVolume = 0;
            DDOL.Instance.backgrounssource.Stop();
            MusicObject.GetComponent<Image>().sprite = OnOff[1];
        }
        else
        {
            DDOL.SoundVolume = 1;
            DDOL.Instance.backgrounssource.Play();
            MusicObject.GetComponent<Image>().sprite = OnOff[0];
        }
    }
    public void SetLevelNumberOpen(int no)
    {
        DDOL.CurrentLevel = no;
        LevelText.text = "LEVEL " + (DDOL.CurrentLevel + 1);
        GameObject Temp_Level = Instantiate(LevelsPrefabs[DDOL.CurrentLevel]);
        homescree.SetActive(false);
        levelsscreen.SetActive(false);
        gameplay.SetActive(true);
    }
    public void GameOver()
    {
        if (!winscreen.activeSelf)
        {
            IsGameOverOrWin = true;
            DDOL.Instance.OverClick();
            lossscreen.SetActive(true);
        }
    }
    public void GameWin()
    {
        if (!lossscreen.activeSelf)
        {
            IsGameOverOrWin = true;
            DDOL.Instance.GameWinClick();
            if (DDOL.CurrentLevel == 4) { NextButton.SetActive(false); }
            winscreen.SetActive(true);
        }
    }
    public void Pause()
    {
        if (IsGamePause)
        {
            pausescreen.SetActive(false);
            IsGamePause = false;
            Time.timeScale = 1;
        }
        else
        {
            pausescreen.SetActive(true);
            IsGamePause = true;
            Time.timeScale = 0;
        }
    }
    public void ButtonClick(string Value)
    {
        switch (Value)
        {
            case "play":
                DDOL.Instance.ButtonClick();
                levelsscreen.SetActive(true);
                break;
            case "Gameplay":
                DDOL.Instance.ButtonClick();
                SceneManager.LoadScene(Value);
                break;
            case "backtohome":
                DDOL.Instance.ButtonClick();
                levelsscreen.SetActive(false);
                break;
            case "Home":
                DDOL.Instance.ButtonClick();
                SceneManager.LoadScene("HomeScreen");
                break;
            case "Restat":
                DDOL.Instance.ButtonClick();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Next":
                DDOL.Instance.ButtonClick();
                if (DDOL.Level == 0 && DDOL.CurrentLevel == 0) { DDOL.Level++; }
                else if (DDOL.Level == DDOL.CurrentLevel) { DDOL.Level++; }
                DDOL.CurrentLevel++;
                if (DDOL.CurrentLevel == 5)
                {
                    DDOL.Level = 4;
                    DDOL.CurrentLevel = 0;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
}
