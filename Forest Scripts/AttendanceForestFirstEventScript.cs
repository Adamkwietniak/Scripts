﻿using UnityEngine;
using System.Collections;
using System;


//Skrypt odpowiedzialny za obsluge zdarzen nieusowalnych w trakcie gry.
public class AttendanceForestFirstEventScript : MonoBehaviour {

	public Canvas msg;
	public AttendanceScript[] attendance;
	public GameObject obiectWithCamerasInside;
	public String colliderName;
	public AudioClip tree;
	public AudioClip stone;

	private int [] oldCam = new int[20];
	private bool msgBool = false;
	private String [] nazwaCollidera = new string[20];//Tablice do sprawdzania czy istnieje dany obiekt
	private String [] nazwaMsg = new string[20];				//czy nie istnieje
	private int temp = 0;					//Zmienna pomocnicza od ktorej zalezy ktory kollider mamy
	private Camera [] cami = new Camera[20];					//Tablica kamer
	AttendanceScript attendancea = new AttendanceScript ();

	public GameObject player;
	UseCameraScript ucs;
	MissionForestScript mfs;

	public bool czywTrigg = false; //dziala
	public bool czyCamTrigg = false;
	private bool resetOldCam = false;
	private bool msgFlag = false;
	private bool missionFlagMassage = false;


	// Use this for initialization
	void Start () {
		ucs = obiectWithCamerasInside.GetComponent<UseCameraScript> ();
		mfs = player.GetComponent<MissionForestScript> ();
		for (int i = 0; i<attendance.Length; i++) {
			nazwaCollidera [i] = attendance [i].eventTrigger.name; //dziala
			if (attendance [i].eventCamera != null) {			//dziala
				cami [i] = attendance [i].eventCamera;
			} else {
				cami [i] = null;
			}
		}

		foreach (AttendanceScript at in attendance) {
			if (at.eventCamera != null)
				at.eventCamera.enabled = true;
			if (at.eventTrigger != null)
				at.eventTrigger.SetActive (true);
			if (at.AttendanceItems != null && at.AttendanceItems.Length > 0) {
				foreach (obslObj item in at.AttendanceItems) {
					//item.rbItem.useGravity = false;
					item.rbItem.isKinematic = true;
				}
			}
			CheckOldCameras ();
		}
		if (msg != null && msgBool == false) {
			msg = msg.GetComponent<Canvas> ();
			msgBool = true;
		}
		if (msgBool == true && attendance != null) {
			for (int i = 0; i < attendance.Length; i++) {
				if (attendance [i].msgZeCanvas != null) {
					nazwaMsg [i] = attendance [i].msgZeCanvas.name;
				} else {
					nazwaMsg [i] = null;
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (czywTrigg == true || czyCamTrigg == true) {
			for (int z=0; z<nazwaCollidera.Length; z++) {
				if (nazwaCollidera [z] == colliderName) {
					temp = z;
					FunCase ();
				} 
			}
		}
		if (mfs.i > 1 && missionFlagMassage == false) {
			DisableOfMssg();
			missionFlagMassage = true;
		}

	}
	void FunCase (){

		switch (temp) {
		
		default:
			Metods ();
			break;
		}
	}
	void AttendanceRigBody ()
	{
		for (int i = 0; i < attendance.Length; i++) {
			if (attendance[i].czyRbEnbl == true && i == temp) {
				for (int j = 0; j < attendance[i].lengthOfTab; j++){
				if(attendancea.AttendanceItems != null)
					
					attendance[i].AttendanceItems[j].rbItem.useGravity = true;
					attendance[i].AttendanceItems[j].rbItem.isKinematic = false;
					if(attendance[i].AttendanceItems[j].isStone == true && attendance[i].AttendanceItems[j].audioSource.isPlaying == false && attendance[i].AttendanceItems[j].rbItem.IsSleeping() == false
					   && attendance[i].AttendanceItems[j].items.GetComponent<Transform>().hasChanged == true){
						attendance[i].AttendanceItems[j].audioSource.clip = stone;
						attendance[i].AttendanceItems[j].audioSource.PlayOneShot(stone);
					}
					else if(attendance[i].AttendanceItems[j].isStone == false){
						attendance[i].AttendanceItems[j].audioSource.clip = tree;
						attendance[i].AttendanceItems[j].audioSource.PlayOneShot(tree);
					}
						
				}
			}
		}
	}
	void AttendanceMsg ()
	{
		for (int j = 0; j < attendance.Length; j++){
		if (attendance[j].canvasView == true && czywTrigg == true) {
			
				if (j == temp && nazwaMsg [j] != null) {
					attendance [j].msgZeCanvas.SetActive (true);
					msgFlag = false;
					} else if (j != temp || nazwaMsg [j] == null) {
					attendance [j].msgZeCanvas.SetActive (false);
					//msgFlag = true;
				}
			
			} else if(czywTrigg == false && attendance[j].canvasView == true)
			{
				attendance [j].msgZeCanvas.SetActive (false);
				//msgFlag = true;
			}
		}
	}

	void DisableCameraBrum ()
	{

		CheckOldCameras ();
		foreach (Camera cams in ucs.camers) {

			cams.enabled = false;
		}
	}
	private void ChangeCameria (int x)
	{
		if (czyCamTrigg == false && czywTrigg == true && cami [x] != null) {
			DisableCameraBrum ();
			cami [x].enabled = true;
			czyCamTrigg = true;
		} //else 
			//SetOldCameras ();
		CheckCam ();
	}
	private void CheckCam ()
	{
		if (czyCamTrigg == true && czywTrigg == false) {
			//UseCameraScript ucs = obiectWithCamerasInside.GetComponent<UseCameraScript> ();
			for(int i = 0; i < ucs.camers.Length; i++)
			{
				if(ucs.camers[i].enabled == true){
					//Debug.Log("przerywam petle");
					break;
				}
				else if(ucs.camers[i].enabled == false && resetOldCam == false) // włączanie starej kamery po wyjechaniu
				{	for(int q = 0; q < ucs.camers.Length; q++)					// z triggera
					{
						if(oldCam[q] == 1){
							//Debug.Log ("jedziemy z koksem");
							ucs.camers[q].enabled = true;
							czyCamTrigg = false;
							resetOldCam = true;

							for(int u = 0; u < attendance.Length; u++)
							{
								//Debug.Log ("wylaczam kamere triggera");
								if(cami[u].enabled == true)
									cami[u].enabled = false;
							}

						}
					}
					ResetOldCameras ();

				}
			}

		}
	}
	private void ResetOldCameras ()
	{
		if(resetOldCam == true)
		{
			for(int i = 0; i < oldCam.Length; i++)
			{
				oldCam[i] = 0;
			}
			resetOldCam = false;
			//Debug.Log("Resetowanie tablicy");
		}

	}
	void Metods ()
	{	for (int i = 0; i < attendance.Length; i++) {
			if (i == temp){
			if (attendance[i].switchCamera == true)
				CameraCase (temp);
			if (attendance[i].czyRbEnbl == true) {
				AttendanceRigBody ();
			}
			if (attendance[i].canvasView == true)
				AttendanceMsg ();
			if (attendance[i].czySieWylacza == true) {
				DisableButtonSwitch (i);

			}
			}
		}
	}
	void DisableButtonSwitch (int i)
	{
		//for (int i = 0; i < attendance.Length; i++) {
			if (i == temp) {
				if (attendance[i].switchCamera == true)
					attendance[i].switchCamera = false;
				if (attendance[i].czyRbEnbl == true)
					attendance[i].czyRbEnbl = false;
				if (attendance[i].canvasView == true)
					attendance[i].canvasView = false;
			}
		//}
	}
	private void SetOldCameras ()
	{
		CheckOldCameras ();
		for(int att= 0; att<attendance.Length; att++) {
			if (attendance [att].eventCamera == null && cami[att] == null) {
				//UseCameraScript ucs = obiectWithCamerasInside.GetComponent<UseCameraScript> ();
				for(int i = 0; i< ucs.camers.Length; i++)
				{
					if(oldCam[i] == 1)
						attendance [att].eventCamera = ucs.camers[i];
				}
			}
		}
		//ResetOldCameras ();
	}
	private void CheckOldCameras()
	{
		//UseCameraScript ucs = obiectWithCamerasInside.GetComponent<UseCameraScript> ();
		for(int i = 0; i< ucs.camers.Length; i++)
		{
			if(ucs.camers[i].enabled == true)
				oldCam[i] = 1;
			else
				oldCam[i] = 0;
		}
	}
	private void CameraCase (int tem)
	{
		if (attendance[tem].switchCamera == true) {
			if (cami [tem] != null) {
				ChangeCameria (tem);
				//Debug.Log ("case 0");
			} else 
				SetOldCameras ();
		}
	}
	public void DisableMsg ()
	{
		for(int i = 0; i < attendance.Length; i++)
		{
			if(attendance[i].canvasView == true && czywTrigg == false){
				attendance[i].msgZeCanvas.SetActive(false);
				msgFlag = true;
			}
		}
	}
	public void DisableOfMssg ()
	{
		for (int j = 0; j < attendance.Length; j++){
			if(attendance[j].canvasView == true && (j == 15 || j == 16 || j == 17))
			{
				attendance[j].canvasView = false;
			}
		}
	}

}









