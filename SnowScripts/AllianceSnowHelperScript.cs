using UnityEngine;
using System.Collections;

public class AllianceSnowHelperScript : MonoBehaviour {

	private GameObject go;
	AllianceSoldierSnowEvent asde;
	// Use this for initialization
	void Start () {
		asde = GetComponentInParent<AllianceSoldierSnowEvent>();
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			asde.colliName = this.go.name;
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
