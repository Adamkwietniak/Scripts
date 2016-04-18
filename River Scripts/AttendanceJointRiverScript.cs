using UnityEngine;
using System.Collections;

public class AttendanceJointRiverScript : MonoBehaviour {

	

	AttendanceRiverFirstEventScript afses;
	private int vlv = 0;
	
	void Start () 
	{
		afses = GetComponentInParent<AttendanceRiverFirstEventScript>();
	}
	void OnTriggerEnter (Collider other) {
		
		if (other.tag == "Player") {

			afses.colliderName = this.gameObject.name;
			afses.czywTrigg = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {

			afses.czywTrigg = false;
			afses.DisableMsg ();
		}
	}

}
