using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : MonoBehaviour
{//abstract class Detect takes aiData as a parameter
    public abstract void Detect(AIData aiData);
}