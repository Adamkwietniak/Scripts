using UnityEngine;
using System.Collections;

public class MissionRoundScript : MonoBehaviour {


	private Transform tr;
	void Awake ()
	{
		tr = this.GetComponent<Transform> ();
	}
	// Update is called once per frame
	//void OnBecomeVisible () {
	void Update (){
		tr.Rotate (Vector3.down * Time.deltaTime *100);
		//transform.position=  Vector3.MoveTowards(transform.position.x, Time.deltaTime*2, transform.position.z);
	
	}
}
