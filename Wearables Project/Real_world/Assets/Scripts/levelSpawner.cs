using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSpawner : MonoBehaviour {

	public string newLevel;
	public float timeBetweenLevel = 120f; 
	private float countdown = 60f;
	public Text Timer;
	public GameObject GameStatsUI;
	public Text Progress;
	public Image batteryBar;
	public Slider battery;
	public float progress;

	void Awake(){
		Debug.Log ("New Level");
		GameStatsUI.SetActive(true);
		batteryBar.fillAmount = 1.0f;
		battery.value = 1.0f;
	}

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
		Timer.text = minutes + ":" + Mathf.RoundToInt(seconds);

		batteryBar.fillAmount -= 0.01f * Time.deltaTime;

		battery.value -= 0.01f * Time.deltaTime;
		progress = battery.value * 100f;
		progress = Mathf.Clamp(progress, 0f, 100f);
		Progress.text = Mathf.Floor(progress) + "%";
		////timerText.text = string.Format("{00:00.00}", countdown);
	}

	void NewLevel()
	{
		objectsToCollect.objects = 0;
		//Application.LoadLevel (newLevel);
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);


	}

}
