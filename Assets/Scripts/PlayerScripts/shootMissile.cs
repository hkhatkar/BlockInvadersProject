using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootMissile : MonoBehaviour
{

    [SerializeField] GameObject missilePrefab;
    missileManager missileUIScript;
    Player2D playerScript;
    
    void Start()
    {
        playerScript = gameObject.GetComponent<Player2D>();
        missileUIScript = (GameObject.FindGameObjectWithTag("missileManager")).GetComponent<missileManager>();
        //finds the script for missiles that manages its UI
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space) && missileUIScript.missileCount > 0) 
        {//if space is pressed instantiate a missile prefab and destroy after 10 seconds if it doesnt hit anything
         //also decrement the UI count by calling the UpdateCount function
                GameObject m = Instantiate(missilePrefab, transform.position, playerScript.bulletRot);
                FindObjectOfType<AudioManager>().Play("missile_launch");
                Destroy(m, 10f);
                missileUIScript.UpdateCount(-1);

        }
    }
}