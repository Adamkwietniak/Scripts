﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MissionCompleteForestScript : MonoBehaviour {
	
	public Canvas missionComplete; 
	public Button quitBtn;
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	public string demoEnd;
	public Button demoBtn;
	MissionForestScript ms;
	
	
	void Start (){
		
		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		ms = obj.GetComponent<MissionForestScript> ();

		
	}
	
	
	/*void OnTriggerEnter(Collider other){ 

		if (other.tag == "Player"){
			missionComplete.enabled = true;
			if (ms.y == 5) {
			Debug.Log("onTriggerEnter dziala");
			

			Time.timeScale = 0;
			}
		}

		if (missionComplete.enabled == true) {
			
			Time.timeScale = 0;
		}
		
	}*/

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

	public void DemoButton () {
		
		Application.LoadLevel(demoEnd);
		MenuInstanceScript.respawn = true;
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;

		
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
		if (LoadGameScript.unlockIndex == 1)
		LoadGameScript.unlockIndex++;
		Time.timeScale = 1;
		
	
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	
}
