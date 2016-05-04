using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeAndMusicScript : MonoBehaviour {
	
	private AudioSource [] allAudios;
	private List <AudioSource> music = new List<AudioSource>();
	private List <AudioSource> sounds = new List<AudioSource>();
	RCCCarControllerV2 rcc;
	public int valueOfVolumeMusic = 3;
	public int valueOfVolumeSound = 3;
	MenuScript ms;
	private float oldValue;
	private float oldValue2;
	private float oldValue3;
	private bool played = true;
	private bool pleyed = false;
	private bool greenLight = false;
	private List<Image> missionImg = new List<Image>();
	private GameObject [] tabOfMissions = new GameObject[25];
	private bool c = false;
	private bool g = false;
	[HideInInspector]public bool isMsg = false;
	private int oldvalueOfVolumeSound = 0;
	private Canvas gzComplete;
	// Use this for initialization
	void Awake (){
		Initiate ();
	}
	void Start ()
	{
		//Initiate ();
		/*for (int i = 0; i < allAudios.Length; i++) {
			Debug.Log (allAudios [i]);
		}*/
	}
	public void Initiate () {
		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;
		ms = (MenuScript)FindObjectOfType (typeof(MenuScript)) as MenuScript;
		if (rcc.gameObject.GetComponent<SprawdzTerenScript> ().enabled == true) {
			gzComplete = GameObject.Find ("GZMissionComplete").GetComponent<Canvas> ();
			greenLight = true;
		} else {
			greenLight = false;
		}
		allAudios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		for (int i = 0; i < allAudios.Length; i++) {
			//Debug.Log(allAudios[i].name);
			if(allAudios[i].outputAudioMixerGroup != null){
				if(allAudios[i].outputAudioMixerGroup.name == "Player" || allAudios[i].outputAudioMixerGroup.name == "Environment" ||
			   	allAudios[i].outputAudioMixerGroup.name == "Enemies" || allAudios[i].outputAudioMixerGroup.name == "Others"){
					sounds.Add(allAudios[i]);
				}
				else
				{
					music.Add(allAudios[i]);
				}
			}
		}
	}
	void Update ()
	{
		if (ms.menuUI != null)
		{
			if(ms.menuUI.enabled == true && played == false){
				oldValue = rcc.actualValue;
				oldvalueOfVolumeSound = valueOfVolumeMusic;
				rcc.WriteNewValueOfCar(0);
				played = true;
				//Debug.Log ("wlaczam menu");
			}
			else if(ms.menuUI.enabled == false && played == true)
			{
				//Debug.Log ("wlaczam menu");
				if(oldvalueOfVolumeSound == valueOfVolumeSound)
					rcc.WriteNewValueOfCar(oldValue);
				else
				{
					switch (valueOfVolumeSound){
					case 0:
						rcc.WriteNewValueOfCar(0);
						break;
					case 1:
						rcc.WriteNewValueOfCar(0.25f);
						break;
					case 2:
						rcc.WriteNewValueOfCar(0.5f);
						break;
					case 3:
						rcc.WriteNewValueOfCar(0.75f);
						break;
					case 4:
						rcc.WriteNewValueOfCar(1);
						break;
					
					default:
						break;
					}
				}
				played = false;
			}
		}
		if(ms.menuUI.enabled == false && isMsg == true && c == false){
			oldValue2 = rcc.actualValue;
			rcc.WriteNewValueOfCar(0);
			c = true;
		}
		else if(ms.menuUI.enabled == false && isMsg == false && c == true)
		{
			rcc.WriteNewValueOfCar(oldValue2);
			c = false;
		}
		if (greenLight == true) {
			if (SceneManager.GetActiveScene ().name != "SceneCanvas") {
				if (gzComplete.enabled == true && ms.menuUI.enabled == false && g == false) {
					oldValue3 = rcc.actualValue;
					rcc.WriteNewValueOfCar (0);
					g = true;
				} else if (gzComplete.enabled == false && ms.menuUI.enabled == false && g == true) {
					rcc.WriteNewValueOfCar (oldValue3);
					g = false;
				}
			}
		}
	}
	// Update is called once per frame
	public void Button1(bool isMusic)
	{
		if (isMusic == true) {
			AssignSoundsVolume(0, true);
			valueOfVolumeMusic = 0;
		}
		if (isMusic == false) {
			AssignSoundsVolume(0, false);
			valueOfVolumeSound = 0;
		}

	}
	public void Button2(bool isMusic)
	{
		if (isMusic == true) {
			AssignSoundsVolume(0.25f, true);
			valueOfVolumeMusic = 1;
		}
		if (isMusic == false) {
			AssignSoundsVolume(0.25f, false);
			valueOfVolumeSound = 1;
		}
	}
	public void Button3(bool isMusic)
	{
		if (isMusic == true) {
			AssignSoundsVolume(0.5f, true);
			valueOfVolumeMusic = 2;
		}
		if (isMusic == false) {
			AssignSoundsVolume(0.5f, false);
			valueOfVolumeSound = 2;
		}
	}
	public void Button4(bool isMusic)
	{
		if (isMusic == true) {
			AssignSoundsVolume(0.75f, true);
			valueOfVolumeMusic = 3;
		}
		if (isMusic == false) {
			AssignSoundsVolume(0.75f, false);
			valueOfVolumeSound = 3;
		}
	}
	public void Button5(bool isMusic)
	{
		if (isMusic == true) {
			AssignSoundsVolume(1, true);
			valueOfVolumeMusic = 4;
		}
		if (isMusic == false) {
			AssignSoundsVolume(1, false);
			valueOfVolumeSound = 4;
		}
	}
	private void AssignSoundsVolume (float volume, bool slider)
	{
		if (slider == true) {
			for(int i = 0; i < music.Count; i++)
			{
				if(music[i] != null)
					music[i].volume = volume;
			}
		}
		
		if (slider == false) {
			for(int i = 0; i < sounds.Count; i++)
			{
				if(sounds[i] != null)
					sounds[i].volume = volume;
				//rcc.WriteNewValueOfCar(volume);
				//rcc.startEngineVolume = volume;
			}
			rcc.WriteNewValueOfCar (volume);
			rcc.startEngineVolume = volume;
		}
	}
	/*private void AssignVolumeAudio(bool isIt)
	{
		for (int i = 0; i < sounds.Count; i++) {

		}
	}*/
}
