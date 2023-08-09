using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2D : MonoBehaviour {

	//public Transform[] bulletSpawnPos;
	public GameObject bullet;			//Bullet prefab
	public Transform bulletTransform;

	public GameObject explosion;		//Explosion prefab
	public int hp = 3;					//Health public accessed by shop
	private int score;					//Score
	private float tmpFireTime;			//Tmp fire time
	private bool dead;					//Are we dead
	private bool canBeHit = true;		//Can we be hit
	public Canvas GOCanvas;
	private Rigidbody2D rb;

	private Camera cam;					//Camera
	private Vector3 mousePos;			//Position
	[SerializeField] private GameObject blockedParticle;
	UseShield shieldScript;

	public Quaternion bulletRot;//bullet rotation to be fired in, used in shootMissile script also (public)


	void Start ()
	{
	//Set screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Grab RB2D comp
		rb = GetComponent<Rigidbody2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		shieldScript = gameObject.GetComponent<UseShield>();
	}
	
	void FixedUpdate()
	{//fixed to ensure that rigid body force acts on a fixed time and accelaration doesnt fluctuate

		//If we are not dead
		if (!dead)
		{
			//Update
			MoveUpdate();
			ShotUpdate();
		}
	}
	
	void MoveUpdate()
	{
		//On WASD input, inact a force in given direction
        if (Input.GetKey(KeyCode.A)) {
        	rb.AddForce(Vector3.left * 10f);
		}
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right* 10f);
		}
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.up* 10f);
		}
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector3.down* 10f);
		}
}

	void ShotUpdate() {
		//Get mouse position relative to camera
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		//Create V3 for the rotation
		Vector3 rotation = mousePos - transform.position;
		float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

		//Rotate the ship on the new angle (and set one for the bullet trajectory)
		transform.rotation = Quaternion.Euler(0, 0, rotZ);
		bulletRot = Quaternion.Euler(0, 0, rotZ - 90);

		//Firerate stuff
		if (tmpFireTime > 0.2f) {
			if (Input.GetMouseButton(0)) 
			{//shoot if the cool down time has passed and left click is pressed
					GetComponent<ObjPool2D>().ActivateBullet(transform.position, bulletRot);
					FindObjectOfType<AudioManager>().Play("player_shoot");
					//Set tmpFireTime to 0
					tmpFireTime = 0;
			}
			else {
				//Set tmpFireTime to 0.2
				tmpFireTime = 0.2f;
			}
		} else {
			tmpFireTime += 1 * Time.deltaTime;
		}
	}
	
	IEnumerator Hit()
	{
		//We cant be hit
		canBeHit = false;
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);		
		//Dont show player
		GetComponent<SpriteRenderer>().enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);		
		//Show player
		GetComponent<SpriteRenderer>().enabled = true;
		//We can be hit
		canBeHit = true;
	}

	//return hp
	public int DisplayHP(){
		return hp;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//If we are in a enemy trigger
		if (shieldScript.shieldUp == true)
		{
			if (other.tag == "enemyBullet")
			{
				FindObjectOfType<AudioManager>().Play("blocked");
				other.gameObject.SetActive(false);
				GameObject currentBlockedP = Instantiate(blockedParticle,transform.position,Quaternion.identity);
				currentBlockedP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
			}
		}
		else if (other.tag == "Enemy" || other.tag == "enemyBullet")
		{
			//If we can be hit
			if (canBeHit)
			{
				//Remove 1 from hp
				hp--;
				//If hp is less than 1
				if (hp < 1)
				{
					FindObjectOfType<AudioManager>().Play("player_die");
					//enable game over canvas
					GOCanvas.GetComponent<Canvas>().enabled = true;
					//We are dead
					dead = true;			
					//Dont show player
					GetComponent<SpriteRenderer>().enabled = false;
					//Set collider to false
					GetComponent<CircleCollider2D>().enabled = false;
					GetComponent<BoxCollider2D>().enabled= false;
				}
				//If hp is bigger than 0
				else
				{
					//Start Hit
					StartCoroutine("Hit");
					FindObjectOfType<AudioManager>().Play("player_hurt");
				}
				
				//Instantiate explosion
				GameObject currentExplosionP = Instantiate(explosion,transform.position,Quaternion.identity);
				currentExplosionP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
				//set inactive
				other.gameObject.SetActive(false);
			}
		}
	}
}
