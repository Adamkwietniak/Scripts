using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnowSplashOnScreenScript : MonoBehaviour {

	private Canvas canvasSnowSplash;
	private Image snowSplash;
	public float multiple = 1f;
	private float timer = 0;
	private float actualAlpha = 0;
	[HideInInspector]public bool inTheSnowdrivt = false;
	private bool splashOnScreen = false;
	[HideInInspector]public bool outZaspa = false;


	RCCCarControllerV2 rcc;

	void Awake ()
	{
		canvasSnowSplash = GameObject.Find ("SnowSplashCanvas").GetComponent<Canvas> ();
		snowSplash = GameObject.Find ("SnowSplashCanvas").GetComponentInChildren<Image> ();
		//Debug.Log (canvasSnowSplash);
		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;
		if(canvasSnowSplash.enabled == true)	
			canvasSnowSplash.enabled = false;
	}
	void Update ()
	{
		if (inTheSnowdrivt == true && canvasSnowSplash.enabled == false && splashOnScreen == false && outZaspa == false) {
			//Debug.Log ("do skurwysyna jasnego");
			float tempSpeec = rcc.speed;
			canvasSnowSplash.enabled = true;
			splashOnScreen = true;
			outZaspa = true;
			timer = tempSpeec / 50;
			if (tempSpeec < 1) {
				Mathf.Clamp (0, 255, tempSpeec);
			} else {
				actualAlpha = 255;
			}
			snowSplash.color = new Color (snowSplash.color.r, snowSplash.color.g,
				snowSplash.color.b, actualAlpha);
		} else if (inTheSnowdrivt == false && splashOnScreen == true) {
			if (actualAlpha > 0) {
				timer -= Time.deltaTime * multiple;
				actualAlpha = Mathf.Clamp (timer, 0, 255);
				snowSplash.color = new Color (snowSplash.color.r, snowSplash.color.g,
					snowSplash.color.b, actualAlpha);
			} else {
				splashOnScreen = false;
				canvasSnowSplash.enabled = false;
			}
			//Debug.Log ("Żydzi do gazu");
		}
	}

}
