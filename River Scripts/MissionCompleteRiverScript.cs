﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MissionCompleteRiverScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Button quitBtn;
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	MissionRiverScript ms;
	
	
	void Start (){
		
		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		ms = obj.GetComponent<MissionRiverScript> ();
		
		
	}
	
	
	void OnTriggerEnter(Collider other){ 

		if (other.tag == "Player"){
			missionComplete.enabled = true;
			if (ms.y == 7) {
			Time.timeScale = 0;
			}
		}

		if (missionComplete.enabled == true) {
			
			Time.timeScale = 0;
		}
		
	}
	
	public void EnabledMissionComplete (){
		
		missionComplete.enabled = true;
		if (missionComplete.enabled == true) {
			Time.timeScale = 0f;
		}
	} 
	
	public void QuitGame (){
		
		Application.Quit ();
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}

	
	public void NextMission (){
		
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel(nextLevel);
		if (LoadGameScript.unlockIndex == 2)
		LoadGameScript.unlockIndex++;
		Time.timeScale = 1;
		
		
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
}
