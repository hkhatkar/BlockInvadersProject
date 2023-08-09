using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class missileManager : MonoBehaviour
{
    public int missileCount; 
    [SerializeField] public Image[] missileImages;

    //public access to shootMissile script and shop script to update number of missiles in ui
    public void UpdateCount(int modifier)
    {//modifier added to missile count
        missileCount = missileCount + modifier;
        if (missileCount < 0) {missileCount = 0;}
        //makes sure missile count is 0, and doesnt go below that
        if (! (missileCount > missileImages.Length))
        {//if the number of missiles is not equal to the number of images displayed in the ui then update them to be the same
            for (int i=0; i < missileImages.Length; i++)
            {//loops to check which images to enable in the UI
                if (i < missileCount)
                {
                        missileImages[i].enabled = true;
                }
                else
                {
                    missileImages[i].enabled = false;
                }
            }
        
        }
    }
}