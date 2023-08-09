using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerPowerBar : MonoBehaviour
{
    private Image content;
    public float MyMaxPowerValue { get; set; }
    public float currentValue;
    private float currentFill;
    UseShield shieldScript;

    public float MyCurrentValue
    {//This property is responsible for setting players max shied power and varying current power
        get
        {//Return the current shield value
            return currentValue;
        }
        set
        {//used to set the current value between clamped values of 0 and MyMaxPowerValue
            if (value >= MyMaxPowerValue){currentValue = MyMaxPowerValue;  }
            //makes sure current value doesnt go above max value
            else if (value <= 0)
            {//if value is less or equal to 0 then start cooldown, to recharge bar
                currentValue = 0;
                StartCoroutine(coolDownTimer());
            }
            else {currentValue = value;}
            currentFill = currentValue / MyMaxPowerValue;
        }
    }
    private void Start()
    {
        shieldScript = GameObject.FindGameObjectWithTag("Player").GetComponent<UseShield>();
        StartCoroutine(RegeneratePower());
        content = GetComponent<Image>();
        Initialize(200f, 200f);
    }
    void Update()
    {
        content.fillAmount = currentFill;
        //The bar will be constantly refilled as it changes
    }
    public void Initialize(float currentValue, float maxValue)
    {//this is the INITIAL values. These can be changed later e.g. when getting power ups from the shop script
        MyMaxPowerValue = maxValue;
        MyCurrentValue = currentValue;
        //initializes the current value and max value to be displayed in UI
    }

    IEnumerator RegeneratePower()
    {
        while (true) //loops forever
        {
            if (Input.GetKey(KeyCode.Mouse1) && shieldScript.disabledShield == false)
            {//if shielding then decrease current bar value at a constant rate
                MyCurrentValue--;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {//else increase current bar value at a constant rate
                MyCurrentValue++;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    public IEnumerator coolDownTimer()
    {
        yield return new WaitForSeconds(5f);
       // playerscript.SprintCoolDown = false;
    }
}
