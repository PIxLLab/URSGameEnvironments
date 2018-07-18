using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour {

	public static int Score;
	public int startScore = 100;

	void Start () {
		Score = startScore;
	}
}
