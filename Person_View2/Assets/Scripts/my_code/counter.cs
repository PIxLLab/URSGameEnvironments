using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour {

    public static float timeLeft =900.0f;

   // public Text text;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime; 
       
        GameObject.Find("counter").GetComponent<Text>().text =  "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft <= 0)
        {
            GameObject.Find("counter").GetComponent<Text>().text =  "Time Left: 0";
            //GameObject.Find("gamestatus").GetComponent<Text>().text = "Game Over";
           
        }
    }
}
