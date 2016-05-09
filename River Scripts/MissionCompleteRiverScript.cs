using UnityEngine;
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
	private GameObject loadingObj;
	MenuScript mns;
	public GameObject hudMenu;

	void Awake ()
	{
		if (loadingObj == null) {
			loadingObj = GameObject.Find ("LOADING");
			//Debug.Log ("Załadwałem GameObiect w MissionRiver: " + loadingObj.name);
		}
	}
	
	void Start (){
		
		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		ms = obj.GetComponent<MissionRiverScript> ();
		mns = GameObject.Find ("GoodCanvas").GetComponentInChildren<MenuScript> ();
		
		
	}
	
	
	void OnTriggerEnter(Collider other){ 

		if (other.tag == "Player"){
			missionComplete.enabled = true;
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
		
		Application.LoadLevel ("SceneCanvas");
		if (mns.menuUI.enabled == false) 
		{
			mns.menuUI.enabled = true;
		}
		mns.IsResume (false);
		mns.escUse = false;

		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}

	
	public void NextMission (){

		Canvas cLoad = loadingObj.GetComponent<Canvas> ();
		if (loadingObj.activeInHierarchy == true && cLoad.enabled == false) {
			cLoad.enabled = true;

			hudMenu.SetActive (false);
		}
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
