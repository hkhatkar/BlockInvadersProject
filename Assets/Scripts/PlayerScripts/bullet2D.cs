using UnityEngine;
using System.Collections;

public class bullet2D : MonoBehaviour {

    public float speed = 1;            //Move speed
    public float destroyTime = 7;    //Time it takes to destroy
    public float damage = 0.5f;        //Damage
    [SerializeField] bool isMissile;
    
    void Start ()
    {
       //Start DestroyGo
        StartCoroutine("DestroyGo");
    }
    
    void Update ()
    {
        //Move bullet
        if (isMissile == false){//missiles use their own script behaviour therefore we don't need this for missiles
        transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);
        }
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        //If we are in a enemy trigger
        if (other.gameObject.tag == "Enemy")
        {//call function takeDamage in others gameobjects enemyHealthManager script
            other.GetComponent<enemyHealthManager>().takeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
    
    IEnumerator DestroyGo()
    {
        //Wait then set inactive after time
        yield return new WaitForSeconds(destroyTime);
        this.gameObject.SetActive(false);
    }
}