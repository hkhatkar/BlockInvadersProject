using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehaviour : MonoBehaviour
{
    private GameObject rm;

    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("roundManager");
         
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {//if the player enters the coins trigger then update players coin count in round manager and deactivate coin
            FindObjectOfType<AudioManager>().Play("get_coin");
            rm.GetComponent<RoundManager>().incrementCoin();
            gameObject.SetActive(false);
        } 
    }
    IEnumerator DestroyGo()
    {
        //Wait then set inactive after time
        yield return new WaitForSeconds(10f);
        this.gameObject.SetActive(false);
    }
}
