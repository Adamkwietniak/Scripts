using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuProfileSaveAndReadScript : MonoBehaviour {

	VolumeAndMusicScript vms;

	void Awake () {
		vms = (VolumeAndMusicScript)FindObjectOfType (typeof(VolumeAndMusicScript)) as VolumeAndMusicScript;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.End))
			SaveInfo ();
	}

	public void SaveInfo()
	{
		if (!File.Exists (Application.persistentDataPath + "/profile.data"))
			File.Delete ((Application.persistentDataPath + "/profile.data"));
		FileStream plik = File.Create(Application.persistentDataPath + "/profile.data");
		
		MenuProff menuP = new MenuProff (vms.valueOfVolumeMusic, vms.valueOfVolumeSound,
			LoadGameScript.unlockIndex, GraphicsScript.qualityLevel, MenuScript.indexOfLang);
		menuP.musicValue = vms.valueOfVolumeMusic;
		menuP.soundValue = vms.valueOfVolumeSound;
		menuP.numberOfUnlockedScene = LoadGameScript.unlockIndex;
		menuP.valueOfGraphic = GraphicsScript.qualityLevel;
		menuP.language = MenuScript.indexOfLang;

		BinaryFormatter binFormat = new BinaryFormatter ();
		binFormat.Serialize(plik, menuP);
		plik.Close();
		//Debug.Log ("Profile status was saved");
		//Debug.Log (Application.persistentDataPath + "/profile.data");
	}
	public void LoadInfo()
	{
		
		if(File.Exists(Application.persistentDataPath + "/profile.data")){
			FileStream plik = File.Open(Application.persistentDataPath + "/profile.data", FileMode.Open);

			BinaryFormatter binFormat = new BinaryFormatter();
			MenuProff menuP = (MenuProff)binFormat.Deserialize(plik);

			vms.valueOfVolumeMusic = menuP.musicValue;
			vms.valueOfVolumeSound = menuP.soundValue;
			LoadGameScript.unlockIndex = menuP.numberOfUnlockedScene;
			GraphicsScript.qualityLevel = menuP.valueOfGraphic;
			MenuScript.indexOfLang = menuP.language;
			plik.Close();
			//Debug.Log ("Wczytano takie wartosci: MusicV: " + menuP.musicValue + " SoundV: " + menuP.soundValue + " UnlockScene: " + 
				//menuP.numberOfUnlockedScene + " V of graf: " + menuP.valueOfGraphic + " Language: " +menuP.language);
		}
		else
		{
			//Debug.Log("Dont read game status becouse program dont find file with profiler");
			SaveInfo ();
			LoadInfo ();
		}
	}
}

[Serializable]
public class MenuProff{
	public int musicValue;
	public int soundValue;
	public int numberOfUnlockedScene;
	public int valueOfGraphic;
	public int language;

	public MenuProff(int musikValu, int soundValu, int valuOfScene, int valuOfGraph, int lang)
	{
		this.musicValue = musikValu;
		this.soundValue = soundValu;
		this.numberOfUnlockedScene = valuOfScene;
		this.valueOfGraphic = valuOfGraph;
		this.language = lang;
	}
}