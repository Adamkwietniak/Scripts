using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllianceSoliderForestEvent : MonoBehaviour {
	public List<AllianceClassForest> allyNpc = new List<AllianceClassForest>();
	private List<AllianceClassForest> npc = new List<AllianceClassForest>();
	private GameObject brumbrum;
	private Transform brumTrans;
	public int distanceToPlayer = 10;
	MissionForestScript mfs;
	float distFrom = 1000;
	[HideInInspector]public string colliName = "none";
	private bool czyPatrole = false;
	private bool czyMisje = false;
	[HideInInspector]public bool czyKolizja = false;
	void Start ()
	{
		brumbrum = GameObject.Find ("BrumBrume");
		mfs = brumbrum.GetComponent<MissionForestScript> ();
		brumTrans = brumbrum.GetComponent<Transform> ();
		for (int i = 0; i < allyNpc.Count; i++) {
			//for(int j = 0; j < allyNpc[i].SunnyPatrol.Length; j++){
				npc.Add(new AllianceClassForest(allyNpc[i].allianceNpc.GetComponent<Animator>(), allyNpc[i].allianceNpc.GetComponent<NavMeshAgent>(),
			                                	allyNpc[i].allianceNpc.GetComponentsInChildren<Rigidbody>(), allyNpc[i].allianceNpc, allyNpc[i].missionNPC, 
			                                	allyNpc[i].allianceNpc.GetComponent<Transform>(), allyNpc[i].defaultPos, 
				                                true, allyNpc[i].allianceNpc.GetComponentsInChildren<SkinnedMeshRenderer>(),
			                                	allyNpc[i].particleSys, 2000, false, allyNpc[i].isPatrol,
				                                allyNpc[i].followedTarget));
			//}
		}
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].isPatrol == true)
				czyPatrole = true;
			if(npc[i].missionNPC == true)
				czyMisje = true;
		}
		for (int i = 0; i < npc.Count; i++) {
			EnableOrDisablePartSys (i, false);
		}
	}
	void Update ()
	{
		if (czyPatrole == true)
			PatrolTerrain ();
		if (czyMisje == true)
			AttendanceMission ();
		if (czyKolizja == true)
			CheckHit ();
	}
	private void PatrolTerrain ()		//Obsluga npc patrolowych (musi posiadac agenta)
	{
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].isLife == npc[i].done != false && npc[i].missionNPC != true && npc[i].isPatrol == true)
			{
				for(int j = 0; j < npc[i].followedTarget.Length; j++)
				{
					if(Distance(i, npc[i].selfTransform.position, npc[i].followedTarget[j].position) < 0.5f){
						if(j < npc[i].followedTarget.Length)
						{
							npc[i].agent.SetDestination(npc[i].followedTarget[j+1].position);
						}
						else if(j >= npc[i].followedTarget.Length)
						{
							j = 0;
							npc[i].agent.SetDestination(npc[i].followedTarget[j].position);
						}
					}
				}
			}
		}
	}
	private void CheckHit ()			//Sprawdzenie czy doszlo do kolizji zolnierza z Playerem
	{
		for (int i = 0; i < npc.Count; i++) {
			if(colliName == npc[i].allianceNpc.name)
			{
				EnableRagDoll (i);
			}
		}
	}
	private void AttendanceMission ()		//Przetwarzanie misji w villige
	{
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].missionNPC == true)
			{																										//Blok zmiany
				npc[i].discanceFromPlayer = Distance (i, npc[i].selfTransform.position, brumTrans.position);
				if(npc[i].discanceFromPlayer>distanceToPlayer && npc[i].discanceFromPlayer<distanceToPlayer+50)
				{
					if (npc [i].anim.GetBool ("isWalk") == false) {
						npc [i].agent.Resume ();
						npc [i].anim.SetBool ("isWalk", true);
					}
					npc [i].agent.SetDestination (brumTrans.position);

				}
				else if (npc [i].discanceFromPlayer > distanceToPlayer + 50 && npc [i].anim.GetBool ("isWalk") == true) {
					float distBetweenDefPos = Distance (i, npc [i].selfTransform.position, npc [i].defaultPos.position);
					if (distBetweenDefPos > 3) {
						npc [i].agent.SetDestination (npc [i].defaultPos.position);
					} else {
						if (npc [i].anim.GetBool("isWalk") == true) {
							npc [i].agent.Stop ();
							npc [i].anim.SetBool ("isWalk", false);
						}
					}
				}
				if(npc[i].discanceFromPlayer<distanceToPlayer)
				{
					for(int j = 0; j < npc[i].SkinnedMeshR.Length; j++)
					{
						npc[i].SkinnedMeshR[j].enabled = false;
					}
					EnableOrDisablePartSys (i, false);
					npc[i].selfTransform.position = new Vector3(0, -100, 0);
					npc[i].agent.Stop();
				}
			}
		}
		CheckAttendanceMission ();
	}
	private void CheckAttendanceMission ()		//Obsluga misji w villige
	{
		for (int i = 0; i < npc.Count; i++) {
			if (npc [i].missionNPC == true) {
				for(int j = 0; j < npc[i].SkinnedMeshR.Length; j++){
					if(npc[i].SkinnedMeshR[j].enabled == true)
					{
						break;
					}
					else
					{
						npc[i].done = true;
					}
				}
				if(CheckDone () == true)
				{
					if(mfs.czyDalej == false){
						mfs.czyDalej = true;
						czyMisje = false;
					}
				}
			}
		}
	}
	private bool CheckDone ()
	{											//Sprawdza czy misja zakonczona sukcesem (zabranie zolnierzy z villige)
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].missionNPC == true)
			{
				if(npc[i].done == false){
					return false;
					break;
				}
			}
		}
		return true;
	}
	private void EnableOrDisablePartSys (int i, bool param)
	{
		if(npc[i].particleSys != null){

			if (param == false) {							//wylacza particle
				npc [i].particleSys.SetActive(false);
			}
			if (param == true) {							//wlacza particle
				npc [i].particleSys.SetActive(true);
			}
		}
	}
	private float Distance (int i, Vector3 dist1, Vector3 dist2) //obliczanie dystansu miedzy 2 obiektami (int i - numerek indeksowy zolnierza
	{															//dist1 - pozycja 1 obiektu, dist2 - pozycja 2 obiektu)
		return distFrom = Vector3.Distance(dist1, dist2);
	}
	private void EnableRagDoll (int i)							//wlaczenie ragdolla
	{
		if (npc [i].isLife == true) {
			npc [i].isLife = false;
			npc[i].anim.enabled = false;
			EnableOrDisablePartSys(i, false);
			for (int j = 0; j < 13; j++) {
				npc [i].rBody [j].isKinematic = false;
			}
		}
	}
}
