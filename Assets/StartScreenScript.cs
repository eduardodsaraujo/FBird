using UnityEngine;
using System.Collections;
using System;

public class StartScreenScript : MonoBehaviour {

	static bool sawOnce = false;
	DateTime dateTime;
	GameObject time;

	// Use this for initialization
	void Start () {

		if(!sawOnce) {
			GetComponent<SpriteRenderer>().enabled = true;
			Time.timeScale = 0;
		}


		sawOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)|| Input.touchCount>0))  {
			Time.timeScale = 1;
			GetComponent<SpriteRenderer>().enabled = false;
			GameManager.Instancia.iniciarContagem =true;
		}
	}


}
