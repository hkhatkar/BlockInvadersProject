using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool2D : MonoBehaviour
{//pooling for enemies, enemy bullets, player bullets and coins (added to each)
    public GameObject objectToPoolPrefab;
    public bool isEnemyBullet;
    public int no_pool;
    GameObject [] poolOfObjects;
    [SerializeField] bool isCoin;


    void Awake () {
        //create instances of objects and if they need to be destroyed each round, they are placed in the gameobject with tag "destroyManager"
        poolOfObjects = new GameObject[no_pool];
        for (int i =0 ; i< no_pool; i++){
           poolOfObjects[i] = Instantiate (objectToPoolPrefab) as GameObject;
           if (poolOfObjects[i].tag == "enemyBullet" ||  poolOfObjects[i].tag == "Enemy"){
                poolOfObjects[i].transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
            }
           poolOfObjects[i].SetActive(false);
           
        }
    }

    public void ActivateBullet(Vector3 pos, Quaternion rot){
        //activates the next available object that has yet to be activated 
        //and sets the position and rotation at the transform of the gameobject with the script that activated it
        for (int i =0 ; i< no_pool; i++){
            if ( poolOfObjects[i].activeSelf == false){
                poolOfObjects[i].SetActive(true);
                poolOfObjects[i].transform.position = pos;
                poolOfObjects[i].transform.rotation = rot;

                //sets the bullets back to inactive using the DestroyGo function after a set amount of time
                if (isCoin == false)
                {//we dont need to activate these functions for coins
                    if (isEnemyBullet == false)
                    {
                        poolOfObjects[i].GetComponent<bullet2D>().StartCoroutine("DestroyGo");
                    }
                    else if (isEnemyBullet == true){
                        poolOfObjects[i].GetComponent<enemyBullet>().StartCoroutine("DestroyGo");
                    }
                }
                else{
                    poolOfObjects[i].GetComponent<coinBehaviour>().StartCoroutine("DestroyGo");
                }

                break;
            }
        }
    }
}
