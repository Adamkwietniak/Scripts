using UnityEngine;
using System.Collections;

public class AttendanceJointDesertScript : MonoBehaviour {

	AttendanceDesertEventScript ades;
	private int vlv = 0;

	void Start () 
	{
		ades = GetComponentInParent<AttendanceDesertEventScript>();
	}
	void OnTriggerEnter (Collider other) {

		if (other.tag == "Player") {
			ades.colliderName = this.gameObject.name;
			ades.czywTrigg = true;

		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			ades.czywTrigg = false;
			ades.DisableMsg ();

		}
	}
}
