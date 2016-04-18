using UnityEngine;
using System.Collections;

public class BombingHelpperScript : MonoBehaviour {

	BombardingScript bs;

	void Start ()
	{
		bs = (BombardingScript)FindObjectOfType (typeof(BombardingScript)) as BombardingScript;
	}
	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player" && bs.bombInScene == false) {
			bs.bombInScene = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player" && bs.bombInScene == true) {
			bs.bombInScene = false;
		}
	}
}
