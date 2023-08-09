using UnityEngine;
using System.Collections;
public class enemy2D : MonoBehaviour {
//NOT USED IN OUR PROJECT

    public float speed = 2;          //Move speed
    public int hp = 100;            //Health
    public int score;                //Score
    private GameObject player;       //The player
    private bool canBeHit = true;    //Can we be hit
    [SerializeField] private GameObject explosionPrefab;

    void Start ()
    {
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        //Move enemy
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void Hit(int _damage)
    {
        //If we can be hit
        if (canBeHit)
        {
            //Start Hit2
            StartCoroutine("Hit2");
            //Remove _damage value from hp
            hp -= _damage;
            //If hp is less than 1
            if (hp < 1)
            {

                //Destroy
                Instantiate(explosionPrefab, new Vector3((gameObject.transform.position).x, (gameObject.transform.position).y, (gameObject.transform.position).z), Quaternion.identity);
                Destroy(gameObject);

            }
        }
    }

    IEnumerator Hit2()
    {
        //We cant be hit
        canBeHit = false;

        //Set material color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        //Wait 0.1 second
        yield return new WaitForSeconds(0.1f);

        //Set material color to white
        GetComponent<SpriteRenderer>().color = Color.white;

        //We can be hit
        canBeHit = true;
    }
}