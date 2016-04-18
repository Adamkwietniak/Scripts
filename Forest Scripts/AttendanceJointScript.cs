using UnityEngine;
using System.Collections;

public class AttendanceJointScript : MonoBehaviour {
	
	AttendanceForestFirstEventScript affes;
	private int vlv = 0;

	void Start () 
	{
		affes = GetComponentInParent<AttendanceForestFirstEventScript>();
		affes = (AttendanceForestFirstEventScript)FindObjectOfType (typeof(AttendanceForestFirstEventScript)) as AttendanceForestFirstEventScript;
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
