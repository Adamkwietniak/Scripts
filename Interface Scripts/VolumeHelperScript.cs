﻿using UnityEngine;
using System.Collections;

public class VolumeHelperScript : MonoBehaviour {

	VolumeAndMusicScript vms;
	MenuScript ms;
	[HideInInspector]public GameObject loading;
	// Use this for initialization
	void Awake () 
	{
		ms = (MenuScript)FindObjectOfType (typeof(MenuScript)) as MenuScript;
		if (loading == null) {
			loading = GameObject.Find ("LOADING");
			//Debug.Log ("Załadwałem GameObiect: " + loading.name);
		}
	}
	void Start () {
		
		if (loading != null)
			loading.GetComponent<Canvas> ().enabled = false;
		//ms.ReloadDash ();
		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));
		//vms.ResetTabs();
		vms.Initiate();
		AssignOldVolume ();

	}
	public void AssignOldVolume ()
	{
		switch(vms.valueOfVolumeMusic)
		{
		case 0:
			vms.Button1(true);
			break;
		case 1:
			vms.Button2(true);
			break;
		case 2:
			vms.Button3(true);
			break;
		case 3:
			vms.Button4(true);
			break;
		case 4:
			vms.Button5(true);
			break;
		default :
			break;
		}
		//Debug.Log (VolumeAndMusicScript.valueOfVolumeSound);
		switch(vms.valueOfVolumeSound)
		{
		case 0:
			vms.Button1(false);
			break;
		case 1:
			vms.Button2(false);
			break;
		case 2:
			vms.Button3(false);
			break;
		case 3:
			vms.Button4(false);
			break;
		case 4:
			vms.Button5(false);
			break;
		default :
			break;
		}
		//Debug.Log("zaladowalem dzwiek");
	}
}
