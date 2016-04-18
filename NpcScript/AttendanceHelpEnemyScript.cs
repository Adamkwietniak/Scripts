using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttendanceHelpEnemyScript : MonoBehaviour {


	public GameObject enemyObj;
	AttendanceEnemy ae;
	void Start ()
	{
		ae = enemyObj.GetComponent<AttendanceEnemy> ();

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			ae.collDetect = true;
			ae.trigerDetection = this.gameObject.name;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			ae.collDetect = false;
			ae.trigerDetection = "none";
		}
	}
}

