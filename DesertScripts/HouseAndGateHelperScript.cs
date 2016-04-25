using UnityEngine;
using System.Collections;

public class HouseAndGateHelperScript : MonoBehaviour {

	HouseAndGateScript hags;
	// Use this for initialization
	void Awake ()
	{
		hags = (HouseAndGateScript)FindObjectOfType (typeof(HouseAndGateScript)) as HouseAndGateScript;
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			if (hags.name != this.gameObject.name) {
				hags.nameObiect = this.gameObject.name;
				hags.isCamAnim = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			if (hags.name == this.gameObject.name) {
				//hags.name = "none";
				hags.isCamAnim = false;
			}
		}
	}

}
