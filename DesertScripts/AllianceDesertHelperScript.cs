using UnityEngine;
using System.Collections;

public class AllianceDesertHelperScript : MonoBehaviour {

	//private GameObject go;
	AllianceSoldierDesertEvent asde;
	// Use this for initialization
	void Start () {
		asde = GetComponentInParent<AllianceSoldierDesertEvent>();
		//go = this.gameObject;
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			asde.colliName = this.gameObject.name;
			asde.czyKolizja = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			asde.colliName = "none";
			asde.czyKolizja = false;
		}
	}
}
