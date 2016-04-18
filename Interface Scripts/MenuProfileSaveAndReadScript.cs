using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuProfileSaveAndReadScript : MonoBehaviour {


	FileStream plik;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveInfo()
	{
		if (!File.Exists (Application.persistentDataPath + "/profile.data"))
			File.Delete ((Application.persistentDataPath + "/profile.data"));
		plik = File.Create(Application.persistentDataPath + "/profile.data");
		
		MenuProff menuP = new MenuProff (VolumeAndMusicScript.valueOfVolumeMusic, VolumeAndMusicScript.valueOfVolumeSound,
			LoadGameScript.unlockIndex, GraphicsScript.qualityLevel, MultiLanguageScript.indexOfLang);
		menuP.musicValue = VolumeAndMusicScript.valueOfVolumeMusic;
		menuP.soundValue = VolumeAndMusicScript.valueOfVolumeSound;
		menuP.numberOfUnlockedScene = LoadGameScript.unlockIndex;
		menuP.valueOfGraphic = GraphicsScript.qualityLevel;
		menuP.language = MultiLanguageScript.indexOfLang;

		BinaryFormatter binFormat = new BinaryFormatter ();
		binFormat.Serialize(plik, menuP);
		plik.Close();
		Debug.Log ("Profile status was saved");
	}
	public static void LoadInfo()
	{

		if(File.Exists(Application.persistentDataPath + "/profile.data")){
			FileStream plik = File.Open(Application.persistentDataPath + "/profile.data", FileMode.Open);

			BinaryFormatter binFormat = new BinaryFormatter();
			MenuProff menuP = (MenuProff)binFormat.Deserialize(plik);
			menuP = (MenuProff)binFormat.Deserialize(plik);

			VolumeAndMusicScript.valueOfVolumeMusic = menuP.musicValue;
			VolumeAndMusicScript.valueOfVolumeSound = menuP.soundValue;
			LoadGameScript.unlockIndex = menuP.numberOfUnlockedScene;
			GraphicsScript.qualityLevel = menuP.valueOfGraphic;
			MultiLanguageScript.indexOfLang = menuP.language;
		}
		else
		{
			Debug.Log("Dont read game status becouse program dont find file with profiler");
			MenuProfileSaveAndReadScript mpsars;
			mpsars = (MenuProfileSaveAndReadScript)FindObjectOfType (typeof(MenuProfileSaveAndReadScript)) as MenuProfileSaveAndReadScript;
			mpsars.SaveInfo ();
			MenuProfileSaveAndReadScript.LoadInfo ();
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