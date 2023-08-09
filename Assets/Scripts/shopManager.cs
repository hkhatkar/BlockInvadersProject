using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI dialogue;
    missileManager missileScript;
    Player2D healthScript;
    playerPowerBar shieldScript;
    [SerializeField] RectTransform shieldBar;
    [SerializeField] RectTransform shieldBarBG;
    RoundManager roundScript;


    void Start()
    {
        missileScript = (GameObject.FindGameObjectWithTag("missileManager")).GetComponent<missileManager>();
        healthScript = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player2D>();
        shieldScript = (GameObject.FindGameObjectWithTag("shieldPowerManager")).GetComponent<playerPowerBar>();
        //all scripts that may be altered during purchases made in shop

        roundScript = (GameObject.FindGameObjectWithTag("roundManager")).GetComponent<RoundManager>();
        //used to get the coins the player has

    }


    public void buyMissile()
    {//activated buy missile button pressed
        if (roundScript.coinCount >= 3 && missileScript.missileCount < 5)
        {//successful purchase
            FindObjectOfType<AudioManager>().Play("buy");
            missileScript.UpdateCount(1);
            dialogue.SetText("Missiles? Be careful!");
            roundScript.takeCoins(3);
        }
        else if (missileScript.missileCount >= 5)
        {//cannot buy we already have 5 missiles
             dialogue.SetText("You have 5 missiles already!");
        }
        else {dialogue.SetText("You can't afford missiles!");}
    }
    
    public void buyHeart()
    {//activated buy heart button pressed
        if (roundScript.coinCount >= 3 && healthScript.DisplayHP() < 3)
        {//successful purchase
            FindObjectOfType<AudioManager>().Play("buy");
            healthScript.hp++;
            dialogue.SetText("Health? Thank you!");
            roundScript.takeCoins(3);
        }
        else if (healthScript.DisplayHP()>= 3)
        {//cannot buy if our health is already at max
            dialogue.SetText("Your health is full!");
        }
        else {dialogue.SetText("You can't afford health!");}
    }

    public void buyShield()
    {//activated buy shield button pressed
        if (roundScript.coinCount >= 3)
        {//successful purchase
            FindObjectOfType<AudioManager>().Play("buy");
            shieldScript.MyMaxPowerValue = shieldScript.MyMaxPowerValue + 50;
            shieldBar.sizeDelta = new Vector2 (shieldBar.sizeDelta.x +40, shieldBar.sizeDelta.y);
            shieldBarBG.sizeDelta = new Vector2 (shieldBarBG.sizeDelta.x +40, shieldBarBG.sizeDelta.y);
            dialogue.SetText("Shield? Good choice.");
            roundScript.takeCoins(3);
        }
        else {dialogue.SetText("You can't afford shields!");}
    }
}
