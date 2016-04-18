using UnityEngine;
using System.Collections;

public class AllianceCityHelperScript : MonoBehaviour {

	private GameObject go;
	AllianceCityScript ags;
	// Use this for initialization
	void Start () {
		ags = GetComponentInParent<AllianceCityScript>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			ags.colliName = this.go.name;
			ags.czyKolizja = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			ags.colliName = "none";
			ags.czyKolizja = false;
		}
	}
}
