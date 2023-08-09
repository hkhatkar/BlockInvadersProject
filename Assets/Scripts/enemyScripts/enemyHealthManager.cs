using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthManager : MonoBehaviour
{

    private bool canBeHit = true;        
    RoundManager roundScript;   
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] private GameObject coinPrefab;

    public Slider slider;
    public float maxHealth, currentHealth;

    void Start()
    {
        roundScript = (GameObject.FindGameObjectWithTag("roundManager")).GetComponent<RoundManager>();
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
        //start off with assignment of roundScript to roundManager objects script amd assigning current and max health
    }
    public void SetMaxHealth(float health)
    {//set the slider max value and current value to the value in health
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {//sets enemy health to value passed in parameter
        slider.value = health;
    }

    public void takeDamage(float damage)
    {//function that is called by other scripts to take health away and check if health is below or equal to 0
        if (canBeHit)
        {
        slider.value = slider.value - damage;
        StartCoroutine("Hit2");
        
        if (slider.value <= 0)
        {//if value is below 0, we need to increase kill count, instantiate an explosion and coin and set gameobject to inactive
            roundScript.currentKillCount++;

            GameObject currentExplodeP = Instantiate(explosionPrefab, new Vector3((gameObject.transform.position).x, (gameObject.transform.position).y, (gameObject.transform.position).z), Quaternion.identity);
            currentExplodeP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
            (GameObject.FindGameObjectWithTag("coinManager")).GetComponent<ObjPool2D>().ActivateBullet(new Vector3((gameObject.transform.position).x, (gameObject.transform.position).y, 1f), Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("enemy_die");
            gameObject.SetActive(false);
          // DESTROY GAME OBJECTS LATER (in roundManager): prevents null error when bullet collides with object and helps with game performance (object pooling)
        }
        else{FindObjectOfType<AudioManager>().Play("enemy_hurt");}
        }
    }
    IEnumerator Hit2()
    {//flash red when called
        //We cant be hit
        canBeHit = false;
        //Set material color to red
        playerSpriteRenderer.color = Color.red;
        //Wait 0.1 second
        yield return new WaitForSeconds(0.1f);
        //Set material color to white
        playerSpriteRenderer.color = Color.white;
        //We can be hit
        canBeHit = true;
    }
}
