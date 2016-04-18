using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGameScript : MonoBehaviour {

	VolumeAndMusicScript vms;
	private bool [] itCanOpen = new bool[5];
	public static int unlockIndex = 0;
	// Use this for initialization

	void Awake ()
	{
		vms = (VolumeAndMusicScript)FindObjectOfType (typeof(VolumeAndMusicScript)) as VolumeAndMusicScript;
		AssignDefaultValues ();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//Co do Buttonow loadGame
	public void ButtonEnterScene (int i)
	{
		switch(i)
		{
		case 0:
			if(itCanOpen[0] == true)
			{
				Debug.Log("ładuje scene forest");
			}
			else
			{
				Debug.Log("Scena zablokowana");
			}
			break;
		case 1:
			if(itCanOpen[1] == true)
			{
				Debug.Log("ładuje scene river");
			}
			else
			{
				Debug.Log("Scena River zablokowana");
			}
			break;
		case 2:
			if(itCanOpen[2] == true)
			{
				Debug.Log("ładuje scene City");
			}
			else
			{
				Debug.Log("Scena City zablokowana");
			}
			break;
		case 3:
			if(itCanOpen[3] == true)
			{
				Debug.Log("ładuje scene Desert");
			}
			else
			{
				Debug.Log("Scena Desert zablokowana");
			}
			break;
		case 4:
			if(itCanOpen[4] == true)
			{
				Debug.Log("ładuje scene Snow");
			}
			else
			{
				Debug.Log("Scena Snow zablokowana");
			}
			break;

		default:
			break;

		}
	}
	public void Intate ()
	{
		for (int i = 0; i < itCanOpen.Length; i++) {
			if (i < unlockIndex)
				itCanOpen [i] = true;
			else
				itCanOpen [i] = false;
		}
	}
	public void AssignDefaultValues ()
	{
		//Write defaults from disc
		MenuProfileSaveAndReadScript.LoadInfo();
		//Music
		if(VolumeAndMusicScript.valueOfVolumeMusic == 0)
			vms.Button1(true);
		else if(VolumeAndMusicScript.valueOfVolumeMusic == 1)
			vms.Button2(true);
		else if(VolumeAndMusicScript.valueOfVolumeMusic == 2)
			vms.Button3(true);
		else if(VolumeAndMusicScript.valueOfVolumeMusic == 2)
			vms.Button3(true);
		else if(VolumeAndMusicScript.valueOfVolumeMusic == 3)
			vms.Button4(true);
		else if(VolumeAndMusicScript.valueOfVolumeMusic == 4)
			vms.Button5(true);
		//Sounds
		if(VolumeAndMusicScript.valueOfVolumeSound == 0)
			vms.Button1(false);
		else if(VolumeAndMusicScript.valueOfVolumeSound == 1)
			vms.Button2(false);
		else if(VolumeAndMusicScript.valueOfVolumeSound == 2)
			vms.Button3(false);
		else if(VolumeAndMusicScript.valueOfVolumeSound == 2)
			vms.Button3(false);
		else if(VolumeAndMusicScript.valueOfVolumeSound == 3)
			vms.Button4(false);
		else if(VolumeAndMusicScript.valueOfVolumeSound == 4)
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
		//Unlocked Scenes
		Intate ();
	}
}
