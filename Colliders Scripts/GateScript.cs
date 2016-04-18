using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {
	private Animator anim;
	public GameObject msObj;

	private bool open = false;

	void Start()
	{
		anim = this.GetComponent<Animator> ();
		anim.enabled = false;
	}
	void Update ()
	{
		if (anim.enabled == false) {
			MissionsScript ms = msObj.GetComponent<MissionsScript> ();
			if (ms.i > 3) {
				anim.enabled = true;
			} else {
				anim.enabled = false;
			}
		}
	}
	public void isOpen (bool open){

		this.open = open;
	}

	public bool isOpen (){

		return open;
	}

}
