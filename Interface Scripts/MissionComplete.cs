using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MissionComplete : MonoBehaviour {

	public Canvas missionComplete; 
	public Button quitBtn;
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject brumBrume;
	//public Button lostClose;
	RCCCarControllerV2 RCC;
	MissionsScript ms;
	
	void Start (){

		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		RCC = brumBrume.GetComponent<RCCCarControllerV2> ();
		ms = brumBrume.GetComponent<MissionsScript> ();

	}


	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			missionComplete.enabled = true;
			if (ms.y == 8) {


				Time.timeScale = 0;

			}
		}
		if (missionComplete.enabled == true) {

			Time.timeScale = 0;
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
		LoadGameScript.unlockIndex++;
		//Time.timeScale = 1;
		
		Cursor.visible = false;
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	
}
