using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI2D : MonoBehaviour
{
   public Slider healthSlider; // Reference to the UI's health bar.
   void Update () {
    healthSlider.value = GetComponent<Player2D> ().DisplayHP () / 3.0f;
    //updates slider value for health
    }
    //used in Game over canvas to load scenes
    public void restartLevel(){
        SceneManager.LoadScene("mainGame");
    }
    public void backToMainMenu(){
        SceneManager.LoadScene("mainMenu");
    }
}
