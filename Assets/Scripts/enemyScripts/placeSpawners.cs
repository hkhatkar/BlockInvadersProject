using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeSpawners : MonoBehaviour
{
    [SerializeField] GameObject spawnerPrefab;
    
    public void placeSpawnersRandomly()
    {//spawns 3 spawners for each round, left middle and right
        //left
        Instantiate(spawnerPrefab,new Vector3(Random.Range(-7.35f,-4.64f),Random.Range(-3f,3f) ,1), Quaternion.identity);
        //mid
         Instantiate(spawnerPrefab,new Vector3(Random.Range(-1.9f,1.5f),Random.Range(-3f,3f) ,1), Quaternion.identity);
          //right
         Instantiate(spawnerPrefab,new Vector3(Random.Range(4.3f,7.5f),Random.Range(-3f,3f) ,1), Quaternion.identity);

    }
}
