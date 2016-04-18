using UnityEngine;
using System.Collections;

public class ChangeGearScript : MonoBehaviour {
	private bool isAutomatic = false;
	RCCCarControllerV2 rcc;
	// Use this for initialization
	void Start () {
		rcc = GetComponent<RCCCarControllerV2> ();
		isAutomatic = rcc.automaticGear;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAutomatic == false) {
			NumericChange();
		}
		if (Input.GetKeyUp (KeyCode.F9)) {
			rcc.automaticGear = !rcc.automaticGear;
			isAutomatic = rcc.automaticGear;
			rcc.autoReverse = true;
		}
	}
	private void NumericChange()
	{
		if (Input.GetKeyUp (KeyCode.Keypad1) && rcc.totalGears >= 1) {
			rcc.currentGear = 0;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad2) && rcc.totalGears >= 2) {
			rcc.currentGear = 1;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad3) && rcc.totalGears >= 3) {
			rcc.currentGear = 2;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad4) && rcc.totalGears >= 4) {
			rcc.currentGear = 3;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad5) && rcc.totalGears >= 5) {
			rcc.currentGear = 4;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad6) && rcc.totalGears >= 6) {
			rcc.currentGear = 5;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad7) && rcc.totalGears >= 7) {
			rcc.currentGear = 6;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad8) && rcc.totalGears >= 8) {
			rcc.currentGear = 7;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		if (Input.GetKeyUp (KeyCode.Keypad9) && rcc.totalGears >= 9) {
			rcc.currentGear = 8;
			rcc.StartCoroutine ("ChangingGear", rcc.currentGear);
			rcc.reversing = false;
		}
		/*if (Input.GetKeyUp (KeyCode.Keypad0)) { 					//wsteczny bieg
			rcc.reversing = true;
			rcc.autoReverse = true;
			rcc.currentGear = -1;
			rcc.StartCoroutine("ChangingGear", rcc.currentGear);
		}*/
		if (rcc.currentGear > 1 && rcc.autoReverse == true) {
			rcc.autoReverse = false;
		} else if (rcc.currentGear < 2 && rcc.autoReverse == false) {
			rcc.autoReverse = true;
		}
	}
}
