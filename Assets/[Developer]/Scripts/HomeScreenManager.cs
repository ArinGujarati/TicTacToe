using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour
{
    public static HomeScreenManager Instance;    
    public GameObject MusicObject;
    public Sprite[] OnOff;    
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
   
    public void ButtonClick(string Value)
    {
        switch (Value)
        {
            case "Player":
                DDOL.Instance.ButtonClick();                
                AlwasOnScript.Instance.SelectedMode = "P vs P";
                SceneManager.LoadScene("Main");
                break;
            case "Computer":
                DDOL.Instance.ButtonClick();                
                AlwasOnScript.Instance.SelectedMode = "P vs C";
                SceneManager.LoadScene("Main");
                break;           
        }
    }
}
