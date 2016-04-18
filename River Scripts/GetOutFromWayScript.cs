using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetOutFromWayScript : MonoBehaviour {

	private List<ObstaclesTr> obsTr = new List<ObstaclesTr> ();
	private GameObject [] objGameObstacle = new GameObject[22];
	SprawdzTerenScript sts;
	public int[] indexesOfTexturesToMS;  //Przypisujesz indexy tekstur na terenie, z których trzeba usuwać przeszkody
	public GameObject arrowPrefab;			//Prefab strzałki który będzie duplikowany na każda przeszkode
	private GameObject [] arrowS = new GameObject[22];
	private Transform [] arrowSTr = new Transform[22];
	[HideInInspector]public bool reachBase = false; // czy dotarles do bazy
	private bool enabledArrows = false;
	public int maxItemsToAwayFromWay = 10;
	private int helpToCount = 0;
	private bool assignCount = false;
	public Text text;
	private bool boolMissionComplete = false;
	MissionRiverScript mrs;
	[HideInInspector] public bool isComplete = false;
	void Awake ()
	{
		helpToCount = maxItemsToAwayFromWay;
	}
	void Start () {
		sts = (SprawdzTerenScript)FindObjectOfType (typeof(SprawdzTerenScript));
		objGameObstacle = GameObject.FindGameObjectsWithTag("ObstacleTag");
		mrs = (MissionRiverScript)FindObjectOfType(typeof(MissionRiverScript));
		for(int i = 0; i < objGameObstacle.Length; i++)
		{
			obsTr.Add (new ObstaclesTr (objGameObstacle[i].GetComponent<Transform>(), objGameObstacle[i].transform.GetChild(0).transform.GetComponent<Transform>(),
			false));
		}
		for (int i = 0; i < obsTr.Count; i++) {
			obsTr[i].isMc = false;
			arrowS [i] = (GameObject)Instantiate (arrowPrefab, new Vector3(obsTr[i].dustTr.position.x, obsTr[i].dustTr.position.y+4.0f, obsTr[i].dustTr.position.z), 
												 Quaternion.Euler(0,0,0));
			//}
		}
		for (int i = 0; i < arrowS.Length; i++) {
			arrowSTr[i]=arrowS[i].GetComponent<Transform>();
			arrowS [i].SetActive (false);
		}
		maxItemsToAwayFromWay = arrowSTr.Length;
	}

	void Update ()
	{
		if(mrs.firstOfSecondScript == true){
			if (Input.GetKeyDown (KeyCode.Alpha0))
				maxItemsToAwayFromWay = 0;
			if (reachBase == true) {
				//1razowe wlaczenie wszystkich strzalek
				if (enabledArrows == false) {
					for (int i = 0; i < arrowS.Length; i++) {
						arrowS [i].SetActive (true);
					}
					text = text.GetComponent<Text> ();
					enabledArrows = true;
				}
				//obsluga strzalek
				if (IsEnd () == false) {
					for (int i = 0; i < obsTr.Count; i++) {
						CoutPositionOfTree (i);
						if(obsTr[i].isMc == false && maxItemsToAwayFromWay >= 0)
						{
							arrowSTr[i].position = new Vector3(obsTr[i].dustTr.position.x, obsTr[i].dustTr.position.y+4.0f, obsTr[i].dustTr.position.z);
						}
					}
					//text.text = string.Format ("{00}", maxItemsToAwayFromWay);
					if(maxItemsToAwayFromWay != helpToCount && assignCount == false){
						maxItemsToAwayFromWay = helpToCount;
						assignCount = true;
					}
					text.text = (maxItemsToAwayFromWay.ToString () + " Obstacles to go.");
				}
			}
			if (maxItemsToAwayFromWay <= 0) {
				maxItemsToAwayFromWay = 0;
				DisableAllArrows ();
			}
		}
	}
	private void CoutPositionOfTree (int ind)
	{
		for (int i = 0; i < indexesOfTexturesToMS.Length; i++) {
			//Debug.Log ("Wszedlem w sprawdzanie tekstury o intexie: "+indexesOfTexturesToMS[i]+" i nazwie: "+ sts.czyKurz [indexesOfTexturesToMS[i]].tekstura.name);
			if (!sts.czyKurz [indexesOfTexturesToMS[i]].tekstura.name.Equals (PowierzchniaTerenu.NazwaTeksturyWPozycji (obsTr[ind].obstaclesTr.position)) &&
				!sts.czyKurz [indexesOfTexturesToMS[i]].tekstura.name.Equals (PowierzchniaTerenu.NazwaTeksturyWPozycji (obsTr[ind].dustTr.position))
				&& obsTr[ind].isMc == false) {
				DisableArrows (ind);
			} 
		}
	}
	private bool IsEnd()
	{
		for (int i = 0; i < obsTr.Count; i++) {
			if (obsTr[i].isMc == false) {
				return false;
				break;
			}
			
		}
		return true;
	}
	private void DisableArrows (int idx)
	{
		if(obsTr[idx].isMc == false)
			obsTr[idx].isMc = true;
		arrowS [idx].SetActive (false);
		maxItemsToAwayFromWay = maxItemsToAwayFromWay - 1;

	}
	private void DisableAllArrows ()
	{
		for(int i = 0; i < arrowS.Length; i++)
		{
			arrowS[i].SetActive(false);
			if(obsTr[i].isMc == false)
				obsTr[i].isMc = true;
		}
		boolMissionComplete = true;
		isComplete = true;
	}
}
public class ObstaclesTr 
{
	[HideInInspector]public Transform obstaclesTr;
	[HideInInspector]public Transform dustTr;
	[HideInInspector]public bool isMc;

	public ObstaclesTr(Transform ostrt, Transform dTr, bool isMcc)
	{
		this.obstaclesTr = ostrt;
		this.dustTr = dTr;
		this.isMc = isMcc;
	}
}
