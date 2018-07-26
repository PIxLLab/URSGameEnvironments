using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Spawner : MonoBehaviour {

	//public Transform CluePrefab;
	public float timeBetweenLevel = 120f; 
	private float countdown = 5f;
	public Text timerText;
    public GameObject GameStatsUI;
    public Text ProgressText;
    public Image batteryBar;
    public Slider battery;
    public float progress;


	void Update()
	{
		if (countdown <= 0f) {
			NewLevel ();
			countdown = timeBetweenLevel;
		}

		countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        float minutes = Mathf.Floor(countdown / 60);
        float seconds = countdown % 60;
        timerText.text = minutes + ":" + Mathf.RoundToInt(seconds);

        batteryBar.fillAmount -= 0.01f * Time.deltaTime;
	
        battery.value -= 0.01f * Time.deltaTime;
        progress = battery.value * 100f;
        progress = Mathf.Clamp(progress, 0f, 100f);
        ProgressText.text = Mathf.Floor(progress) + "%";
		//timerText.text = string.Format("{00:00.00}", countdown);
	}

	void NewLevel()
	{
		Debug.Log ("New Level");
        GameStatsUI.SetActive(true);
        batteryBar.fillAmount = 1.0f;
        battery.value = 1.0f;
	}

}
