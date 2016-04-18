using UnityEngine;
using System.Collections;

public class AllianceTutorialHelperScript : MonoBehaviour {
	
	private GameObject go;
	AllianceSoliderEvent asfe;
	// Use this for initialization
	void Start () {
		asfe = GetComponent<AllianceSoliderEvent>();
	}
	/*
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			asfe.colliName = this.go.name;
			asfe.czyKolizja = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			asfe.colliName = "none";
			asfe.czyKolizja = false;
		}
	}*/
}