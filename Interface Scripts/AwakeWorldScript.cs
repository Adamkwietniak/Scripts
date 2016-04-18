using UnityEngine;
using System.Collections;

public class AwakeWorldScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Canvas loadingTime;
	public Canvas gameOver;



	void Start (){

		missionComplete = missionComplete.GetComponent<Canvas>();
		loadingTime = loadingTime.GetComponent<Canvas> ();
		gameOver = gameOver.GetComponent<Canvas> ();


	}

	void Awake (){

		missionComplete.enabled = false;
		loadingTime.enabled = false;
		gameOver.enabled = false;



	}

}
