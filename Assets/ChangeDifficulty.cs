using UnityEngine;
using System.Collections;
using System.Net;
using System;



public class ChangeDifficulty : MonoBehaviour {
		private GameObject[] pipesFirst;
		private GameObject cameraBird;
		private GameObject bird;
		private float cameraSize;
		public float birdVelocity;
		public float spacing;
		public GameObject showValues;
		public bool changeSpace;

		// Use this for initialization
		void Start () {
				changeSpace = false;
				pipesFirst = GameObject.FindGameObjectsWithTag("PipeFirst");
				cameraBird = GameObject.FindGameObjectWithTag("MainCamera");
				bird = GameObject.FindGameObjectWithTag("Player");

				spacing = 1.43f;
				cameraSize = cameraBird.GetComponent<Camera> ().orthographicSize;
				birdVelocity = bird.GetComponent<BirdMovement> ().forwardSpeed;
		}

		// Update is called once per frame
		void Update () {
				showValues.GetComponent<GUIText>().text = "Vel: " + birdVelocity + "\n" + "Space: " + spacing;
				//StartCoroutine (changeDifficulty());
				alterarDificuldade ();
				bird.GetComponent<BirdMovement> ().forwardSpeed = birdVelocity;
		}

		void alterarDificuldade(){
			foreach (GameObject pipeFirst in pipesFirst) {	
					//print ("size pipe:"+pipeFirst.GetComponent<SpriteRenderer> ().bounds.size.x);
					if ((cameraBird.transform.position.x - cameraSize - 1.2f > pipeFirst.transform.position.x) ||
					    (pipeFirst.transform.position.x > cameraBird.transform.position.x + cameraSize + 1.2f)) {
							Vector3 pos2 = pipeFirst.transform.localPosition;
							pos2.y = spacing;
							pipeFirst.transform.localPosition = pos2;
					}
			}
		}
		IEnumerator changeDifficulty(){
			if (changeSpace == false) {
					changeSpace = true;
						while (birdVelocity <= 6f) {
								yield return new WaitForSeconds(6);
								birdVelocity += 0.5f;
						}
						birdVelocity = 1.2f;
						spacing -= 0.2f;
					changeSpace = false;
			}

		}

}