﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;


public class AttendanceEnemy : MonoBehaviour {
	//Podstawowe zmienne
	public List<EnemyClassScript> npcEnemy = new List<EnemyClassScript>();

	private GameObject[] tempEnemyToWriteList = new GameObject [100];
	[HideInInspector]public List<EnemyClassScript> mainEnemy = new List<EnemyClassScript>();
	[HideInInspector]public bool boolsDest = false;
	private Transform driverTr;

	//Zmienne do wyznaczenia i obslugi kollizji
	[HideInInspector]public bool collDetect = false;
	[HideInInspector]public string trigerDetection = "none";
	private bool timerWakeStart = false;
	private bool timerBoolToNextStage = false;

	public float runSpeed = 7F;
	public float walkSpeed = 3.5F;
	public float multiplerOfWakeUp = 1; // mnożnik do timera wstawania
	public int maxSpeedToNotDead = 30;
	public int endTimeToGetUp = 45;
	public int multiForFarquencyHp = 1;//mnoznik Timera zwiekszajacego czestotliwosc ubytku hp
	public float distanceToStop = 2;
	[HideInInspector]public bool isShooterNow = false;

	private GameObject brumBrum;

	private float timerToNextStage = 0;
	private float timerToHp;
	private Transform target;
	//zalaczone skrypty
	RCCCarControllerV2 rcv2;
	PlayerHealth ph;
	private Transform[] tankAndShooter = new Transform[2];
	private bool changedTarget = false;
	public bool takeDmg = true;

	public AudioClip soundShoot;

	void Start ()
	{
		if(brumBrum == null)
			brumBrum = GameObject.Find ("BrumBrume");
		driverTr = GameObject.Find("Kierowca").GetComponent<Transform>();
		target = brumBrum.GetComponent<Transform> ();
		rcv2 = brumBrum.GetComponent<RCCCarControllerV2>();
		ph = brumBrum.GetComponentInChildren<PlayerHealth> ();
		tempEnemyToWriteList = GameObject.FindGameObjectsWithTag ("enemy");
	
		for (int i = 0; i < tempEnemyToWriteList.Length; i++) {
			for(int j = 0; j < npcEnemy.Count; j++)
			{
				if(tempEnemyToWriteList[i].gameObject.name == npcEnemy[j].enemyObject.name && tempEnemyToWriteList[i].gameObject.name != null){
					mainEnemy.Add(new EnemyClassScript(npcEnemy[j].enemyObject.GetComponent<Animator>(), npcEnemy[j].enemyObject.GetComponent<NavMeshAgent>(), 
					                                   npcEnemy[j].enemyObject, npcEnemy[j].defaultPositionObject,npcEnemy[j].maxDistance, npcEnemy[j].minDistance, 
					                                   npcEnemy[j].maxDistanceFromDefaultPosition, npcEnemy[j].dmgFromShoot, npcEnemy[j].enemyIsStatic, false, false, 
						false, true, true, false, false, 0, 0, 2000, 0, 0, 0, tempEnemyToWriteList[i].GetComponentsInChildren<Rigidbody>(), FunToMakeTags (npcEnemy[j].enemyObject), 
						npcEnemy[j].defaultPositionObject.GetComponent<Transform>(), npcEnemy[j].enemyObject.GetComponent<Transform>(), 
						npcEnemy[j].enemyObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetChild(1).GetChild(1).gameObject,
					                                   npcEnemy[j].enemyObject.GetComponent<AudioSource>()
					                                 	));
				}
			}
		}
		/*for (int i = 0; i < mainEnemy.Count; i++) {
			for (int j = 0; j < mainEnemy [i].obiectToChangeTag.Length; j++) {
				//if (npcEnemy [i].obiectToChangeTag [j] != null) {
				//	npcEnemy [i].obiectToChangeTag [j] = npcEnemy [i].rigbodyList [j].gameObject;
				Debug.Log (mainEnemy [i].obiectToChangeTag [j].name);
				//}
			}
		}*/
		foreach (EnemyClassScript ecs in mainEnemy) {
			for (int i = 0; i < ecs.rigbodyList.Length; i++)
			{
				
				ecs.rigbodyList[i].useGravity = true;
				ecs.rigbodyList[i].isKinematic = true;
			}
			ecs.agent.stoppingDistance = distanceToStop;
			ecs.audiosorce.maxDistance = ecs.minDistance;
		}
		if (brumBrum.GetComponent<HeheScript> ().enabled == true) {
			tankAndShooter [0] = GameObject.Find ("BattleTank Green").GetComponent<Transform> ();
			tankAndShooter [1] = GameObject.Find ("CameraOfMinigun").GetComponent<Transform> ();
		} else {
			tankAndShooter [0] = target;
			tankAndShooter [1] = driverTr;
		}
		for (int i = 0; i < mainEnemy.Count; i++) { 
			mainEnemy [i].PartSys.SetActive (false);
			//Debug.Log ("Wylaczylem part sys dla " + mainEnemy [i].enemyObject);
		}
	}
	/*private GameObject FindPartSys (GameObject ojciec)
	{
		//Transform tr = ojciec.transform.Find ("Particle System");
		//GameObject go = tr.gameObject;
		return ojciec.transform.FindChild ("Particle System").gameObject;
	}*/
	private GameObject [] FunToMakeTags (GameObject obj)
	{
		Rigidbody[] helpRb = new Rigidbody[13];
		GameObject[] tabObj = new GameObject[11];
		helpRb = obj.GetComponentsInChildren<Rigidbody> ();
		for (int i = 0; i < helpRb.Length; i++) {
			tabObj [i] = helpRb [i].gameObject;
		}
		return tabObj;
	}
	void Update ()
	{
		if (collDetect == true) {
			
			AttendanceCollision();
			}

		if (isShooterNow == true) {
			if (takeDmg == true) {
				LowDMG ();
				takeDmg = false;
			}
			AttendanceEvent (tankAndShooter[0], tankAndShooter[1]);
		} else {
			StartCoroutine (AttendanceUpdate ());
		}
		StartCoroutine (AttendanceAnim ());
	}
	private IEnumerator AttendanceUpdate ()
	{
		yield return new WaitForSeconds (2);
		AttendanceEvent (target, driverTr);
		CheckGetUp ();
	}
	private IEnumerator AttendanceAnim ()
	{
		yield return new WaitForSeconds (1);
		AnimationDoIt ();
	}
	public int EnemyIsShooted (string targetOfMinigun, string nameOfRb, Vector3 normal, float multPow, int maxToKill)
	{
		for (int i = 0; i < mainEnemy.Count; i++) {
			if (mainEnemy [i].isLife == true) {
				if (mainEnemy [i].enemyObject.name == targetOfMinigun) {

					mainEnemy [i].anim.enabled = false;
					SimpleEnableRagdoll (i);
					mainEnemy [i].PartSys.SetActive (false);
                    for (int j = 0; j < mainEnemy[i].rigbodyList.Length; j++)
                    {
                        
                        if (mainEnemy[i].rigbodyList[j].gameObject.name == nameOfRb)
                        {
                            mainEnemy[i].rigbodyList[j].AddForce(-normal.x* multPow, normal.y, -normal.z* multPow, ForceMode.Impulse);
                        }
                    }
                    mainEnemy[i].isLife = false;
                    mainEnemy [i].agent.enabled = false;
					mainEnemy [i].isShooting = false;
					mainEnemy [i].wantGetUp = false;
					mainEnemy [i].isWalk = false;
					mainEnemy [i].isRun = false;
					StartCoroutine (DisableColliders (i));
					maxToKill++;
				}
			}
		}
		return maxToKill;
	}
	private IEnumerator DisableColliders (int i)
	{
		yield return new WaitForSeconds (5);
		if (mainEnemy [i].isLife == false) {
			for (int j = 0; j < mainEnemy [i].obiectToChangeTag.Length; j++) {
				mainEnemy [i].obiectToChangeTag [j].GetComponent<Collider> ().enabled = false;
				mainEnemy [i].rigbodyList [j].isKinematic = true;
			}
		}
	}
	private void ShootSound (int i)
	{
		if (mainEnemy [i].isShooting == true) {
			mainEnemy[i].audiosorce.clip = soundShoot;
			if(mainEnemy[i].audiosorce.isPlaying != true)
				mainEnemy[i].audiosorce.Play();
			
		}
	}
	private void MuteSoundShoot (int i)
	{
		if (mainEnemy [i].isShooting == false && mainEnemy[i].audiosorce.isPlaying == true) {
			mainEnemy[i].audiosorce.clip = soundShoot;
			mainEnemy[i].audiosorce.Stop();

		}
	}
	private void AnimationDoIt ()
	{
		for (int i = 0; i < mainEnemy.Count; i++) {
			if(mainEnemy[i].isLife == true && mainEnemy[i].anim.GetFloat("Speedy") != mainEnemy[i].actualSpeed && mainEnemy[i].isShooting == false)
			{
				mainEnemy[i].anim.SetFloat("Speedy", mainEnemy[i].actualSpeed);
			}
			if(mainEnemy[i].isLife == true && mainEnemy[i].isShooting == true && mainEnemy[i].wantGetUp == false)
			{
				mainEnemy[i].anim.SetBool("isShoot",mainEnemy[i].isShooting);
				mainEnemy[i].timerToShoot +=Time.deltaTime*multiForFarquencyHp;
				if(mainEnemy[i].timerToShoot > 5)
				{
					ph.currentHealth -= mainEnemy[i].dmgFromShoot;
					mainEnemy[i].timerToShoot = 0;
				}
				else if(mainEnemy[i].timerToShoot>6)
				{
					mainEnemy[i].timerToShoot = 0;
				}
			}
			else if(mainEnemy[i].isLife == true && mainEnemy[i].isShooting == false && mainEnemy[i].wantGetUp == false && mainEnemy[i].actualInt != 2)
			{
				mainEnemy[i].anim.SetBool("isShoot",mainEnemy[i].isShooting);
				if(mainEnemy[i].audiosorce.volume == 0)
					MuteSoundShoot (i);
			}
		}
	}
	private void GetUp ()
	{
		if (timerWakeStart == true) {
			for(int i = 0; i < mainEnemy.Count; i++)
			{
				if(mainEnemy[i].wantGetUp == true)
					mainEnemy[i].timerToGetUp += Time.deltaTime*multiplerOfWakeUp;
				if(mainEnemy[i].timerToGetUp >= endTimeToGetUp && mainEnemy[i].wantGetUp == true)
				{
					mainEnemy[i].wantGetUp = false;
					mainEnemy[i].anim.enabled = true;
					mainEnemy[i].onTheGround = false;
					mainEnemy[i].timerToGetUp = 0;
					SimpleDisableRagdoll(i);
					AttendanceEvent(target, driverTr);
				}
			}
		}
	}
	private void CheckGetUp()
	{
		if (timerWakeStart == true) {
			for (int i = 0; i < mainEnemy.Count; i++)
			{
				if (mainEnemy [i].wantGetUp == true) {
					GetUp ();
					break;
				}
				else
				{
					timerWakeStart = false;
				}
			}
		}
	}
	public void CheckEnemyOne ()
	{
		for (int i = 0; i < mainEnemy.Count; i++) {
			if (mainEnemy [i].agent.SetDestination (target.position) == true)
				break;
			else
				boolsDest = false;
		}
	}

	private void AttendanceEvent (Transform target1, Transform driverTr1)
	{
		for (int i = 0; i < mainEnemy.Count; i++)
		{
			if(mainEnemy[i].isLife == true){
				CountDistanceFromPlayer (i, target1);

					if(mainEnemy[i].enemyIsStatic == false){
					switch (mainEnemy [i].actualInt)
					{
					case 0:			//Idle Domyslne
						mainEnemy[i].agent.speed = 0;
						mainEnemy[i].actualSpeed = 0;
						DisEnbpPartSystem(i, false);

						break;
					case 1:			//Wrog jest w odleglosci umozliwiajacej rozpoczecie ostrzalu
						mainEnemy [i].agent.speed = 0;
						mainEnemy [i].actualSpeed = 0;
						if (mainEnemy [i].isShooting == true) {
							mainEnemy [i].enemyObject.transform.LookAt (driverTr1.position);
							DisEnbpPartSystem (i, true);
							ShootSound (i);
						}
						break;
					case 2:			// Wrog jest miedzy predkoscia minimalna a maksymalna (rozpoczyna ruch)
						if(runSpeed>mainEnemy[i].agent.speed)
							mainEnemy[i].actualSpeed += Time.deltaTime*mainEnemy[i].agent.acceleration;
						if(runSpeed<mainEnemy[i].agent.speed)
							mainEnemy[i].agent.speed = runSpeed;

						mainEnemy[i].agent.speed = mainEnemy[i].actualSpeed;
						mainEnemy[i].agent.SetDestination(target1.position);
						DisEnbpPartSystem(i, false);
						boolsDest = true;

						break;
					case 3:			//Wrog jest poza widokiem bruma staje i wlacza iddle
						mainEnemy[i].agent.speed = 0;
						mainEnemy[i].actualSpeed = 0;
						DisEnbpPartSystem(i, false);

						break;
					case 4:			// Wrog zaczyna isc w strone swojego punktu defaultowego (trzeba dodac timer do przejscia do 5)
						if(walkSpeed>mainEnemy[i].agent.speed)
							mainEnemy[i].actualSpeed += Time.deltaTime*(mainEnemy[i].agent.acceleration/2);
						if(walkSpeed<mainEnemy[i].agent.speed)
							mainEnemy[i].agent.speed = walkSpeed;
						mainEnemy[i].agent.speed = mainEnemy[i].actualSpeed;
						mainEnemy[i].agent.SetDestination(mainEnemy[i].transDefPos.position);
						DisEnbpPartSystem(i, false);
						boolsDest = true;

						break;
					case 5:			//Wrog wlacza iddle na miejscu "zbiorki"
						mainEnemy [i].agent.speed = 0;
						mainEnemy [i].actualSpeed = 0;
						DisEnbpPartSystem (i, false);
						break;
					}
				}else{
					mainEnemy[i].agent.speed = 0;
					switch (mainEnemy [i].actualInt){
					case 1:			//Strzal
						if(mainEnemy[i].isShooting == true){
							mainEnemy[i].enemyObject.transform.LookAt(target1.position);
							DisEnbpPartSystem(i, true);
							ShootSound (i);
						}

						break;
					
					default:		//Iddle
						DisEnbpPartSystem(i, false);

						break;

					}
				}
			}
		}
	}
	private void CountDistanceFromPlayer (int i, Transform target11)
	{
		//for (int i = 0; i < mainEnemy.Count; i++) {
				mainEnemy [i].distFromPlayer = ContDistance (target11.position, mainEnemy [i].enemyTr.position);
			//if (mainEnemy [i].enemyObject.name == "Army3-finalReady (3)")
			
			if (mainEnemy [i].enemyIsStatic == false){
				mainEnemy [i].distFromDefPos = ContDistance (mainEnemy[i].enemyTr.position, mainEnemy [i].transDefPos.position);
			}
				if (mainEnemy [i].distFromPlayer <= mainEnemy [i].minDistance && mainEnemy [i].distFromDefPos < mainEnemy [i].maxDistanceFromDefaultPosition && mainEnemy[i].onTheGround == false && ph.currentHealth > 0)
				{
				if (mainEnemy [i].actualInt != 1) {
					mainEnemy [i].actualInt = 1;
					mainEnemy [i].isRun = false;
					mainEnemy [i].isShooting = true;
					mainEnemy [i].isWait = false;
					mainEnemy [i].isWalk = false;
				}
					//return 1; //Wrog jest w odleglosci umozliwiajacej rozpoczecie ostrzalu
				}
			else if (mainEnemy [i].distFromPlayer < mainEnemy [i].maxDistance && mainEnemy [i].distFromPlayer > mainEnemy [i].minDistance && mainEnemy [i].distFromDefPos < mainEnemy [i].maxDistanceFromDefaultPosition && mainEnemy[i].onTheGround == false)
				{
				if (mainEnemy [i].actualInt != 2) {
					mainEnemy [i].actualInt = 2;
					mainEnemy [i].isRun = true;
					mainEnemy [i].isShooting = false;
					mainEnemy [i].isWait = false;
					mainEnemy [i].isWalk = false;
				}
					//return 2; // Wrog jest miedzy predkoscia minimalna a maksymalna (rozpoczyna ruch)
				}
			else if (mainEnemy [i].distFromPlayer > mainEnemy [i].maxDistance && mainEnemy [i].distFromPlayer < mainEnemy [i].maxDistanceFromDefaultPosition && mainEnemy [i].distFromDefPos < mainEnemy [i].maxDistanceFromDefaultPosition && mainEnemy[i].onTheGround == false)
				{
				if (mainEnemy [i].actualInt != 3) {
					mainEnemy [i].actualInt = 3;
					mainEnemy [i].isRun = false;
					mainEnemy [i].isShooting = false;
					mainEnemy [i].isWait = true;
					mainEnemy [i].isWalk = false;
				}
					//return 3; //Wrog jest poza widokiem bruma staje i wlacza iddle
				}
			else if (mainEnemy [i].distFromPlayer > mainEnemy [i].maxDistance && mainEnemy [i].distFromDefPos >= distanceToStop  && mainEnemy [i].enemyIsStatic == false && mainEnemy[i].onTheGround == false)
				{
				if (mainEnemy [i].actualInt != 4) {
					mainEnemy [i].actualInt = 4;
					mainEnemy [i].isRun = false;
					mainEnemy [i].isShooting = false;
					mainEnemy [i].isWait = false;
					mainEnemy [i].isWalk = true;
				}
					//return 4; // Wrog zaczyna isc w strone swojego punktu defaultowego
				}
			else if (mainEnemy [i].distFromPlayer > mainEnemy [i].maxDistance && mainEnemy [i].distFromDefPos < distanceToStop && mainEnemy[i].onTheGround == false)
				{
					if (mainEnemy [i].actualInt != 5) {
						mainEnemy [i].actualInt = 5;
						mainEnemy [i].isRun = false;
						mainEnemy [i].isShooting = false;
						mainEnemy [i].isWait = true;
						mainEnemy [i].isWalk = false;
					}
					//return 5; //Wrog wlacza iddle na miejscu "zbiorki"
				}
		//}
		//return 0;
	}
	private float ContDistance (Vector3 a, Vector3 b)
	{
		//return (a - b).magnitude;
		return Vector3.Distance (a, b);
	}
	private void SimpleEnableRagdoll(int index)
	{
		 if (mainEnemy [index].isLife == true) {
			for(int i = 0; i < mainEnemy [index].rigbodyList.Length; i++)
			{
				mainEnemy [index].rigbodyList[i].isKinematic = false;
			}
		}

	}
	private void DisEnbpPartSystem (int i, bool pass)
	{
		if (pass == true) 
			mainEnemy[i].PartSys.SetActive(true);
		if (pass == false)
			mainEnemy [i].PartSys.SetActive (false);
	}
	private void SimpleDisableRagdoll(int index)
	{
		if (mainEnemy [index].isLife == true) {
			for(int i = 0; i < mainEnemy [index].rigbodyList.Length; i++)
			{
				mainEnemy [index].rigbodyList[i].isKinematic = true;
			}
		}

	}
	private void AllEnableRagdoll ()
	{
		foreach (EnemyClassScript ecs in mainEnemy) {
			if(ecs.isLife == true)
			{
				for(int i = 0; i < ecs.rigbodyList.Length; i++)
				{
					ecs.rigbodyList[i].isKinematic = false;
				}
			}
		}

	}
	private void AllDisableRagdoll ()
	{
		foreach (EnemyClassScript ecs in mainEnemy) {
			if(ecs.isLife == true)
			{
				for(int i = 0; i < ecs.rigbodyList.Length; i++)
				{
					ecs.rigbodyList[i].isKinematic = true;
				}
			}
		}
		
	}
	private void AttendanceCollision()
	{
		

		for (int i = 0; i < mainEnemy.Count; i++) {
			if(mainEnemy[i].enemyObject.name == trigerDetection)
			{
				
				if(rcv2.speed > maxSpeedToNotDead)
				{
					mainEnemy[i].anim.enabled = false;
					SimpleEnableRagdoll(i);
					mainEnemy[i].isShooting = false;
					mainEnemy[i].wantGetUp = false;
					mainEnemy[i].isLife = false;
					mainEnemy[i].isRun = false;
					mainEnemy[i].isWalk = false;
					mainEnemy[i].isWait = false;
					mainEnemy[i].onTheGround = true;
					DisEnbpPartSystem (i, false);
					for (int j = 0; j < mainEnemy [i].obiectToChangeTag.Length; j++) {
						mainEnemy [i].obiectToChangeTag [j].tag = "NonCollider";
					}
					mainEnemy [i].audiosorce.Stop ();
				}
				else
				{
					
					mainEnemy[i].anim.enabled = false;
					SimpleEnableRagdoll(i);
					mainEnemy[i].timerToGetUp = 0;
					timerWakeStart = true;
					mainEnemy[i].isShooting = false;
					mainEnemy[i].wantGetUp = true;
					mainEnemy[i].isRun = false;
					mainEnemy[i].isWalk = false;
					mainEnemy[i].isWait = false;
					mainEnemy[i].onTheGround = true;
					DisEnbpPartSystem (i, false);
				}
			}
		}
		
	}
	private void LowDMG ()
	{
		for (int i = 0; i < mainEnemy.Count; i++) {
			mainEnemy [i].dmgFromShoot = 0;
		}
	}

}
