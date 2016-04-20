using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
	[HideInInspector]public Canvas menuUI;
	
	public Canvas quitMenu;
	public Canvas settings;
	public Canvas credits;
	public Canvas loadingTime;
	public Canvas loadGame;
	
	public Button btnNewGame;
	public Button btnLoadGame;
	public Button btnSettings;
	public Button btnCredits;
	public Button btnExit;
	public Button btnBack;
	public Button btnQuit;
	public Button btnCancel;
	
	[HideInInspector]public bool escUse;
	[HideInInspector]public bool duringGame;
	[HideInInspector]public bool newGameDisabled = false;
	
	public static int indexOfLang = 0;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public AudioMixerSnapshot pause;
	public AudioMixerSnapshot withoutPause;
	
	public string nextLevel;
	
	public GameObject gameMusic;
	public GameObject creditMovingObj;
	CreditsMovingScript cms;
	RCCCarControllerV2 rcc;

	void Awake()
	{
		rcc = GameObject.Find("BrumBrume").GetComponent<RCCCarControllerV2>();
	}
	// Use this for initialization
	void Start () {
		menuUI = (Canvas)GetComponent<Canvas>();
		quitMenu = quitMenu.GetComponent<Canvas>();
		settings = settings.GetComponent<Canvas>();
		credits = credits.GetComponent<Canvas>();
		loadingTime = loadingTime.GetComponent<Canvas> ();
		
		btnNewGame = btnNewGame.GetComponent<Button>();
		btnLoadGame = btnLoadGame.GetComponent<Button>();
		btnSettings = btnSettings.GetComponent<Button>();
		btnCredits = btnCredits.GetComponent<Button>();
		btnExit = btnExit.GetComponent<Button>();
		btnBack = btnBack.GetComponent<Button>();
		btnQuit = btnQuit.GetComponent<Button>();
		btnCancel = btnCancel.GetComponent<Button>();
		
		escUse = false;
		duringGame = false;
		//cms = creditMovingObj.GetComponent<CreditsMovingScript>();
		
		quitMenu.enabled = false;
		settings.enabled = false;
		credits.enabled = false;
		loadingTime.enabled = false;
		loadGame.enabled = false;
		
		Time.timeScale = 0;
		
		if(gameMusic == null)
			gameMusic = GameObject.FindWithTag ("music");
		if (gameMusic == null)
			Debug.Log ("GameMusic zostal niezaladowany");
	}
	
	//Update is called once per frame
	void Update () {
		//if (cms.tempBoolCredits == true)
			//escUse = false;
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if(escUse == true && duringGame == true)
				menuUI.enabled = !menuUI.enabled;
			
			
			if (menuUI.enabled == true)
			{

				//oldValue = rcc.actualValue;
				//rcc.WriteNewValueOfCar (0);
				Time.timeScale = 0;

				/*quitMenu.enabled = false;
				settings.enabled = false;
				credits.enabled = false;*/
				
				
			}
			else
			{
				//rcc.WriteNewValueOfCar (oldValue);
				Time.timeScale = 1;
				/*quitMenu.enabled = false;
				settings.enabled = false;
				credits.enabled = false;*/
				
				
				
			}
			
		}
		//setSounds ();
		
	}
	
	
	/*public void setSounds (){
		if (pause != null && withoutPause != null) {
			if (Time.timeScale == 0) {
				pause.TransitionTo (0.01f);
			} else {
				withoutPause.TransitionTo (0.01f);
			}
		}
	}*/
	
	
	
	
	public void ButtonLoadGame()
	{
		loadGame.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		btnExit.enabled = false;
		quitMenu.enabled = false;
		duringGame = false;
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	public void ButtonSettings()
	{
		settings.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		btnExit.enabled = false;
		quitMenu.enabled = false;
		duringGame = false;
		
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	
	public void ButtonCredits()
	{
		
		credits.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		btnExit.enabled = false;
		quitMenu.enabled = false;
		duringGame = false;
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	
	public void ButtonExit()
	{
		
		
		quitMenu.enabled = true;
		btnNewGame.enabled = false;
		btnLoadGame.enabled = false;
		btnSettings.enabled = false;
		btnCredits.enabled = false;
		duringGame = false;
		btnExit.enabled = false;
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
		
	}
	
	public void ButtonNewGame()
	{

		if (newGameDisabled == false) {
		
			menuUI.enabled = false;
			loadingTime.enabled = true;
			escUse = true;
			duringGame = true;
			newGameDisabled = true;

			//cms.tempBoolCredits = false;
		
			Application.LoadLevel (nextLevel);
		
			Time.timeScale = 1;
		
		
			if (soundSource != null) {
				soundSource.PlayOneShot (clickSound);
			}
		}
	}
	
	
	public void ButtondoNotExit()
	{
		settings.enabled = false;
		quitMenu.enabled = false;
		credits.enabled = false;
		btnNewGame.enabled = true;
		btnExit.enabled = true;
		btnCredits.enabled = true;
		btnSettings.enabled = true;
		btnLoadGame.enabled = true;
		duringGame = true;
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	public void SetLang(int i)
	{
		MenuScript.indexOfLang = i;
	}
	public void ButtonConfirmExit()
	{
		Application.Quit();
		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
}
