using UnityEngine;
using System.Collections;
using System.Threading;
using System.IO;
using System;


public class GameManager : MonoBehaviour {
	public bool iniciarContagem=false;
	public static GameManager Instancia = null;
		private static StreamWriter w;
		static Thread producer;
		public bool fecha;
		Thread logThread;
		private GameObject bird;
		FileInfo logFile;
		StreamWriter w1;

	void Awake(){
		//Estrutura para garantir que o gameobject com os atributos fundamentais do jogo não seja destruído ao trocar de cena
		if (Instancia == null) {
				Instancia = this;
		} else if (Instancia != this) {
				Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		//logFile = new FileInfo ("log.txt");
	}
	
	void OnLevelWasLoaded(){
		iniciarContagem=true;
		//if (w == null)
		//	w = logFile.AppendText ();
				if (w1 == null)
						w1 = new StreamWriter("log1.txt",true);
					
		if(bird == null){
				bird = GameObject.FindGameObjectWithTag ("Player");
		}				
	}
				
	
	// Use this for initialization
	void Start () {
				iniciarContagem=false;

				bird = GameObject.FindGameObjectWithTag("Player");
//
				 w1 = new StreamWriter("log1.txt",true);

//				if (w == null) {
//						w1 = logFile.AppendText ();
//				}
//				Log ("Jogo FBird");


	}
	


	public void Log(string logMessage)
	{
			StartCoroutine (AppendLog(logMessage));

	}

	IEnumerator  AppendLog(string log){
				//	using (w = File.AppendText ("log.txt")) {
				logThread = new Thread(o => {
						//while (true) {
						w1.Write ("{0} - {1} ", DateTime.Now.ToLongTimeString (),
								DateTime.Now.ToShortDateString ());
						w1.Write ("  :");
						w1.WriteLine ("  {0}", log);

						print (log);
						print (w1.ToString ());

						//}
				});
				logThread.Start ();
				yield return null;

				//w.Close ();
		}
	
	// Update is called once per frame
	void Update () {
//				if (fecha == true) {
//						w.Close ();
//				}

				if (bird.GetComponent<BirdMovement> ().dead == true) {
						w1.Close ();
				}


	}



}
