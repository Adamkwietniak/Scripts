using UnityEngine;
using System.Collections;

public class FallingTrapScript : MonoBehaviour {

	public GameObject [] fallRock = new GameObject [1];
	//public Camera cam;

	// Use this for initialization
	void Start () {
		//cam = GetComponent<Camera> ();
		//cam.enabled = false;
		foreach (GameObject fr in fallRock) {
			fr.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider NonCollider)
	{
		if (NonCollider.tag == "Player") {
			StartFallingRock();
		}
	}
	void StartFallingRock()
	{
		//rb = GetComponentsInChildren<Rigidbody> ();
		foreach (GameObject fr in fallRock) {
			fr.SetActive(true);
		}
		//cam.enabled = true;
	}

}
