using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class droneBatteryControl : MonoBehaviour {

	public Color fullBattery;
	public Color lowBattery;
	public Color halfBattery;
	public Color currentBattery;
	public Image batteryFill;
	public float battery;

	void Update()
	{
		battery= batteryFill.fillAmount;
		if (battery < 0.3f)
		{
			currentBattery = Color.Lerp(batteryFill.color,lowBattery, Time.deltaTime);
		}
		else if (battery < 0.6f)
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
