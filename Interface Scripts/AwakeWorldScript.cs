using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AwakeWorldScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Canvas gameOver;
	public Image lightOnDB;
	private Color [] lightsColor = new Color[2];
	RCCCarControllerV2 rcc;



	void Start (){

		missionComplete = missionComplete.GetComponent<Canvas>();
		gameOver = gameOver.GetComponent<Canvas> ();
		lightsColor [0] = new Color (0.74f, 0.66f, 0.05f);
		lightsColor [1] = new Color (0.96f, 0.96f, 0.96f);



	}

	void Awake (){

		missionComplete.enabled = false;
		gameOver.enabled = false;

		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.L) && rcc.headLights [0].enabled == false)
			lightOnDB.color = lightsColor [0];
		else if(Input.GetKeyDown (KeyCode.L) && rcc.headLights [0].enabled == true)
			lightOnDB.color = lightsColor [1];
		
	}

}
