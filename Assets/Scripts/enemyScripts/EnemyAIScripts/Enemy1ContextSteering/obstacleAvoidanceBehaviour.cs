using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleAvoidanceBehaviour : steeringBehaviour
{
    [SerializeField]private float radius = 2f, colliderSize = 0.6f;
    [SerializeField]private bool showGizmo = true;
    //gizmo parameters
    float[] dangerGizmo = null;

    public override (float[] danger, float[] interest) GetCalculatedSteering(float[] danger, float[] interest, AIData aiData) //Doesnt use interest part of tuple
    {//anonymous type float[] tuple (danger, interest) is returned to ContextSoliver (when called)
    //This function gets the steering magnitudes for each direction based on distance between enemies and obstacles
        foreach (Collider2D col in aiData.obstacles)
        {//for each collider detected
            if (col != null)
            {
                Vector2 direction = col.ClosestPoint(transform.position) - (Vector2)transform.position;
                //returns vector on the obstacles collider that is closest to the enemy and takes away the enemy transform to get the vector distance to that point
                float distance = direction.magnitude;
                //magnitude gets the euclidian distance to the object
               

                //calculate weight based on the distance, if distance is less than or equal to collider size weight = 1 (Max), otherwise return a lower weight
                float weight= distance <= colliderSize? 1: (radius - distance) / radius;
                Vector2 normalizedDistance = direction.normalized;

            //calculates dot product of distance to obstacle to each direction and uses this with weight to override each direction in danger[i] value if the value is higher 
            for (int i = 0; i < Directions.allDirections.Count; i++)
            {
                float result = Vector2.Dot(normalizedDistance, Directions.allDirections[i]);
                float val = result * weight;
                if (val > danger[i]){danger[i] = val;}
            }
            }
        }
        dangerGizmo = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {//draws rays in 8 directions representing by length how strong each danger value is
        if (showGizmo == false)
            return;

        if (Application.isPlaying && dangerGizmo != null)
        {
            if  (dangerGizmo != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangerGizmo.Length; i++)
                {
                    Gizmos.DrawRay(
                        transform.position,
                        Directions.allDirections[i] * dangerGizmo[i] * 2
                        );
                }
            }
        }

    }
}

