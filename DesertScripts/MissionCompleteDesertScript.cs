using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionCompleteDesertScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	MissionDesertScript mds;
	private GameObject loadingObj;
	void Awake ()
	{
		if (loadingObj == null) {
			loadingObj = GameObject.Find ("LOADING");
			//Debug.Log ("Załadwałem GameObiect w MissionCmplte: " + loadingObj.name);
		}
	}
	// Use this for initialization
	void Start () {

		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		//mds = (MissionDesertScript)FindObjectOfType(typeof(MissionDesertScript)) as MissionDesertScript;
	
	}

	void OnTriggerEnter(Collider other){ 

		if (other.tag == "Player"){
			missionComplete.enabled = true;
			}


		if (missionComplete.enabled == true) {
			
			Time.timeScale = 0;
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
		if (LoadGameScript.unlockIndex == 4)
			LoadGameScript.unlockIndex++;
		Time.timeScale = 1;

		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}


}
