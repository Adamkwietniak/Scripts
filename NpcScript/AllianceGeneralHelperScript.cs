using UnityEngine;
using System.Collections;

public class AllianceGeneralHelperScript : MonoBehaviour {

	private GameObject go;
	AllianceGeneralScript ags;
	// Use this for initialization
	void Start () {
		ags = GetComponentInParent<AllianceGeneralScript>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Plater") {
			ags.colliName = this.go.name;
			ags.czyKolizja = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Plater") {
			ags.colliName = "none";
			ags.czyKolizja = false;
		}
	}
}
