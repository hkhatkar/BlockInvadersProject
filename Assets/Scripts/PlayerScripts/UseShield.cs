using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShield : MonoBehaviour
{
    [SerializeField]
    GameObject shieldObject;
    playerPowerBar powerScript;
    [SerializeField]
    GameObject powerOutParticle;
    public bool shieldUp = false; //used in Player2D to check whether player takes damage
    public bool disabledShield = false;
   
    void Start()
    {
        powerScript = GameObject.Find("powerFillBar").GetComponent<playerPowerBar>();
        //script for managing ui of shield power bar
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && powerScript.currentValue > 0 && disabledShield == false)
        {//if we get the right click button down and the bar isnt depleted and shield is currently disabled
           
            shieldUp = true;
            shieldObject.SetActive(true);
            //set shield active
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) || powerScript.currentValue <= 0)
        {//otherwise right click is up or the power is depleted

            shieldUp = false;
            shieldObject.SetActive(false);
            //set shield inactive 
            //check if power is depleted. if it is call this coroutine which is a cooldown, we cant shield again until the waiting time is over and the shield is recharged
            if (powerScript.currentValue <= 0) {StartCoroutine(cooldownForPower());}
        }
  
    }

    IEnumerator cooldownForPower()
    {//wait 4 seconds for the shield to recharge in the playerPowerBar script
        FindObjectOfType<AudioManager>().Play("shield_break");
        GameObject currentPowerOutP = Instantiate(powerOutParticle, gameObject.transform.position, Quaternion.identity);
        currentPowerOutP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
        disabledShield = true;
        yield return new WaitForSeconds(4f);
        disabledShield = false;

    }
}
