using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableStartRoundAnim : MonoBehaviour
{//simple script with a function that disables the StartRound animation, used at the end of the animation and is called with an animation event
    void disable(){ gameObject.SetActive(false);}
}
