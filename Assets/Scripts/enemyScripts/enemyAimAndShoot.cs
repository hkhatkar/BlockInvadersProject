using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAimAndShoot : MonoBehaviour
{
    EnemyAI enemyAIScript;
    Vector2 goalRotation;
    GameObject playerObj;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] Transform shootStart;
    [SerializeField] float fireRateMin;
    [SerializeField] float fireRateMax;
    float nextFire;
    
    void Start()
    {
        enemyAIScript = gameObject.GetComponentInParent<EnemyAI>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        nextFire = Time.time;
        //Assigns variables at start of script  for the player object and the enemy AI script

    }


    void FixedUpdate()
    {//fixed update is used to work with deltaTime, therefore rotation occurs at a fixed pace
        aimDirection();
        fireBullet();
    }

    private void aimDirection()
    {
        if (enemyAIScript.following == true)
        {
         goalRotation = enemyAIScript.movementInput;
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward: Vector3.forward, upwards: goalRotation), Time.deltaTime/0.5f);
         //rotates towards the current direction the object is travelling in based on the AI behaviour.
         //Look rotation needs to know what is forward and upwards to be able to face the right way with 2D transform
        }
        else{
    
            Vector3 facePlayerVector = playerObj.transform.position - transform.position;
            //calculates vector between 2 points
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward: Vector3.forward, upwards: facePlayerVector), Time.deltaTime/0.5f);
            //uses slerp to rotate to face player
        }
    }

    void fireBullet()
    {
        if (Time.time > nextFire)
        {//if the time passed is more than the time needed to wait until next fire then we can shoot
            //Debug.Log("Shoot");
            GetComponent<ObjPool2D>().ActivateBullet(shootStart.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("enemy_shoot");
            nextFire = Time.time + Random.Range(fireRateMin, fireRateMax);
        }
    }
}
