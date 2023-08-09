using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    int currentRound;
    public int currentKillCount; //increment in enemy scripts before they die
    int objectiveKills=0;
    bool inRound=false;
    public int coinCount = 0;
    placeSpawners placeSpawnerScript;

    GameObject[] allEnemies;
    GameObject[] allSpawners;
    GameObject[] allEnemyBullets;
    //things to destroy after each round
    
    [SerializeField] GameObject shopObject;
    [SerializeField] GameObject enterRoundPrompt;
    [SerializeField] GameObject roundStartUI;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject shopUI;
    
    
    void Start()
    {
       placeSpawnerScript = (GameObject.FindGameObjectWithTag("spawnManager")).GetComponent<placeSpawners>();
    }

    // Update is called once per frame
    void Update()
    { //manage rounds, shop, when objective met, clears enemies and UI after round

        if (inRound == true){
            objectiveText.text = "Objective:  Kill " + objectiveKills +  " enemies || Killed: " + currentKillCount;
        }//set the objective if we enter a round

        if (Input.GetKeyUp(KeyCode.Return) && inRound == false && shopUI.activeSelf == false)  
        {//if we press the enter key and we are out of round, we are now back in round and need to do the following
            placeSpawnerScript.placeSpawnersRandomly(); //place spawners
            shopObject.SetActive(false);                //set shop inactive
            objectiveKills = objectiveKills + 2;        //next objective we need 5 more kills than last objective to complete the round
            enterRoundPrompt.SetActive(false);          //disable prompt to start round
            inRound = true;                             
            currentRound = currentRound+1;              //increment round
            roundText.text = "Round " + currentRound;   
            roundStartUI.SetActive(true);
            enemySpawner.SetActive(true);
            //add spawner (set active) and change spanwer script to spawn in multiple locations

            
        }

        //destroys stuff out of rounds in order to not effect gameplay performance
        if (inRound == true && (currentKillCount >= objectiveKills)) 
        {//runs only once, as currentKillCount is reset back to 0
            objectiveText.text = "Objective:  Complete! ";
            FindObjectOfType<AudioManager>().Play("round_victory");

            allSpawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in allSpawners) {
                Destroy(spawner);
            }//destroys all spawners out of round

            foreach (Transform child in (GameObject.FindGameObjectWithTag("destroyManager")).transform) {
                GameObject.Destroy(child.gameObject);
            }//destroys all bullets, enemies, enemy bullets that was stored in destroyManager

            currentKillCount = 0;
            enterRoundPrompt.SetActive(true);
            shopObject.SetActive(true);
            enterRoundPrompt.SetActive(true);
            inRound=false;
        }
    }
    public void incrementCoin() 
    {
        coinCount = coinCount + 1;
        coinText.text = "Coins: " + coinCount;
    }
    public void takeCoins(int coinsToLose)
    {
        coinCount = coinCount - coinsToLose;
        coinText.text = "Coins: " + coinCount;
    }
}