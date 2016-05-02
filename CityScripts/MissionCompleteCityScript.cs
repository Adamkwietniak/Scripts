using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MissionCompleteCityScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Button quitBtn;
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	MissionCityScript ms;
	private GameObject loadingObj;
	void Awake ()
	{
		if (loadingObj == null) {
			loadingObj = GameObject.Find ("LOADING");
			Debug.Log ("Załadwałem GameObiect w MissionCmplte: " + loadingObj.name);
		}
	}
	
	void Start (){
		
		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		quitBtn = quitBtn.GetComponent<Button>();
		ms = obj.GetComponent<MissionCityScript> ();
		
		
	}
	
	
	/*void OnTriggerEnter(Collider other){ 

		if (other.tag == "Player"){
			missionComplete.enabled = true;
			if (ms.y == 7) { // wpisujemy wartosc messega po ktorym ma sie pojawic mission complete
			Time.timeScale = 0;
			}
		}

		if (missionComplete.enabled == true) {
			
			Time.timeScale = 0;
		}
		
	}
	*/
	public void EnabledMissionComplete (){
		
		missionComplete.enabled = true;
		if (missionComplete.enabled == true) {
			Time.timeScale = 0f;
		}
	} 
	
	public void QuitGame (){
		
		Application.LoadLevel ("SceneCanvas");
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}

	
	public void NextMission (){
		Canvas cLoad = loadingObj.GetComponent<Canvas> ();
		if (loadingObj.activeInHierarchy == true && cLoad.enabled == false) {
			cLoad.enabled = true;

			GameObject go = GameObject.Find ("DashboardOnScreen");
			if (go.activeInHierarchy == true)
				go.SetActive (false);
		}
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel(nextLevel);
		if (LoadGameScript.unlockIndex == 3)
		LoadGameScript.unlockIndex++;
		Time.timeScale = 1;
		
		
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
}
