using UnityEngine;
using System.Collections;

public class HelperDustInDynamicScript : MonoBehaviour {

	DustFromDynamicObiect dfdo;
	private bool chiki = false;
	private GameObject trapObj;
	// Use this for initialization
	void Start () {
		trapObj = GameObject.Find("TrapsMove");
		dfdo = trapObj.GetComponent<DustFromDynamicObiect>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Teren")
		{
			for(int i = 0; i < dfdo.collList.Count; i++)
			{
				if(dfdo.collList[i].idx == this.gameObject.name)
				{
					dfdo.collList[i].isDusting = true;
				}
			}
		}
	}
	void OnTriggerExit (Collider other)
	{
		if(other.tag == "Teren")
		{
			for(int i = 0; i < dfdo.collList.Count; i++)
			{
				if(dfdo.collList[i].idx == this.gameObject.name)
				{
					dfdo.collList[i].isDusting = false;
				}
			}
		}
	}
}
