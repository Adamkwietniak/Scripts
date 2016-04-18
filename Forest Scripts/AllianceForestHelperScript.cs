using UnityEngine;
using System.Collections;

public class AllianceForestHelperScript : MonoBehaviour {

	private GameObject go;
	AllianceSoliderForestEvent asfe;
	// Use this for initialization
	void Start () {
		asfe = GetComponentInParent<AllianceSoliderForestEvent>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Plater") {
			asfe.colliName = this.go.name;
			asfe.czyKolizja = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Plater") {
			asfe.colliName = "none";
			asfe.czyKolizja = false;
		}
	}
}
