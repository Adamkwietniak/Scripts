using UnityEngine;
using System.Collections;

public class CheckAboutPositionScript : MonoBehaviour {

	SprawdzTerenScript sts;
	PlayerHealth ph;
	private Terrain terr;
	private float maxDist = 15.0f;
	private AudioSource audioS;
	//private Transform thisTR;
	public bool turnOnMusic = false;
	private Transform brumTR;
	private float lastDist = 0;
	// Use this for initialization
	void Awake ()
	{
		//thisTR = this.GetComponent<Transform> ();
		sts = GetComponentInParent<SprawdzTerenScript>();
		audioS = this.GetComponent<AudioSource> ();
		ph = this.GetComponentInChildren<PlayerHealth> ();
		brumTR = this.transform.parent.GetComponent<Transform> ();
		//audioS.Stop ();
	}
	void Start ()
	{
		terr = sts.terrain;
		if (audioS.isPlaying == true)
			audioS.Stop ();
	}
	void Update ()
	{
		if (turnOnMusic == true) {
			float dist;
			bool isPlay = false;
			dist = Mathf.Abs (brumTR.position.y - terr.SampleHeight (brumTR.position));
			if (dist > maxDist)
				isPlay = true;
			if (audioS.isPlaying == false && isPlay == true) {
				audioS.Play ();
			}
			if (dist > maxDist * 5 && brumTR.position.y < -50 && isPlay == true) {
				ph.GameOver ();
			}
			if (dist <= maxDist && lastDist > dist) {
				turnOnMusic = false;
			}
			lastDist = dist;
		} else if (turnOnMusic == false && audioS.isPlaying == true) {
			audioS.Stop ();
			lastDist = 0;
		}
		/*if (Input.GetKeyDown (KeyCode.G))
			brumTR.position = new Vector3 (brumTR.position.x, brumTR.position.y+25f, brumTR.position.z);*/
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
		if (sts.kolizjaZCzymkolwiek == true) {
			sts.kolizjaZCzymkolwiek = false;
			turnOnMusic = true;
		}
		//if (turnOnMusic == false)
			
	}

}

