using UnityEngine;
using System.Collections;

public class MissionRoundScript : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.down * Time.deltaTime *100);
		//transform.position=  Vector3.MoveTowards(transform.position.x, Time.deltaTime*2, transform.position.z);
	
	}
}
