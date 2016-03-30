using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool iniciarContagem=false;
	public static GameManager Instancia = null;



	void Awake(){
		//Estrutura para garantir que o gameobject com os atributos fundamentais do jogo não seja destruído ao trocar de cena
		if (Instancia == null) {
				Instancia = this;
		} else if (Instancia != this) {
				Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	
	void OnLevelWasLoaded(){
		 iniciarContagem=true;
						
	}
				
	
	// Use this for initialization
	void Start () {

		iniciarContagem=false;
		
	}
	
	// Update is called once per frame
	void Update () {

	}



}
