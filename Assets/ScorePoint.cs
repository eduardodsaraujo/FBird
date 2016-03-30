using UnityEngine;
using System.Collections;
using System.Net;
using System;


public class ScorePoint : MonoBehaviour {

		void Start(){

		}

		void Update(){
		}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player") {
			Score.AddPoint();

		}

	}
}
