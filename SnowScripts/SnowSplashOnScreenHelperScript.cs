using UnityEngine;
using System.Collections;

public class SnowSplashOnScreenHelperScript : MonoBehaviour {

	SnowSplashOnScreenScript ssoss;

	void Awake ()
	{
		ssoss = (SnowSplashOnScreenScript)FindObjectOfType (typeof(SnowSplashOnScreenScript)) as SnowSplashOnScreenScript;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if (ssoss.inSnowM == false) {
				ssoss.inSnowM = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			if (ssoss.needToEscape == true) {
				ssoss.needToEscape = false;
			}
			if (ssoss.inSnowM == true) {
				ssoss.inSnowM = false;
			}
		}
	}
}
