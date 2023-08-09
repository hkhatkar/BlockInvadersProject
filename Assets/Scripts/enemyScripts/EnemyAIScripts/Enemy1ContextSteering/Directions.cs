using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions
{//static class that can be accessed by all scripts, for the steering behaviour directions. These vaues do not change
    public static List<Vector2> allDirections = new List<Vector2>{
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
}

