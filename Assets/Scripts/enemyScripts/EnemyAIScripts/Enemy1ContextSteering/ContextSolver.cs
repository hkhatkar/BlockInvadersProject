using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    //gizmo parameters used for debugging
    [SerializeField]
    private bool showGizmos = true;
    float[] interestGizmo = new float[0]; 
    private float gizmoDirectionLength = 2;
    //direction the enemy should travel
    public Vector2 resultDirection = Vector2.zero;

    private void Start()
    {
        interestGizmo = new float[8];
    }

    public Vector2 GetDirectionToMove(List<steeringBehaviour> behaviours, AIData aiData)
    {//called from the EnemyAI script to get the Vector to move in

        float[] danger = new float[8];
        float[] interest = new float[8];
        //danger represents how undesirable a direction is with 8 different directions
        //interest represents how desirable a direction is with 8 different directions

        foreach (steeringBehaviour behaviour in behaviours)
        {//loops through each function of the steering behaviour scripts (seekBehaviour and ObstacleAvoidanceBehaviour) and a tuple is returned 
            (danger, interest) = behaviour.GetCalculatedSteering(danger, interest, aiData);
        }
    
        for (int i = 0; i < 8; i++)
        {//loops through and subtract each danger values from corresponding direction for interest 
            interest[i] = Mathf.Clamp(interest[i] - danger[i], 0, 1);
            //range between 0 and 1
        }
        interestGizmo = interest;
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {  //Assigns the outputDirection to the interest magnitudes multiplied by the movement vectors in the Direction class. Which determines what direction to move
            outputDirection += Directions.allDirections[i] * interest[i];
            
        }
        outputDirection.Normalize();
        //normalizing the outputDirection makes the resultant vector = 1 and therefore only move in that direction
        resultDirection = outputDirection;
        return resultDirection;
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showGizmos)
        {//debug displays Vector direction to travel
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * gizmoDirectionLength);
        }
    }
}