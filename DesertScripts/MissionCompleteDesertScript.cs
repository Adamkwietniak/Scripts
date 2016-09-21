using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionCompleteDesertScript : MonoBehaviour
{

	public Canvas missionComplete;
	public Button nextMissionBtn;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public string nextLevel;
	public string respawnPlace;
	public GameObject obj;
	MissionDesertScript mds;
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
	// Use this for initialization
	void Start ()
	{


		//mds = (MissionDesertScript)FindObjectOfType(typeof(MissionDesertScript)) as MissionDesertScript;
		mns = GameObject.Find ("GoodCanvas").GetComponentInChildren<MenuScript> ();
	
	}

	void OnTriggerEnter (Collider other)
	{ 

		if (other.tag == "Player") {
			missionComplete.enabled = true;

		}


		if (missionComplete.enabled == true) {
			Cursor.visible = true;
			Time.timeScale = 0;
		}
		
	}

	public void QuitGame ()
	{

		Application.LoadLevel ("SceneCanvas");
		if (mns.menuUI.enabled == false) {
			mns.menuUI.enabled = true;
		}
		mns.newGameDisabled = false;
		mns.IsResume (false);
		mns.escUse = false;

		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
		mns.EnableButtonsAfterExit ();
	}


	public void NextMission ()
	{
		mns.escUse = true;
		Canvas cLoad = loadingObj.GetComponent<Canvas> ();
		if (loadingObj.activeInHierarchy == true && cLoad.enabled == false) {
			cLoad.enabled = true;

			hudMenu.SetActive (false);
		}
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel (nextLevel);
		if (LoadGameScript.unlockIndex == 3)
			LoadGameScript.unlockIndex++;
		Time.timeScale = 1;

		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
	}


}
