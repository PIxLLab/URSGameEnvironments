using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangerousZoneWarning : MonoBehaviour {

	public Sprite sprite2change;
	GameObject objUI;

	void Awake(){

		objUI = GameObject.Find ("Dangerous");
	}

	void OnTriggerEnter(Collider plyr){
		if (plyr.gameObject.tag == "Player") {
			objUI.GetComponent<Image> ().sprite = sprite2change;
			//gameObject.SetActive(false);
		}

	}
}
