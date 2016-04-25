using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnowSplashOnScreenScript : MonoBehaviour {

	public Canvas canvasSnowSplash;
	public Image [] images = new Image[2]; //first for cleanSnow, second for full screen on the snow

	private float actualTimeClean = 1;

	[HideInInspector]public bool inSnowM = false;
	[HideInInspector]public bool needToEscape = true;
	private bool snwOnScreen = false;
	private bool cleaningScreen = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (inSnowM == true) {
			if(snwOnScreen == false)
				snwOnScreen = true;
			if (snwOnScreen == true && needToEscape == true) {
				needToEscape = false;
				images [1].enabled = true;
				images [1].color = new Color (images [1].color.r, images [1].color.g, images [1].color.b, 255);
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad0) && snwOnScreen == true && cleaningScreen == false) {
			cleaningScreen = true;
		}
		if (cleaningScreen == true) {
			if (actualTimeClean <= 1 && actualTimeClean > 0)
				actualTimeClean -= Time.deltaTime/2;
			else {
				snwOnScreen = false;
				actualTimeClean = 1;
				images [1].enabled = false;
				cleaningScreen = false;
			}
			CleaningScreen (Mathf.Clamp (actualTimeClean, 0, 255));
		}
	}
	private void CleaningScreen (float actT)
	{
		images [1].color = new Color (images [1].color.r, images [1].color.g, images [1].color.b, actT);
	}
}
