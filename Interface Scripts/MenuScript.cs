using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	
	[HideInInspector]public Canvas menuUI;
	
	public Canvas quitMenu;
	public Canvas settings;
	public Canvas loadingTime;
	public Canvas loadGame;
	
	public Button btnNewGame;
	public Button resumeGame;
	public Button btnLoadGame;
	public Button btnSettings;
	public Button btnCredits;
	public Button btnExit;
	public Button btnBack;
	public Button btnQuit;
	public Button btnCancel;

	public GameObject[] loadGameComponents = new GameObject[3];
	//mapki z load game bo się kurwa zasłaniały
	
	[HideInInspector]public bool escUse;
	[HideInInspector]public bool duringGame;
	[HideInInspector]public bool newGameDisabled = false;
	
	public static int indexOfLang = 0;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public AudioMixerSnapshot pause;
	public AudioMixerSnapshot withoutPause;
	
	private string nextLevel = "Scene01Tutorial";
	public string creditsScene;
	
	public GameObject gameMusic;
	public GameObject creditMovingObj;
	CreditsMovingScript cms;
	RCCCarControllerV2 rcc;
	MenuProfileSaveAndReadScript mps;
	private Button[] helpbUTTONtAB = new Button[5];
	private GameObject resumeButtonObj;

	private GameObject dashBoard;
	private bool dashboardWasLoaded = false;

	public bool isFullScreen = true;
	public Canvas languagePanel;
	public static bool isLanguagePanel = false;

	void Awake ()
	{
		rcc = GameObject.Find ("BrumBrume").GetComponent<RCCCarControllerV2> ();
		//ReloadDash ();
		helpbUTTONtAB [0] = btnNewGame;
		helpbUTTONtAB [1] = btnLoadGame;
		helpbUTTONtAB [2] = btnSettings;
		helpbUTTONtAB [3] = btnCredits;
		helpbUTTONtAB [4] = btnExit;
		languagePanel.enabled = false;
		/*for (int i = 0; i < helpbUTTONtAB.Length; i++) 
		{
			Debug.Log ("hdupencjanehhee" + helpbUTTONtAB [i].name);
		}*/
		mps = GetComponent<MenuProfileSaveAndReadScript> ();
	}
	// Use this for initialization
	void Start ()
	{
		menuUI = (Canvas)GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		settings = settings.GetComponent<Canvas> ();
		loadingTime = loadingTime.GetComponent<Canvas> ();
		//loadGame = loadGame.GetComponent<Canvas> ();
		resumeButtonObj = resumeGame.gameObject;
		escUse = false;
		duringGame = false;


		//cms = creditMovingObj.GetComponent<CreditsMovingScript>();
		
		quitMenu.enabled = false;
		resumeGame.enabled = false;
		settings.enabled = false;
		loadingTime.enabled = false;
		loadGame.enabled = false;
		if (isLanguagePanel == false && languagePanel.enabled == false) {
			languagePanel.enabled = true;
		}
			
		
		Time.timeScale = 0;
		Disable (loadGameComponents);
		
		if (gameMusic == null)
			gameMusic = GameObject.FindWithTag ("music");
		if (gameMusic == null)
			Debug.Log ("GameMusic zostal niezaladowany");

		IsResume (false);
		//helpbUTTONtAB [5] = ;
		//Debug.Log (helpbUTTONtAB [5]);
	}

	public void EnabledDisableButtonsMenu (bool chan)
	{
		for (int i = 1; i < helpbUTTONtAB.Length; i++) {
			if (i != 3)
				helpbUTTONtAB [i].enabled = chan;
		}
	}
	//Update is called once per frame
	void Update ()
	{
		//if (cms.tempBoolCredits == true)
		//escUse = false;
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (escUse == true && duringGame == true)
				menuUI.enabled = !menuUI.enabled;
			if (menuUI.enabled == true) {
				if ((SceneManager.GetActiveScene ().name == "SceneCanvas" || SceneManager.GetActiveScene ().name == "SceneCredits")) {
					IsResume (false);
					Cursor.visible = false;
					//Debug.Log ("zzz Wylaczam resume");
				} else {
					IsResume (true);
					Cursor.visible = true;
					//Debug.Log ("wlaczam resume");
				}
			}
			EnabledDisableButtonsMenu (true);

			if (menuUI.enabled == true) {
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				/*if (Cursor.visible == false) {

				}*/
				Time.timeScale = 0;

				/*quitMenu.enabled = false;
				settings.enabled = false;
				credits.enabled = false;*/
				
				
			} else {
				//rcc.WriteNewValueOfCar (oldValue);
				Time.timeScale = 1;
				Cursor.visible = false;

			}
		}
		

		//setSounds ();
		
	}

	public void IsResume (bool zmienna)
	{
		helpbUTTONtAB [0].enabled = !zmienna;
		helpbUTTONtAB [3].enabled = !zmienna;
		resumeGame.enabled = zmienna;
		resumeButtonObj.SetActive (zmienna);
		//Debug.Log ("DzienDobry");
	}

	public void ResumeGame ()
	{
		EnabledDisableButtonsMenu (false);
		resumeGame.enabled = false;
		menuUI.enabled = false;
		Cursor.visible = false;
		if (SceneManager.GetActiveScene ().name != "SceneCanvas" && SceneManager.GetActiveScene ().name != "SceneCredits") {
			/*if (dashboardWasLoaded == true && dashBoard.activeInHierarchy == false) {
				dashBoard.SetActive (true);
			}*/
		}
		if (Time.timeScale == 0)
			Time.timeScale = 1;
	}

	public void ButtonLoadGame ()
	{
		loadGame.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		btnExit.enabled = false;
		quitMenu.enabled = false;
		duringGame = false;
		Cursor.visible = true;
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
		Enable (loadGameComponents);
	}

	public void ButtonSettings ()
	{
		settings.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		btnExit.enabled = false;
		quitMenu.enabled = false;
		duringGame = false;
		Cursor.visible = true;
		loadGame.enabled = false;
		
		
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
	}

	public void ButtonCredits ()
	{

		Application.LoadLevel (creditsScene);

		quitMenu.enabled = false;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		duringGame = false;
		btnExit.enabled = false;
		Time.timeScale = 1;

		IsResume (false);
		mps.SaveInfo ();
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
	}

	public void ButtonExit ()
	{
		
		
		quitMenu.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		duringGame = false;
		Cursor.visible = true;
		btnExit.enabled = false;
		
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}

		mps.SaveInfo ();

		
	}

	public void ButtonNewGame ()
	{

		if (newGameDisabled == false) {
		
			menuUI.enabled = false;
			loadingTime.enabled = true;
			escUse = true;
			duringGame = true;
			Cursor.visible = false;
			newGameDisabled = true;

			//cms.tempBoolCredits = false;
		
			Application.LoadLevel ("Scene01Tutorial");
		
			Time.timeScale = 1;
		
		
			if (soundSource != null) {
				soundSource.PlayOneShot (clickSound);
			}
		}
		IsResume (true);
	}

	
	public void ButtondoNotExit ()
	{
		settings.enabled = false;
		quitMenu.enabled = false;
		loadGame.enabled = false;
		btnNewGame.enabled = true;
		btnExit.enabled = true;
		btnCredits.enabled = true;
		btnSettings.enabled = true;
		btnLoadGame.enabled = true;
		duringGame = true;
	
		
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
		Disable (loadGameComponents);
		mps.SaveInfo ();

		/*if((SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 6) && helpbUTTONtAB[1].enabled == true) 
		{
			IsResume (false);
		}*/
	}

	public void SetLang (int i)
	{
		MenuScript.indexOfLang = i;
		languagePanel.enabled = false;
		isLanguagePanel = true;
	}

	public void ButtonConfirmExit ()
	{
		Application.Quit ();
		
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
	}

	public void Enable (GameObject[] tab)
	{
		for (int i = 0; i < tab.Length; i++) {
			tab [i].SetActive (true);
		}
	}

	public void Disable (GameObject[] tab)
	{
		for (int i = 0; i < tab.Length; i++) {
			tab [i].SetActive (false);
		}
	}

	public void EnableButtonsAfterExit ()
	{
		for (int i = 0; i < helpbUTTONtAB.Length; i++) {
			if (helpbUTTONtAB [i].enabled == false)
				helpbUTTONtAB [i].enabled = true;
		}
	}
}
