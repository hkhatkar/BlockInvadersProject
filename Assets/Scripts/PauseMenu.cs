using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    [SerializeField] Canvas GameOverCanvas;
    [SerializeField] GameObject shopUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameOverCanvas.enabled == false && shopUI.activeSelf == false)
        {//make sure that we cant open pause menu when the game over menu or shop menu are open
            if (paused)
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
    {//public so it is accessible by the resume button
        FindObjectOfType<AudioManager>().Play("menu_click");
        pauseUI.SetActive(false);
        Time.timeScale= 1f;
        paused = false;
    }
    void Paused()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
}
