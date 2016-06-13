﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AwakeWorldScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Canvas gameOver;
	public Image lightOnDB;
	private Color [] lightsColor = new Color[2];
	RCCCarControllerV2 rcc;
	MenuScript mns;



	void Start ()
	{

		lightsColor [0] = new Color (0.74f, 0.66f, 0.05f);
		lightsColor [1] = new Color (0.96f, 0.96f, 0.96f);
		//Debug.Log (rcc.LightIsTunnOn ());;

	}

	void Awake ()
	{
		
		missionComplete.enabled = false;
		gameOver.enabled = false;
		mns = (MenuScript)FindObjectOfType (typeof(MenuScript)) as MenuScript;
		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;
		mns.escUse = true;


	}
	void LateUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.L)) {
			if (rcc.LightIsTunnOn ()) {
				lightOnDB.color = lightsColor [0];
			} else {
				lightOnDB.color = lightsColor [1];
			}
		}
	}

}
