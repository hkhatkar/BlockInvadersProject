using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{//script containing data for the list of targets and obstacles that have been detected
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;

    public Transform currentTarget;
    public int GetTargetsCount() => targets == null ? 0 : targets.Count;
    //if target is null return 0 else targets.Count
}