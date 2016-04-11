using UnityEngine;
using System.Collections;
using System.Net;
using System;


public class ScorePoint : MonoBehaviour {
		GameObject[] pipesFirst;
		GameObject[] pipesSecond;
		GameObject[] bgsSky;
		private GameObject cameraBird;
		GameObject guiScore;
		int score;
		private float cameraSize;
		public Sprite[] spritesPipeFirst;
		public Sprite[] spritesPipeSecond;
		public Sprite[] spritesBgSky;


		void Start(){
				pipesFirst = GameObject.FindGameObjectsWithTag("PipeFirst");
				pipesSecond = GameObject.FindGameObjectsWithTag("PipeSecond");
				bgsSky = GameObject.FindGameObjectsWithTag("BgSky");
				guiScore = GameObject.Find ("guiScore");
				cameraBird = GameObject.FindGameObjectWithTag("MainCamera");
				score = Score.score;
				cameraSize = cameraBird.GetComponent<Camera> ().orthographicSize;

		}

		void Update(){
				score = Score.score;

		}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player") {
			Score.AddPoint();
						if (score > 100) {
								changeScene (4);
						} else if (score > 70) {
								changeScene (3);

						} else if (score > 50) {
								changeScene (2);

						} else if (score > 25) {
								changeScene (1);
						}
						else if (score > 10) {
								changeScene (0);
						}

		}



	}

		void changeScene(int sceneType){
				foreach (GameObject pipeFirst in pipesFirst) {	

						if ((cameraBird.transform.position.x - cameraSize - 1.2f > pipeFirst.transform.position.x) ||
								(pipeFirst.transform.position.x > cameraBird.transform.position.x + cameraSize + 1.2f)) {
								pipeFirst.GetComponent<SpriteRenderer> ().sprite = spritesPipeFirst[sceneType];

						}
				}
				foreach (GameObject pipeSecond in pipesSecond) {	

						if ((cameraBird.transform.position.x - cameraSize - 1.2f > pipeSecond.transform.position.x) ||
								(pipeSecond.transform.position.x > cameraBird.transform.position.x + cameraSize + 1.2f)) {
								pipeSecond.GetComponent<SpriteRenderer> ().sprite = spritesPipeSecond[sceneType];

						}
				}
				//								foreach (GameObject bgSky in bgsSky) {	
				//
				//										if ((cameraBird.transform.position.x - cameraSize - 1.54f > bgSky.transform.position.x) ||
				//												(bgSky.transform.position.x > cameraBird.transform.position.x + cameraSize - 1.54f)) {
				//												bgSky.GetComponent<SpriteRenderer> ().color = new Color (0f, 0.34f, 1.0f, 1f);
				//										}
				//								}

		}
}
