using UnityEngine;
using System.Collections;

public class CheckAboutPositionScript : MonoBehaviour {

	SprawdzTerenScript sts;
	// Use this for initialization
	void Awake ()
	{
		sts = GetComponentInParent<SprawdzTerenScript>();
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Teren" && sts.kolizjaZObjektem == true && (other.tag != "Untagged" || other.tag != "ObstacleTag"))
			sts.kolizjaZObjektem = false;
		else if(other.tag != "Teren" && sts.kolizjaZObjektem == false && (other.tag == "Untagged" || other.tag == "ObstacleTag")){
			sts.kolizjaZObjektem = true;
			sts.gejmObject = other.gameObject;
		}
		if((other.tag == "Teren" || other.tag == "Untagged" || other.tag == "ObstacleTag") && sts.kolizjaZCzymkolwiek == false)
			sts.kolizjaZCzymkolwiek = true;
		
	}
	void OnTriggerExit(Collider other)
	{
		if (sts.kolizjaZCzymkolwiek == true) {
			sts.kolizjaZCzymkolwiek = false;
		}
	}
}
