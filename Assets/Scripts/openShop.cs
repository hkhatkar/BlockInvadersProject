using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openShop : MonoBehaviour
{//simple script to set shop UI active when the player enters the trigger
    [SerializeField] GameObject shopUI;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="Player")
        {//if tag is player set the UI to active
            shopUI.SetActive(true);
        }
    }
      void OnTriggerExit2D(Collider2D col)
    {//if the player leaves the trigger zone then set back to inactive
        if (col.gameObject.tag == "Player")
        {
            shopUI.SetActive(false);
        }
    }
}
