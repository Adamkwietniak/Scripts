using UnityEngine;
using System.Collections;

public class AttendanceJointSnowScript : MonoBehaviour {
	
	AttendanceSnowEventScript affes;

	void Start () 
	{
		affes = GetComponentInParent<AttendanceSnowEventScript>();
	}
	void OnTriggerEnter (Collider other) {
		
		if (other.tag == "Player") {
			affes.colliderName = this.gameObject.name;
			affes.czywTrigg = true;

		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			affes.czywTrigg = false;
			affes.DisableMsg ();

		}
	}
}
