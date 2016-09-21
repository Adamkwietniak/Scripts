using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MissionCompleteCityScript : MonoBehaviour
{

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
	MenuScript mns;
	public GameObject hudMenu;

	void Awake ()
	{
		if (loadingObj == null) {
			loadingObj = GameObject.Find ("LOADING");
			//Debug.Log ("Załadwałem GameObiect w MissionCmplte: " + loadingObj.name);
		}
	}

	void Start ()
	{
		

		quitBtn = quitBtn.GetComponent<Button> ();
		ms = obj.GetComponent<MissionCityScript> ();
		mns = GameObject.Find ("GoodCanvas").GetComponentInChildren<MenuScript> ();
		
		
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
	public void EnabledMissionComplete ()
	{
		
		missionComplete.enabled = true;

		if (missionComplete.enabled == true) {
			Time.timeScale = 0f;
			Cursor.visible = true;
			if (Cursor.visible == false) {
				
				Cursor.lockState = CursorLockMode.None;
			}
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

			/*GameObject go = GameObject.Find ("DashboardOnScreen");
			if (go != null)
				go.SetActive (false);*/
		}
		missionComplete.enabled = false;
		MenuInstanceScript.respawnPlace = respawnPlace;
		MenuInstanceScript.respawn = true;
		Application.LoadLevel (nextLevel);
		if (LoadGameScript.unlockIndex == 2)
			LoadGameScript.unlockIndex++;
		Time.timeScale = 1;
		
		
		
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
	}
}
