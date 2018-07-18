using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Spawner : MonoBehaviour {

	//public Transform CluePrefab;
	public float timeBetweenLevel = 120f; 
	private float countdown = 60f;
	public Text timerText;

	void Update()
	{
		if (countdown <= 0f) {
			NewLevel ();
			countdown = timeBetweenLevel;
		}

		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp (countdown, 0f, Mathf.Infinity);
		float minutes = Mathf.Floor(countdown / 60);
		float seconds = countdown%60;
		timerText.text = minutes + ":" + Mathf.RoundToInt (seconds);
		//timerText.text = string.Format("{00:00.00}", countdown);
	}

	void NewLevel()
	{
		Debug.Log ("New Level");
	}

}
