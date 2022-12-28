using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitgameScript : MonoBehaviour
{
    public static ExitgameScript Instance;
    public GameObject gameplay, lossscreen, winscreen, pausescreen;
    public bool IsGamePause;
    private void Start()
    {
        Instance = this;
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
            case "Home":
                DDOL.Instance.ButtonClick();
                SceneManager.LoadScene("HomeScreen");
                break;
            case "Restat":
                DDOL.Instance.ButtonClick();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;                          
        }
    }
}
