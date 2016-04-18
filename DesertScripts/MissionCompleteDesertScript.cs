using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionCompleteDesertScript : MonoBehaviour {

	public Canvas missionComplete; 
	public Button nextMissionBtn;
	public Canvas loadingTime;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	MissionDesertScript mds;

	// Use this for initialization
	void Start () {

		missionComplete = missionComplete.GetComponent<Canvas>();
		nextMissionBtn = nextMissionBtn.GetComponent<Button> ();
		loadingTime = loadingTime.GetComponent<Canvas> ();
		mds = (MissionDesertScript)FindObjectOfType(typeof(MissionDesertScript)) as MissionDesertScript;
	
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

		Application.Quit ();

		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	

	public void NextMission (){

		missionComplete.enabled = false;
		loadingTime.enabled = true;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel(nextLevel);

		Time.timeScale = 1;

		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}


}
