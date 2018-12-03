using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsToCollect : MonoBehaviour {

	public static int objects = 0;
	public Sprite sprite1;
	GameObject objUI;
	GameObject objUI4Image;
	GameObject objUI2Color2;
	GameObject objUI2Color3;

	void Awake(){
		objects++;
		objUI = GameObject.Find ("ObjectNum");
		objUI4Image = GameObject.Find ("Dangerous");
		objUI2Color2 = GameObject.Find ("Clues");
		objUI2Color3 = GameObject.Find ("GPS");
	}

	void OnTriggerEnter(Collider plyr){
		if (plyr.gameObject.tag == "Player") {
			objects--;
			objUI.GetComponent<Text> ().text = objects.ToString ();
			objUI.GetComponent<Text> ().color = Color.magenta;
			objUI4Image.GetComponent<Image> ().sprite = sprite1;
			objUI2Color2.GetComponent<Image> ().color = Color.green;
			objUI2Color3.GetComponent<Image> ().color = Color.magenta;
			gameObject.SetActive(false);
		}

	}

}
