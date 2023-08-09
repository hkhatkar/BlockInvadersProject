using UnityEngine;
using System.Collections;

public class Spawn2D : MonoBehaviour {

    public GameObject[] enemys;    //Enemy prefabs

    void Start()
    {
        //Start Spawn
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        //spawn rate
        yield return new WaitForSeconds(Random.Range(8, 15));
        
        //Spawn enemy
       if ((GameObject.FindGameObjectsWithTag("Enemy")).Length < 12)
       { //prevents more than 12 enemies on screen at one time
        GameObject instEnemy = Instantiate(enemys[Random.Range(0,enemys.Length)],
        new Vector3(transform.position.x + Random.Range(-3,3) * 0.2f,Random.Range(-3,3) * 0.2f+ transform.position.y ,1), Quaternion.identity);
        instEnemy.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
        //spawns random enemy in enemys array
        //spawn in local area around spawner
        instEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);
       }

        //repeat Spawn coroutine
        StartCoroutine("Spawn");
    }
}