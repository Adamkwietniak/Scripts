using UnityEngine;
using System.Collections;

public class CheckAboutPositionScript : MonoBehaviour {

	SprawdzTerenScript sts;
	private Terrain terr;
	private float maxDist = 25f;
	private AudioSource audioS;
	private Transform thisTR;
	// Use this for initialization
	void Awake ()
	{
		thisTR = this.GetComponent<Transform> ();
		sts = GetComponentInParent<SprawdzTerenScript>();
		terr = sts.terrain;
		audioS = this.GetComponent<AudioSource> ();
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
		float dist;
		if (sts.kolizjaZCzymkolwiek == true) {
			sts.kolizjaZCzymkolwiek = false;
		}
		dist = Mathf.Abs(thisTR.position.y - terr.SampleHeight (thisTR.position));
		if (dist > maxDist) {
			if (audioS.isPlaying == false) {
				audioS.Play ();
			}
		}
	}
}
