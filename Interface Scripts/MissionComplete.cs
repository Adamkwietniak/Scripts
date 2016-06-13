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
	VolumeHelperScript vhs;
	private GameObject loadingObj;
	MenuScript mns;
	public GameObject hudMenu;
	void Awake ()
	{
		if (loadingObj == null) {
			loadingObj = GameObject.Find ("LOADING");
			//Debug.Log ("Załadwałem GameObiect w MissionCmplte: " + loadingObj.name);
		}
	}
	void Start (){
		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		RCC = brumBrume.GetComponent<RCCCarControllerV2> ();
		ms = brumBrume.GetComponent<MissionsScript> ();
		mns = GameObject.Find ("GoodCanvas").GetComponentInChildren<MenuScript> ();
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

		Application.LoadLevel ("SceneCanvas");
		if (mns.menuUI.enabled == false) 
		{
			mns.menuUI.enabled = true;
		}
		mns.newGameDisabled = false;
		mns.IsResume (false);
		mns.escUse = false;

		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
		mns.EnableButtonsAfterExit ();
	}


	public void NextMission (){
		mns.escUse = true;
		Canvas cLoad = loadingObj.GetComponent<Canvas> ();
		if (loadingObj.activeInHierarchy == true && cLoad.enabled == false) {
			cLoad.enabled = true;

			hudMenu.SetActive (false);
		}
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel(nextLevel);
		//if (LoadGameScript.unlockIndex == 0)
		Time.timeScale = 1;
		
		Cursor.visible = false;
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	
}
