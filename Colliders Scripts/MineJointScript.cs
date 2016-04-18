using UnityEngine;
using System.Collections;

public class MineJointScript : MonoBehaviour {

	MineScript ms;
	private Transform trans;
	// Use this for initialization
	void Start () {
		ms = GetComponentInParent<MineScript> ();
		trans = this.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			ms.minePos = this.trans.position;
			ms.checkTrigger = true;
			ms.isMine = true;
			ms.checkCollider = this.gameObject.name;
			//Debug.Log(this.gameObject.name);
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			ms.checkTrigger = false;
			ms.checkCollider = "none";
			ms.isMine = false;
		}
	}
}
