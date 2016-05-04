using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

public class LoadGameScript : MonoBehaviour {

	VolumeAndMusicScript vms;
	MenuScript ms;
	MenuProfileSaveAndReadScript mps;
	ChangeResolutionScript crs;
	private bool [] itCanOpen = new bool[5];
	public static int unlockIndex = 0;
	// Use this for initialization
	public Canvas canvasLoadImage;
	public ImagesToLoadGame[] imagesToLoadGames = new ImagesToLoadGame[5];
	//private ImagesToLoadGame[] activ = new ImagesToLoadGame[5];
	private List<ImagesToLoadGame> activ = new List<ImagesToLoadGame>();
	private Image loadFr;
	private int actualIndex;
	private Canvas GZMissionComplete;

	private string[] nameLvls = {"Scene02Forest", "Scene03River", "Scene04City", "Scene05Desert", "Scene06Snow"};

	void Awake ()
	{
		vms = (VolumeAndMusicScript)FindObjectOfType (typeof(VolumeAndMusicScript)) as VolumeAndMusicScript;
		ms = (MenuScript)FindObjectOfType(typeof(MenuScript)) as MenuScript;
		mps = (MenuProfileSaveAndReadScript)FindObjectOfType (typeof(MenuProfileSaveAndReadScript)) as MenuProfileSaveAndReadScript;
		crs = (ChangeResolutionScript)FindObjectOfType (typeof(ChangeResolutionScript)) as ChangeResolutionScript;
	}
	void Start () {
		unlockIndex = 0;
		actualIndex = unlockIndex;
		loadFr = GameObject.Find ("LoadingFrame").GetComponent<Image> ();
		//canvasLoadImage.enabled = false;
		for (int i = 0; i < imagesToLoadGames.Length; i++) {
			activ.Add(new ImagesToLoadGame (nameLvls [i], imagesToLoadGames [i].picturesInColor,
				imagesToLoadGames [i].picturesWithoutColor, imagesToLoadGames [i].okButton,
				i, imagesToLoadGames[i].okButton.GetComponentInChildren<Text>()));
		}
		for (int i = 0; i < activ.Count; i++) { //wyłączanie obrazków z LoadGame
			activ[i].picturesInColor.enabled = false;
			activ[i].picturesWithoutColor.enabled = false;
			activ [i].okButton.enabled = false;
			activ [i].okButton.image.enabled = false;
			activ [i].tekstInside.enabled = false;
		}
		AssignDefaultValues ();
		PreparationOfImages (unlockIndex);
	}
	void Update ()
	{
		if (actualIndex != unlockIndex) {
			actualIndex = unlockIndex;
			Intate ();
			PreparationOfImages (actualIndex);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			unlockIndex = 0;
		}
		/*if (Input.GetKeyDown (KeyCode.O)) {
			LoadGameScript.unlockIndex ++;
			//Debug.Log ("Unlock index = " + LoadGameScript.unlockIndex);
		}*/
	}
	public void PreparationOfImages (int unlockValiu)	//Przesylamy wartosc, która wskazuje na ilosc odblokowanych scen liczoną od 0
	{
		for(int i = 0; i < activ.Count; i++)
		{
			if (i <= unlockValiu) {
				activ[i].picturesWithoutColor.enabled = false;
				activ[i].picturesInColor.enabled = true;
			} else {
				activ[i].picturesInColor.enabled = false;
				activ[i].picturesWithoutColor.enabled = true;
			}
		}
	}
	private void EnabledAndDisabledButtonGame (int valiu)
	{
		for (int i = 0; i < activ.Count; i++) {
			if (i == valiu && itCanOpen[i] == true) {
				activ[i].okButton.enabled = true;
				activ [i].okButton.image.enabled = true;
				activ [i].tekstInside.enabled = true;
			} else {
				activ[i].okButton.enabled = false;
				activ [i].okButton.image.enabled = false;
				activ [i].tekstInside.enabled = false;
			}
		}
	}
	//Co do Buttonow loadGame
	public void ButtonImageScene (int i)
	{
		switch(i)
		{
		case 0:
			if(itCanOpen[0] == true)
			{
				EnabledAndDisabledButtonGame (0);
				//Debug.Log("ładuje scene forest");
			}
			else
			{
				//Debug.Log("Scena Forest zablokowana");
			}
			break;
		case 1:
			if(itCanOpen[1] == true)
			{
				EnabledAndDisabledButtonGame (1);
				//Debug.Log("ładuje scene river");
			}
			else
			{
				//EnabledAndDisabledButtonGame (2);
				//Debug.Log("Scena River zablokowana");
			}
			break;
		case 2:
			if(itCanOpen[2] == true)
			{
				EnabledAndDisabledButtonGame (2);
				//Debug.Log("ładuje scene City");
			}
			else
			{
				//Debug.Log("Scena City zablokowana");
			}
			break;
		case 3:
			if(itCanOpen[3] == true)
			{
				EnabledAndDisabledButtonGame (3);
				//Debug.Log("ładuje scene Desert");
			}
			else
			{
				//Debug.Log("Scena Desert zablokowana");
			}
			break;
		case 4:
			if(itCanOpen[4] == true)
			{
				EnabledAndDisabledButtonGame (4);
				//Debug.Log("ładuje scene Snow");
			}
			else
			{
				//Debug.Log("Scena Snow zablokowana");
			}
			break;

		default:
			break;

		}
	}
	//Co do okejek w scenie
	public void ClickOk (int valu)
	{
		if (valu <= unlockIndex) {
			for (int i = 0; i < activ.Count; i++) {
				if (i == valu && activ[i].okButton.enabled == true) {
					//AttendanceCanvas
					canvasLoadImage.gameObject.SetActive(true);
					canvasLoadImage.enabled = true;
					ms.loadingTime.enabled = true;
					ms.menuUI.enabled = false;
					ms.loadGame.enabled = false;
					/*if (GameObject.Find ("BrumBrume").GetComponent<SprawdzTerenScript> ().enabled == true) {
						GameObject.Find ("DashboardOnScreen").gameObject.SetActive (false);
					}*/
					ms.Disable (ms.loadGameComponents);
					//Attendance buttons
					activ [i].okButton.enabled = false;
					//Attendance menuScript
					ms.escUse = true;
					ms.duringGame = true;
					ms.newGameDisabled = true;
					//Load Scene
					MenuInstanceScript.respawnPlace = "respawnPlace"; //narazie nie wiem zapytac adama
					MenuInstanceScript.respawn = true;
					Application.LoadLevel(activ[i].nameOfScene);
					ms.IsResume (true);
				} 
			}
		}
	}
	public void Intate ()
	{
		for (int i = 0; i < itCanOpen.Length; i++) {
			if (i <= unlockIndex)
				itCanOpen [i] = true;
			else
				itCanOpen [i] = false;
		}
	}
	public void AssignDefaultValues ()
	{
		//Write defaults from disc
		mps.LoadInfo();
		//Music
		//Debug.Log(VolumeAndMusicScript.valueOfVolumeMusic);
		if(vms.valueOfVolumeMusic == 0)
			vms.Button1(true);
		else if(vms.valueOfVolumeMusic == 1)
			vms.Button2(true);
		else if(vms.valueOfVolumeMusic == 2)
			vms.Button3(true);
		else if(vms.valueOfVolumeMusic == 2)
			vms.Button3(true);
		else if(vms.valueOfVolumeMusic == 3)
			vms.Button4(true);
		else if(vms.valueOfVolumeMusic == 4)
			vms.Button5(true);
		//Sounds
		//Debug.Log(VolumeAndMusicScript.valueOfVolumeSound);
		if(vms.valueOfVolumeSound == 0)
			vms.Button1(false);
		else if(vms.valueOfVolumeSound == 1)
			vms.Button2(false);
		else if(vms.valueOfVolumeSound == 2)
			vms.Button3(false);
		else if(vms.valueOfVolumeSound == 2)
			vms.Button3(false);
		else if(vms.valueOfVolumeSound == 3)
			vms.Button4(false);
		else if(vms.valueOfVolumeSound == 4)
			vms.Button5(false);
		//Graphic
		switch (GraphicsScript.qualityLevel) {
		case 0:
			QualitySettings.currentLevel = QualityLevel.Fastest;
			break;
		case 1:
			QualitySettings.currentLevel = QualityLevel.Fast;
			break;
		case 2:
			QualitySettings.currentLevel = QualityLevel.Simple;
			break;
		case 3:
			QualitySettings.currentLevel = QualityLevel.Good;
			break;
		case 4:
			QualitySettings.currentLevel = QualityLevel.Beautiful;
			break;
		case 5:
			QualitySettings.currentLevel = QualityLevel.Fantastic;
			break;

		default:
			break;
		}
		crs.ChangeRes (ChangeResolutionScript.resolution);
		//Unlocked Scenes
		Intate ();
	}
}
[System.Serializable]
public class ImagesToLoadGame{
	[HideInInspector]public string nameOfScene;
	public Image picturesInColor;
	public Image picturesWithoutColor;
	public Button okButton;
	[HideInInspector]public int indeks;
	[HideInInspector]public Text tekstInside;

	public ImagesToLoadGame(string name, Image color, Image nonColor, Button okImidz, int ind, Text insider)
	{
		this.nameOfScene = name;
		this.picturesInColor = color;
		this.picturesWithoutColor = nonColor;
		this.okButton = okImidz;
		this.indeks = ind;
		this.tekstInside = insider;
	}
}