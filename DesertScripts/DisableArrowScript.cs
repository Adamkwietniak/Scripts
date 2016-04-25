using UnityEngine;
using System.Collections;

public class DisableArrowScript : MonoBehaviour {

	public GameObject arrowObj;
	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			arrowObj.SetActive (false);
			//Destroy (this);
		}
	}
}
