using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class seekBehaviour : steeringBehaviour
{
    [SerializeField]
    private float targetReachedThreshold = 0.5f;
    [SerializeField]
    private bool showGizmo = true;
    bool lastTarget = true;

    //gizmo parameters
    private Vector2 targetLastSeenPos;
    private float[] interestGizmo;

    public override (float[] danger, float[] interest) GetCalculatedSteering(float[] danger, float[] interest, AIData aiData)
    {// returns (danger, interest) float[] tuple

        //if we don't have a target stop detecting, otherwise get the closest target that has been detected
        if (lastTarget)
        {
            if (aiData.targets == null || aiData.targets.Count <= 0)
            {//if there are no targets detected then the currentTarget is set to null
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {//otherwise we havent reached the last target and we want to set the current target equal to the first element of the rodered targets list (ordered by the closest distance)
                lastTarget = false;
                aiData.currentTarget = aiData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        //remember the last detected position of the player by setting the targetLastSeenPos variable to that position
        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
            targetLastSeenPos = aiData.currentTarget.position;

        // check if we have reached the the last seen position and still not found the player then we set last target to true to stop seeking 
        if (Vector2.Distance(transform.position, targetLastSeenPos) < targetReachedThreshold)
        {
            lastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        //set direction to target equal to the direction to the last seen position
        Vector2 directionToTarget = (targetLastSeenPos - (Vector2)transform.position);

        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.allDirections[i]);

            //accept only directions at the less than 90 degrees to the target direction
            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestGizmo = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {//debug for rays in each direction representing by length interest to move in that direction

        if (showGizmo == false)
            return;
        Gizmos.DrawSphere(targetLastSeenPos, 0.2f);

        if (Application.isPlaying && interestGizmo != null)
        {
            if (interestGizmo != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestGizmo.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.allDirections[i] * interestGizmo[i] * 2);
                }
                if (lastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetLastSeenPos, 0.1f);
                }
            }
        }
    }
}