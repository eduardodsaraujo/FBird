using UnityEngine;
using System.Collections;
using System;


public class BirdMovement : MonoBehaviour {

		Vector3 velocity = Vector3.zero;
		public float flapSpeed    = 100f;
		public float forwardSpeed = 1f;

		bool didFlap = false;
		bool isMobile = false;
		Animator animator;

		public bool dead = false;
		float deathCooldown;

		public bool godMode = false;

		GameObject time;
		bool fimTempo = false;

		DateTime dateTime;
		public bool contarTempo = false;

		public bool pauseBirdActive;
		public int countColision = 0;
		public bool canCountColision;

		GameObject startScreen;



		// Use this for initialization
		void Start () {
			pauseBirdActive = false;
			animator = transform.GetComponentInChildren<Animator>();
			
			if(animator == null) {
				Debug.LogError("Didn't find animator!");
			}

			time = GameObject.FindGameObjectWithTag("Time");
				startScreen = GameObject.FindGameObjectWithTag("StartScreen");
			dateTime = new DateTime (1,1,1,1,1,0);
			time.GetComponent<GUIText>().text = String.Format ("{0:mm:ss}", dateTime);
			
			countColision = 0;
			canCountColision = false;
	
		}

		// Do Graphic & Input updates here
		void Update() {

			
			if(dead) {

				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
								StartCoroutine(PlayAgain());
				}
			}
			else {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
					didFlap = true;
				}
				if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Began) {
							didFlap = true;

						} 
				}
			}

			if (time.GetComponent<GUIText> ().text == "00:00") {
					fimTempo = true;
			}

			if (fimTempo) {
					GameManager.Instancia.Log("Tempo Esgotado \t Velocidade: " + GetComponent<ChangeDifficulty>().birdVelocity + 
								"\tEspaçamento: " + GetComponent<ChangeDifficulty>().spacing + "\tDistancia: " + Score.score);
						
					animator.SetTrigger ("Death");
					dead = true;
			}
			if (GameManager.Instancia.iniciarContagem == true) {
					GameManager.Instancia.iniciarContagem = false;
					StartCoroutine (esperarSegundo ());
			}



		}

	
		// Do physics engine updates here
		void FixedUpdate () {

			if(dead)
				return;

			GetComponent<Rigidbody2D>().AddForce( Vector2.right * forwardSpeed );

			if(didFlap) {
						if (!isMobile) {
								GetComponent<Rigidbody2D> ().AddForce (Vector2.up * flapSpeed);
						} else {
								GetComponent<Rigidbody2D> ().AddForce (Vector2.up * flapSpeed* 0.3f);

						}
				animator.SetTrigger("DoFlap");

						GameManager.Instancia.Log("Pulou \t Velocidade: " + GetComponent<ChangeDifficulty>().birdVelocity + 
								"\tEspaçamento: " + GetComponent<ChangeDifficulty>().spacing + "\tDistancia: " + Score.score);
						
				didFlap = false;
			}

			if(GetComponent<Rigidbody2D>().velocity.y > 0) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else {
				float angle = Mathf.Lerp (0, -90, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
				transform.rotation = Quaternion.Euler(0, 0, angle);
			}
		}

		void OnCollisionEnter2D(Collision2D collision) {
			if(godMode)
				return;
//			animator.SetTrigger ("Death");
//			dead = true;
//			deathCooldown = 0.5f;
			
			//Score.SubPoint();

			
			GameManager.Instancia.Log("Colidiu \t Velocidade: " + GetComponent<ChangeDifficulty>().birdVelocity + 
						"\tEspaçamento: " + GetComponent<ChangeDifficulty>().spacing + "\tDistancia: " + Score.score);
			

			StartCoroutine (pauseBird());
			StartCoroutine (resetCountColision ());

		}

		IEnumerator resetCountColision (){
			if (canCountColision == false) {
					canCountColision = true;
					yield return new WaitForSeconds (10);
					countColision = 0;
					canCountColision = false;
			}
		}

		IEnumerator pauseBird(){
				if (pauseBirdActive == false) {
						pauseBirdActive = true;
						countColision++;

						GetComponent<ChangeDifficulty> ().birdVelocity = 0.0f;
						GetComponent<BirdMovement> ().flapSpeed = 0.0f;
						GetComponentInChildren<SpriteRenderer> ().color = new Color (255F, 255F, 255F, 0.3F);
						float alpha = 0.3f;
						GetComponent<Rigidbody2D> ().mass = 20f;
						yield return new WaitForSeconds (2f);



						while (alpha < 0.5f) {
								GetComponentInChildren<SpriteRenderer> ().color = new Color (255F, 255F, 255F, alpha);
								alpha += 0.01f;
								yield return new WaitForSeconds (0.01f);
						}
						GetComponent<Rigidbody2D> ().mass = 1;

						GetComponent<BirdMovement> ().flapSpeed = 175f;
						GetComponent<ChangeDifficulty> ().birdVelocity = 0.6f;
						yield return new WaitForSeconds (0.6f);
						GetComponent<ChangeDifficulty> ().birdVelocity = 1.2f;
						alpha = 0.3f;
						while (alpha < 1f) {
								yield return new WaitForSeconds (0.6f);
								GetComponentInChildren<SpriteRenderer> ().color = new Color (255F, 255F, 255F, alpha);
								alpha += 0.1f;
						}
						//imunidade
						pauseBirdActive = false;

				}

		}

		public IEnumerator esperarSegundo(){
				contarTempo = true;
				GameManager.Instancia.Log("Iniciou \t Velocidade: " + GetComponent<ChangeDifficulty>().birdVelocity + 
						"\tEspaçamento: " + GetComponent<ChangeDifficulty>().spacing + "\tDistancia: " + Score.score);

				int i= dateTime.Second + dateTime.Minute*60;
				while (i != 0) {

						yield return new WaitForSeconds (1);
						dateTime = dateTime.AddSeconds (-1);
						time.GetComponent<GUIText>().text = String.Format ("{0:mm:ss}", dateTime);
						if (i <= 10) {
								time.GetComponent<GUIText> ().color = Color.red;
						}
						i--;


				}

		}

		public IEnumerator PlayAgain(){
				yield return new WaitForSeconds (2.5f);
				Application.LoadLevel (Application.loadedLevel);
		}

}
