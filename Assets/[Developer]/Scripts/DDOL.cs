using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL Instance;
    public AudioClip BtnClick, Oversound,winClip;
    public AudioSource backgrounssource,effectssource;
    private void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (SoundVolume == 1)
        {
            backgrounssource.Play();
            effectssource.Play();
        }
        else
        {
            backgrounssource.Stop();
            effectssource.Stop();
        }
    }

    public void ButtonClick()
    {
        if (SoundVolume == 1)
        {
            effectssource.PlayOneShot(BtnClick);
        }
    }
    public void OverClick()
    {
        if (SoundVolume == 1)
        {
            effectssource.PlayOneShot(Oversound);
        }
    }
    public void GameWinClick()
    {
        if (SoundVolume == 1)
        {
            effectssource.PlayOneShot(winClip);
        }
    }
    public static int SoundVolume
    {
        get => PlayerPrefs.GetInt("Sound", 1);
        set => PlayerPrefs.SetInt("Sound", value);
    }
    public static int Level
    {
        get => PlayerPrefs.GetInt("Level",0);
        set => PlayerPrefs.SetInt("Level", value);
    }
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set => PlayerPrefs.SetInt("CurrentLevel", value);
    }
}
