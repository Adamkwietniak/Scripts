using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleTagScript : MonoBehaviour {


	public float distanceFromTheTarget = 50; // Warunek max dystansu do obliczania predkosci kątowej i poruszania sie
	public float speedOfTime = 1; // Mnożnik timera
	public int clauseOfSpeedTime = 1; // warunek koncowy do Timera

	[HideInInspector]public int ss = 0;
	private int ss1 = 0;
	private float timer = 0;
	//valueObj inst = new valueObj ();
	public List<valueObjt> obstacleTab = new List<valueObjt> ();
	private GameObject[] obstacleTab2 = new GameObject[50];
	private bool [] pomocniczaBool = new bool[50];
	private Transform trs;
	private bool Iteration = true;
	private bool vSpeed = false;
	RCCCarControllerV2 rcc;
	
	// Use this for initialization
	void Awake ()
	{
		obstacleTab2=GameObject.FindGameObjectsWithTag("ObstacleTag");
	}
	void Start () {
		trs = this.GetComponent<Transform> ();
		SortTab ();
		AssignValue ();
		for (int i = 0; i < pomocniczaBool.Length; i++) {
			pomocniczaBool [i] = false;
		}
		/*foreach (valueObjt vo in obstacleTab) {
			Debug.Log(vo.gmob + "bool: " + vo.iSleep);
		}*/
		ss = obstacleTab.Count;
		ss1 = ss - 1;
		rcc = GetComponent<RCCCarControllerV2> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StartBool ())
			Started ();

		if (timer < clauseOfSpeedTime) {
			timer += Time.deltaTime * speedOfTime;
		}
		else
			timer = 0;
	}
	private void Started ()
	{
		for (int i = 0; i < ss; i++) {
			if(pomocniczaBool[i] == false && obstacleTab[i].rb.IsSleeping()==false && obstacleTab[i].useCount == false)
			{
				pomocniczaBool[i] = true;
				obstacleTab[i].iSleep = false;
			}
			Iteration = true;
			if (timer >= clauseOfSpeedTime) {
					if (pomocniczaBool [i] == true && Iteration == true) {
						obstacleTab [i].Distance = Vector3.Distance (this.trs.position, obstacleTab [i].trans.position);
						vSpeed = true;
					}
					if (obstacleTab [i].Distance < distanceFromTheTarget && vSpeed == true) {
						if (pomocniczaBool [i] == true) {
							Calculate (i);
							obstacleTab[i].useCount = true;
						}
					} else if (pomocniczaBool [i] == true && obstacleTab [i].Distance > distanceFromTheTarget && obstacleTab [i].iSleep == false && obstacleTab [i].useCount == true) {
						ResetTab (i);
						obstacleTab [i].iSleep = true;
						obstacleTab[i].useCount = false;
					}
				Iteration = false;
				vSpeed = false;
			}
		}
		//AttendanceMovedItems ();
	}
	private void AttendanceMovedItems ()
	{
		if (timer >= clauseOfSpeedTime) {
			for (int i = 0; i < ss; i++) {
				if (pomocniczaBool [i] == true && Iteration == true) {
					obstacleTab [i].Distance = Vector3.Distance (this.trs.position, obstacleTab [i].trans.position);
					vSpeed = true;
					//Debug.Log ("Dystans do bruma obiektu: "+obstacleTab[i].gmob.name+" wynosi: "+obstacleTab[i].Distance);
				}
				//for (int j = 0; j < ss; j++) {
					if (obstacleTab [i].Distance < distanceFromTheTarget && vSpeed == true) {
						if (pomocniczaBool [i] == true && rcc.speed < 15) {
							Calculate (i);
							obstacleTab[i].useCount = true;
						}
					} else if (pomocniczaBool [i] == true && obstacleTab [i].Distance > distanceFromTheTarget && obstacleTab [i].iSleep == false && obstacleTab [i].useCount == true) {
						ResetTab (i);
						obstacleTab [i].iSleep = true;
						obstacleTab[i].useCount = false;
					}
				//}
			}
			Iteration = false;
			vSpeed = false;
		}
	}
	private void Calculate (int swit)
	{
		obstacleTab [swit].angularV = Mathf.Abs(obstacleTab [swit].rb.angularVelocity.magnitude);
		obstacleTab [swit].speedD = Mathf.Abs(obstacleTab [swit].rb.velocity.magnitude);

	}
	private bool StartBool ()
	{
		for (int i = 0; i < ss; i++) {
			if(obstacleTab[i].rb.IsSleeping() == false)
				return true;
		}
		return false;
	}
	private void ResetTab (int z)
	{
		//if (pomocniczaBool.Length > 0) {
		if(obstacleTab[z] != null && (obstacleTab[z].angularV == 0 && obstacleTab[z].speedD == 0)){
			pomocniczaBool[z] = false;
			obstacleTab[z].iSleep = true;
				//Debug.Log ("Obiekt o nazwie " + obstacleTab[z].gmob.name + " zostal zresetowany");		
			}
		//}

	}
	private void ResetAllTab ()
	{
		for (int i = 0; i < ss; i++) {
			if(pomocniczaBool[i] == true)
			{
				pomocniczaBool[i] = false;
			}

		}
		//Debug.Log ("Tablica pomocniczaBool zostala zresetowana");
	}
	/// <summary>
	/// S////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	/// </summary>
	private void SortTab ()
	{
		if(obstacleTab2[0].name == obstacleTab2[1].name){
			obstacleTab2[1] = null;
		}
		for (int i = 1; i <  obstacleTab2.Length - 1; i++) {
			
			if(obstacleTab2[i].name == obstacleTab2[i+1].name){
				obstacleTab2[i] = null;
			}
			if(obstacleTab2[i] == null && obstacleTab2[i+1] != null)
			{
				obstacleTab2[i] = obstacleTab2[i+1];
			}
			else if(obstacleTab2[i] == null && obstacleTab2[i+1] == null){
				for (int j = 1; j <  obstacleTab2.Length; j++){
					if(obstacleTab2[i+j] != null && obstacleTab2[i].name != obstacleTab2[j].name)
						obstacleTab2[i] = obstacleTab2[i+j];
				}
			}
		}
	}
	
	private void AssignValue()
	{
		for (int i = 0; i < obstacleTab2.Length; i++) {
			if(obstacleTab2[i] != null){
				obstacleTab.Add (new valueObjt (obstacleTab2[i], obstacleTab2[i].GetComponent<Transform>(), obstacleTab2[i].GetComponent<Rigidbody>(), 0, 0,1000, true, false));
			}else
				break;
		}
	}

}



