using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]int rotationDir;
    [SerializeField]private List<steeringBehaviour> steeringBehaviours;
    [SerializeField]private List<Detector> detectors;
    [SerializeField]private AIData aiDataScript;
    [SerializeField]private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f;//Delays runs ai less often, for performace purpouses
    [SerializeField]public Vector2 movementInput;
    [SerializeField]private ContextSolver contextSolverScript;
    [SerializeField]private GameObject foundParticle, lostParticle;
    [SerializeField] enemyAimAndShoot aimAndShootScript;
    public bool following = false;
    //public as used in aim/shoot script to switch between rotation types
    bool lostPlayer = false;


    private void Start()
    {
        //Detecting Player and Obstacles around
        StartCoroutine(triggerAllDetectors());
    }

    IEnumerator triggerAllDetectors()
    {//triggers all detectors every 'detectionDelay' seconds
        while (true){
            yield return new WaitForSeconds(detectionDelay);
            foreach (Detector detector in detectors){detector.Detect(aiDataScript);}
        }
    }

    private void Update()
    {
        //Enemy AI movement based on Target availability
        if (aiDataScript.currentTarget != null)
        {
            if (following == false)
            {
                if (lostPlayer == true){
                    aimAndShootScript.enabled = true;
                    lostPlayer= false; 
                    GameObject currentFoundP = Instantiate(foundParticle, transform);
                    currentFoundP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
                }
                following = true;
                StartCoroutine(Chase());
            }
        }
        else if (aiDataScript.GetTargetsCount() > 0){aiDataScript.currentTarget = aiDataScript.targets[0];}
        //if there are more than 1 targets to follow, we want the first detected target in the array to be the currentTarget
 
        if (following == true) {transform.position += new Vector3(movementInput.x * Time.deltaTime, movementInput.y * Time.deltaTime, 0* Time.deltaTime);}
        else {rotateAroundTarget();}
    }

    private IEnumerator Chase()
    {
        if (aiDataScript.currentTarget == null)
        {//Stop Chase coroutine if the enemy can no longer detect any targets
            Debug.Log("Stopping");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiDataScript.currentTarget.position, transform.position);
            //detects distance between target and enemy

            if (distance < 2f)
            {//Enemy will circle around the player if it gets close enough, by setting following to false therefore triggering rotateAroundTarget in update
                movementInput = Vector2.zero;
                following = false;
                rotateAroundTarget();
            }
            else
            {//Move based on the context steering AI. It uses the ContextSolver script to return the direction it should move
                movementInput = contextSolverScript.GetDirectionToMove(steeringBehaviours, aiDataScript);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(Chase());
            }

        }

    }

    void rotateAroundTarget()
    {//simple movement AI that circles the enemy when close enough
        if (aiDataScript.currentTarget != null)
        {
            if (Vector2.Distance(gameObject.transform.position, aiDataScript.currentTarget.position) < 5f )
            {
          
                transform.RotateAround(aiDataScript.currentTarget.position, new Vector3(0f,0f,1f), rotationDir * Time.deltaTime);
                transform.RotateAround(transform.position, new Vector3(0f,0f,-1f), rotationDir * Time.deltaTime);
            }
            
        }
        else
        { //if neither close enough to rotate around enemy or follow the player then the enemy is lost and stops all behaviour
            if (lostPlayer == false)
            {
                GameObject currentLostP = Instantiate(lostParticle, transform);
                currentLostP.transform.parent = (GameObject.FindGameObjectWithTag("destroyManager")).transform;
                lostPlayer = true;
                aimAndShootScript.enabled = false;

            }             
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Obstacle" )
        {//in rotate mode, the enemy will switch direction if it collides with an obstacle or enemy
          rotationDir = -rotationDir;
        } 
    }
}