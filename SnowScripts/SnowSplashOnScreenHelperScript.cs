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
			if (ssoss.inTheSnowdrivt == false) {
				ssoss.inTheSnowdrivt = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			if (ssoss.outZaspa == true) {
				ssoss.outZaspa = false;
			}
			if (ssoss.inTheSnowdrivt == true) {
				ssoss.inTheSnowdrivt = false;
			}
		}
	}
}
