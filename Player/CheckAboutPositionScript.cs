using UnityEngine;
using System.Collections;

public class CheckAboutPositionScript : MonoBehaviour {

	SprawdzTerenScript sts;
	PlayerHealth ph;
	private Terrain terr;
	private float maxDist = 25f;
	private AudioSource audioS;
	private Transform thisTR;
	private bool turnOnMusic = false;
	// Use this for initialization
	void Awake ()
	{
		thisTR = this.GetComponent<Transform> ();
		sts = GetComponentInParent<SprawdzTerenScript>();
		audioS = this.GetComponent<AudioSource> ();
		ph = this.GetComponentInChildren<PlayerHealth> ();
	}
	void Start ()
	{
		terr = sts.terrain;
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
		if (turnOnMusic == true)
			turnOnMusic = false;
	}
	void OnTriggerExit(Collider other)
	{
		float dist;
		if (sts.kolizjaZCzymkolwiek == true) {
			sts.kolizjaZCzymkolwiek = false;
		}
		dist = Mathf.Abs(thisTR.position.y - terr.SampleHeight (thisTR.position));
		if (dist > maxDist) {
			if (turnOnMusic == false)
				turnOnMusic = true;
			if (audioS.isPlaying == false && turnOnMusic == true) {
				audioS.Play ();
			}
			if (dist > maxDist * 5 && thisTR.position.y < -50) {
				ph.GameOver ();
			}
		} else if (dist <= maxDist && turnOnMusic == true) {
			turnOnMusic = false;
			if (audioS.isPlaying == true)
				audioS.Stop ();
		}
	}

}
