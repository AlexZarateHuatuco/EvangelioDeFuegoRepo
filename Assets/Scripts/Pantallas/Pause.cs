using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menuPause;
    public bool gamePause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        gamePause = false;
    }
    public void Paused()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        gamePause = true;
    }
    public void Menu()
    {
        Resume();
        SceneManager.LoadScene("MenuInicio");
    }
}
