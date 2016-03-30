using UnityEngine;
using System.Collections;
using System.Timers;
using System;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	public static Score instance;

	static public void AddPoint() {
		if(instance.bird.dead)
			return;

		score++;

		if(score > highScore) {
			highScore = score;
		}
	}
		static public void SubPoint() {
			if (instance.bird.dead)
					return;
					score = score - 1;
		}

	

	BirdMovement bird;

	void Start() {
		instance = this;

		GameObject player_go = GameObject.FindGameObjectWithTag("Player");

		if(player_go == null) {
			Debug.LogError("Could not find an object with tag 'Player'.");
		}

		bird = player_go.GetComponent<BirdMovement>();
		score = 0;
		highScore = PlayerPrefs.GetInt("highScore", 0);
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt("highScore", highScore);
	}

	void Update () {
		GetComponent<GUIText>().text = "Distance: " + score +" M"+"\nRecord: " + highScore + " M";
	}
}
