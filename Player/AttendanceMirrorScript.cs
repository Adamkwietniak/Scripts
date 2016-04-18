using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AttendanceMirrorScript : MonoBehaviour {
	
	private Transform cameraTR;
	public GameObject objWithCams;

	private List<Camera> cams= new List<Camera>();
	private List<Transform> camTr = new List<Transform>();
	private Camera activeCamera;
	private int helpInt;
	private bool isN = false;
	private bool czySieWysuwa = false;
	private bool czySiePorusza = false;
	private bool currentCamFlag = true; // zmienna ktora ustala ktory mesh z listy luster ma byc brany pod uwage
	UseCameraScript usc;
	public Canvas dirtyMirror;
	public Canvas miniMap;
	private bool isMinimap = false;
	public Text textOutMinimap;
	public Text [] textsInMinimap = new Text[1];

	// Use this for initialization
	void Start () {
		usc = objWithCams.GetComponent<UseCameraScript> ();
		dirtyMirror.enabled = false;

		for (int i = 0; i < usc.camers.Length; i++) {
			cams.Add(usc.camers[i].GetComponent<Camera>());
			camTr.Add (usc.camers[i].GetComponent<Transform>());
		}

		cameraTR = objWithCams.GetComponent<Transform> ();

		CheckActiveCam ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (dirtyMirror.enabled == true) {
				dirtyMirror.enabled = false;
				BackCamera (0);
			}
			if (miniMap.enabled == true) {
				miniMap.enabled = false;
				Time.timeScale = 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			czySieWysuwa = false;
			if(dirtyMirror.enabled == true)
				dirtyMirror.enabled = false;
			//DisableMirrors ();
		}
		if (Input.GetKeyDown (KeyCode.RightAlt)) {
			helpInt = CheckActiveCam ();
			if(helpInt == cams.Count-2 || helpInt == cams.Count-3){
				if(helpInt == cams.Count-2)
				{
					if(dirtyMirror.enabled == false)
						dirtyMirror.enabled = true;
					BackCamera (cams.Count-3);
					currentCamFlag = true;
				}
				else if(helpInt == cams.Count-3)
				{
					if(dirtyMirror.enabled == false)
						dirtyMirror.enabled = true;
					BackCamera (cams.Count-2);
					currentCamFlag = true;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			helpInt = CheckActiveCam ();

			if (helpInt > -1) {
				if (helpInt == 0 || helpInt == cams.Count - 2 || helpInt == cams.Count - 3) {
					isN = ! isN;
					if (currentCamFlag == false || helpInt == cams.Count - 3) {
						dirtyMirror.enabled = false;
						BackCamera (0);
						currentCamFlag = true;
						czySiePorusza = false;
					} else if (currentCamFlag == true) {
						dirtyMirror.enabled = true;
						BackCamera (cams.Count - 2);
						currentCamFlag = false;
						czySiePorusza = false;
					}

				} else if (helpInt > 0 && helpInt != cams.Count - 2 && helpInt != cams.Count - 3) {
					dirtyMirror.enabled = false;
					czySieWysuwa = !czySieWysuwa;
					czySiePorusza = true;
				} 
			}
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			isMinimap = !isMinimap;
			MiniMapService (isMinimap);
		}

	}
		
	private void MiniMapService (bool isi)
	{
		if (textOutMinimap.enabled == isi)
			textOutMinimap.enabled = !isi;
		if (miniMap.enabled != isi)
			miniMap.enabled = isi;
		if (textsInMinimap.Length > 0) {
			for (int i = 0; i < textsInMinimap.Length; i++) {
				if(textsInMinimap [i].enabled != isi)
					textsInMinimap [i].enabled = isi;
			}
		}
		if (isi == false) {
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}
	}
	private void BackCamera (int i)
	{
		usc.ChangeCams (i);
		//Debug.Log ("Przelaczam");
	}

	private int CheckActiveCam ()
	{
		for (int i = 0; i < cams.Count; i++) {
			if (HelpCheckActiveCam (i) != null) {
				activeCamera = cams [i];
				return i;
			}
		}
		return -1;
	}
	private Camera HelpCheckActiveCam (int i)
	{
		if (cams [i].enabled == true)
			return cams [i];
		return null;
	}

}
