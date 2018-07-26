using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Battery_Controller : MonoBehaviour {

	
    public Slider battery;
    public Color fullBattery;
    public Color lowBattery;
    public Color halfBattery;
    public Color currentBattery;
    public Image batteryFill;
    public Text ProgressText;
    //private float progress;

    void Update()
	{
        //if (battery.value < 0.3f)
        //{
        //    currentBattery = lowBattery;
        //}
        //else if (battery.value < 0.6f)
        //{
        //    currentBattery = halfBattery;
        //}
        //else
        //{
        //    currentBattery = fullBattery;
        //}

        //batteryFill.color = currentBattery;
        //progress = Mathf.Floor(battery.value);
        //ProgressText.text = progress * 100f + "%";
        //battery.value -= 0.01f * Time.deltaTime;
       

	}
}
