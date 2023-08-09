using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetDetector : Detector
{
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;
    //check player visible to enemy thats why need both
    [SerializeField]
    private bool showGizmos = false;
    //gizmo parameters
    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        //detect player if it is within the detection range
        Collider2D detectedPlayerCol =Physics2D.OverlapCircle(transform.position, detectRange, playerLayerMask);
           
        if (detectedPlayerCol != null)
        {
            //Check if we can directly see the player using a raycast. if there is an obstacle in the way then we cant.
            Vector2 dir = (detectedPlayerCol.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detectRange, obstaclesLayerMask); //obstacle mask will also include player layer as raycast can detect both

            //checks if collider is detected and if that collider layer mask is the same as the layermask assigned 'playerLayerMask'
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {     
                colliders = new List<Transform>() { detectedPlayerCol.transform };
                //Debug.DrawRay(transform.position, dir * detectRange, Color.white);
            }
            else{colliders = null;} //cant see player
        }
        else{colliders = null;}//cant see player

        aiData.targets = colliders;
        //assign the colliders to the AIData script
    }

    private void OnDrawGizmosSelected()
    {//draw line between enemy and player if in range
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, detectRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}