using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Load different scenes based on button inputs
    public void loadgame(){
        FindObjectOfType<AudioManager>().Play("round_victory");
        SceneManager.LoadScene("mainGame");
    }

    public void loadcred(){
        FindObjectOfType<AudioManager>().Play("menu_click");
        SceneManager.LoadScene("creditScreen");
    }

    public void loadmenu(){
        FindObjectOfType<AudioManager>().Play("menu_click");
        SceneManager.LoadScene("mainMenu");
    }

    public void loadhowto(){
        FindObjectOfType<AudioManager>().Play("menu_click");
        SceneManager.LoadScene("howToPlay");
    }
}
