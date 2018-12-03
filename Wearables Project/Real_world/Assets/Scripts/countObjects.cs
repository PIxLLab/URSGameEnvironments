using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countObjects : MonoBehaviour {
	public GameObject objToDestroy;
	GameObject objUI;

	// Use this for initialization
	void Start () {
		objUI = GameObject.Find ("ObjectNum");
	}
	
	// Update is called once per frame
	void Update () {
		objUI.GetComponent<Text> ().text = objectsToCollect.objects.ToString ();
		if (objectsToCollect.objects == 0) {
			objUI.GetComponent<Text> ().color = Color.red;
			//Destroy (objToDestroy);
		}
	}
}
