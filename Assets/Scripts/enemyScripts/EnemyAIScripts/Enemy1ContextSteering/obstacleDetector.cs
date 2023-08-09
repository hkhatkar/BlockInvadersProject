using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleDetector : Detector
{
    [SerializeField]
    private float detectionRadius = 2;
    //detect all in this radius
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private bool showGizmos = true;
    Collider2D[] colliders;

   
    public override void Detect(AIData aiData)
    {//get colliders detected in radius and store in aiData.obstacles
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        aiData.obstacles = colliders;
    }

    private void OnDrawGizmos()
    {//draw a sphere on all detected obstacles
        if (showGizmos == false)
            return;
        if (Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in colliders)
            {
                if (obstacleCollider.gameObject != null)//prevent it triggering when object is deleted
                {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
                }
            }
        }
    }
}