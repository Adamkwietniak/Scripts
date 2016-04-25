using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AwakeWorldScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Canvas gameOver;



	void Start (){

		missionComplete = missionComplete.GetComponent<Canvas>();
		gameOver = gameOver.GetComponent<Canvas> ();

	}

	void Awake (){

		missionComplete.enabled = false;
		gameOver.enabled = false;



	}

}
