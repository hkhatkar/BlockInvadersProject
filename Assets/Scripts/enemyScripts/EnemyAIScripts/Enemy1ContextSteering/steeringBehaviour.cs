using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class steeringBehaviour : MonoBehaviour
{//abstract class for all steering behaviours, all containing the GetCalculatedSteering function
    public abstract (float[] danger, float[] interest) GetCalculatedSteering(float[] danger, float[] interest, AIData aiData);
}