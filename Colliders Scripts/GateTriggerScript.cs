using UnityEngine;
using System.Collections;

public class GateTriggerScript : MonoBehaviour {

	private Animator animator;
	public GameObject gatePivot;

	// Use this for initialization
	void Start () {

		animator = (Animator)gatePivot.GetComponent<Animator>();
	
	}

	void OnTriggerEnter (Collider other){

		//Debug.Log("Player Enter");

		if (other.tag == "Player") {
			GateScript gs = (GateScript)gatePivot.GetComponent<GateScript>();

			if (!gs.isOpen()){

				animator.SetTrigger ("Opened");
				gs.isOpen(true);
			}
		}

	}

	void OnTriggerExit(Collider other){

		//Debug.Log("Player Exit");

		if (other.tag == "Player") {
			GateScript gt = (GateScript)gatePivot.GetComponent<GateScript>();

			if (gt.isOpen()){
				animator.SetTrigger ("Closed");
				gt.isOpen(false);
			}
		}
	}

}
