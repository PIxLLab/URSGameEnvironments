using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class batteryControl : MonoBehaviour {

	public Slider battery;
	public Color fullBattery;
	public Color lowBattery;
	public Color halfBattery;
	public Color currentBattery;
	public Image batteryFill;
	//GameObject objUI;

	void Awake(){
		
		//objUI = GameObject.Find ("Dangerous");

	}

	void Update()
	{
		if (battery.value < 0.3f)
		{
			currentBattery = Color.Lerp(batteryFill.color,lowBattery, Time.deltaTime);
		}
		else if (battery.value < 0.6f)
		{
			currentBattery = Color.Lerp(batteryFill.color,halfBattery, Time.deltaTime);
		}
		else
		{
			currentBattery = Color.Lerp(batteryFill.color,fullBattery, Time.deltaTime);
		}

		batteryFill.color = currentBattery;
	}
}
