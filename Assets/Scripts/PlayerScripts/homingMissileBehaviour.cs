using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMissileBehaviour : MonoBehaviour
{
    //findClosestEnemy
    [SerializeField] GameObject targetting;
    float distanceToEnemy;
    float closestDistance;
    //rotate and move to target
    GameObject[] allEnemies;
 
    void FixedUpdate()
    {
        if (targetting == null || targetting.activeSelf == false)
        {//if our targetting variable is empty then we just want to move in the direction we are facing and call findClosestEnemy()
            targetting = null;
            transform.Translate(new Vector3(0,1,0) * 3f * Time.deltaTime);
            findClosestEnemy();
        }
        else{moveAndRotateToTarget();}
        //otherwise we want to move to the target  
    }

    void findClosestEnemy()
    {//finds closest enemy object
        closestDistance = Mathf.Infinity;
        //we start with infinity distance and replace once we find a lower distance in the for each loop
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {//goes through each enemy game object in scene and works out the magnitude between missile and enemy, replace targetting if this is shorter distance than current
            distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < closestDistance)
            {
                targetting = enemy;
                closestDistance = distanceToEnemy;
            }
            
        }
    }

    void moveAndRotateToTarget()
    {//rotates towards enemy by finding the angle in radians to the target, then coverting it in degrees. use this with AngleAxis to get the quaternion rotation and move to that direction over time with slerp
        transform.position = Vector3.MoveTowards(transform.position, targetting.transform.position, 2f * Time.deltaTime);
         float angle = Mathf.Atan2((targetting.transform.position-transform.position).y, (targetting.transform.position-transform.position).x) * Mathf.Rad2Deg - 90f;
         Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
         transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2f);
    }
}