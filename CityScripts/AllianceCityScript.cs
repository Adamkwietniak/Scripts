using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AllianceCityScript : MonoBehaviour {

	public List<AllianceClassCity> allyNpc = new List<AllianceClassCity>();
	private List<AllianceClassCity> npc = new List<AllianceClassCity>();
	private GameObject brumbrum;
	private Transform brumTrans;
	public int distanceToPlayer = 10;
	MissionCityScript mfs;
	float distFrom = 1000;
	[HideInInspector]public string colliName = "none";
	private bool czyPatrole = false;
	private bool czyMisje = false;
	[HideInInspector]public bool czyKolizja = false;
    [HideInInspector]public List<GameObject> firstGroup = new List<GameObject>();    //lista cywilow ktore znajduja sie w 1 grupie
    [HideInInspector]public List<GameObject> secondGroup = new List<GameObject>();   //lista cywilow ktore znajduja sie w 2 grupie
    [HideInInspector]public List<GameObject> thirdGroup = new List<GameObject>();    //lista cywilow ktore znajduja sie w 3 grupie
    [HideInInspector]public bool allTake = false;

    private float timer1 = 0;
    private float maxTimer1 = 5;
    public bool playerInBase = false;
    public int maxDistanceToBase = 50;

    private AllianceClassCity[] objInBrum = new AllianceClassCity[5]; 

    public Text textCounting;           //liczy ile załadowano osob do bruma
    private int maxNumberInBrum = 5;
    private int actualInsideBrum = 0;
    public Text countFirstGroup;                //niezabrana liczba z grupy 1
    private int iloscOsobW1Grupie = 0;
    public Text countSecondGroup;              //niezabrana liczba z grupy 2
    private int iloscOsobW2Grupie = 0;
    public Text countThirdGroup;              //niezabrana liczba z grupy 3
    private int iloscOsobW3Grupie = 0;

	public string keepTextAllianceCityScript;
	[HideInInspector]public string cont1;
	[HideInInspector]public string cont2;
	[HideInInspector]public string cont3;

    public Transform targetInBase;
    RCCCarControllerV2 rcc;
    void Start ()
	{
		brumbrum = GameObject.Find ("BrumBrume");
		mfs = brumbrum.GetComponent<MissionCityScript> ();
        rcc = brumbrum.GetComponent<RCCCarControllerV2>();
		brumTrans = brumbrum.GetComponent<Transform> ();
		for (int i = 0; i < allyNpc.Count; i++) {
			//for(int j = 0; j < allyNpc[i].SunnyPatrol.Length; j++){
			npc.Add(new AllianceClassCity(allyNpc[i].allianceNpc.GetComponent<Animator>(), allyNpc[i].allianceNpc.GetComponent<NavMeshAgent>(),
			                                allyNpc[i].allianceNpc.GetComponentsInChildren<Rigidbody>(), allyNpc[i].allianceNpc, allyNpc[i].missionNPC, 
			                                allyNpc[i].allianceNpc.GetComponent<Transform>(), allyNpc[i].defaultPos, 
			                                true, allyNpc[i].allianceNpc.GetComponentsInChildren<SkinnedMeshRenderer>(),
			                                allyNpc[i].particleSys, 2000, false, allyNpc[i].isPatrol,
			                                allyNpc[i].followedTarget, allyNpc[i].numberOfGroup, false, false, false));
			//}
		}
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].isPatrol == true)
				czyPatrole = true;
			if(npc[i].missionNPC == true)
				czyMisje = true;
            if(npc[i].numberOfGroup == 1)
            {
                firstGroup.Add(npc[i].allianceNpc);
                iloscOsobW1Grupie++;
            }
            else if (npc[i].numberOfGroup == 2)
            {
                secondGroup.Add(npc[i].allianceNpc);
                iloscOsobW2Grupie++;
            }
            else
            {
                thirdGroup.Add(npc[i].allianceNpc);
                iloscOsobW3Grupie++;
            }
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

        //blok sprawdzajacy czy player jest w bazie
        if (allTake == false) {
            if (timer1 < maxTimer1)
            {
                timer1 += Time.deltaTime;
            }
            else
            {
                if (Distance(brumTrans.position, targetInBase.position) < maxDistanceToBase && playerInBase == false)
                {
                    playerInBase = true;
                }
                else
                    playerInBase = false;

                timer1 = 0;
            }
            if (playerInBase == true && rcc.speed <= 1 && objInBrum[0] != null)
            {
                RemoveObjInBrum();
            }
			textCounting.text = (keepTextAllianceCityScript+" "+actualInsideBrum.ToString() + "/" + maxNumberInBrum.ToString());
			countFirstGroup.text = (cont1+" "+iloscOsobW1Grupie.ToString());
			countSecondGroup.text = (cont2+" "+ iloscOsobW2Grupie.ToString());
			countThirdGroup.text = (cont3+" "+ iloscOsobW3Grupie.ToString());
        }
	}
	private void PatrolTerrain ()		//Obsluga npc patrolowych (musi posiadac agenta)
	{
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].isLife == npc[i].done != false && npc[i].missionNPC != true && npc[i].isPatrol == true)
			{
				for(int j = 0; j < npc[i].followedTarget.Length; j++)
				{
					if(Distance(npc[i].selfTransform.position, npc[i].followedTarget[j].position) < 0.5f){
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
	private void AttendanceMission ()		//Przetwarzanie misji w city
	{
		for (int i = 0; i < npc.Count; i++) {
			if(npc[i].missionNPC == true)
			{	//Blok odpowiedzialny za obliczanie odleglosci miedzy obiektami (player-cywil/oboz-cywil)
                //Poczatek
                float distFromBase = 0;
                npc[i].discanceFromPlayer = Distance (npc[i].selfTransform.position, brumTrans.position);
                if(npc[i].inBase == true)
                    distFromBase = Distance(npc[i].selfTransform.position, targetInBase.position);
                if (actualInsideBrum < maxNumberInBrum) {
                    if (npc[i].discanceFromPlayer > distanceToPlayer && npc[i].discanceFromPlayer < distanceToPlayer + 50 && npc[i].inBase == false)
                    {
                        npc[i].agent.SetDestination(brumTrans.position);
                        if (npc[i].anim.GetBool("isWalk") == false)
                            npc[i].anim.SetBool("isWalk", true);
                        if (npc[i].isWalk == false)
                        {
                            npc[i].agent.Resume();
                            npc[i].isWalk = true;
                        }
                    }
                    else if (npc[i].discanceFromPlayer < distanceToPlayer && npc[i].inBase == false)
                    {
                        npc[i].agent.SetDestination(brumTrans.position);
                        if (npc[i].anim.GetBool("isWalk") == true)
                            npc[i].anim.SetBool("isWalk", false);
                        if (npc[i].isWalk == true)
                        {
                            npc[i].agent.Stop();
                            npc[i].isWalk = false;
                        }
                    }
                    else if (distFromBase > 3 && npc[i].discanceFromPlayer > distanceToPlayer && npc[i].inBase == true) // kiedy cywile docieraja na miejsce zbiorki w bazie
                    {
                        if (npc[i].isWalk == false)
                        {
                            npc[i].agent.Resume();
                            npc[i].isWalk = true;
                        }
                        if (npc[i].anim.GetBool("isWalk") == false)
                            npc[i].anim.SetBool("isWalk", true);
                        npc[i].agent.SetDestination(targetInBase.position);

                    }
                    else if (distFromBase > 3 && npc[i].discanceFromPlayer <= distanceToPlayer && npc[i].inBase == true) // w bazie kiedy player zagraza ich zyciu to staja
                    {
                        if (npc[i].anim.GetBool("isWalk") == true)
                            npc[i].anim.SetBool("isWalk", false);
                        if (npc[i].isWalk == true)
                        {
                            npc[i].agent.Stop();
                            npc[i].isWalk = false;
                        }

                    }
                    else if (distFromBase <= 3 && npc[i].inBase == true)		// kiedy cywile są na miejscu zbiorki
                    {
                        if (npc[i].anim.GetBool("isWalk") == true)
                            npc[i].anim.SetBool("isWalk", false);
                        if (npc[i].isWalk == true)
                        {
							npc[i].agent.Stop();
                            npc[i].isWalk = false;
                        }
                    }
                }
                //Koniec bloku

                //Poczatek bloku odpowiedzialnego za wlaczanie i wylaczanie cywili
                if(actualInsideBrum < maxNumberInBrum)
                {
                    if (npc[i].discanceFromPlayer < distanceToPlayer && npc[i].isHide == false && npc[i].inBase == false)
                    {
                        for (int j = 0; j < npc[i].SkinnedMeshR.Length; j++)
                        {
                            npc[i].SkinnedMeshR[j].enabled = false;
                        }

                        if (npc[i].numberOfGroup == 1)
                        {
                            ClearGroup(firstGroup, npc[i].allianceNpc.name);
                            iloscOsobW1Grupie--;
                        }
                        else if (npc[i].numberOfGroup == 2)
                        {
                            ClearGroup(secondGroup, npc[i].allianceNpc.name);
                            iloscOsobW2Grupie--;
                        }
                        else
                        {
                            ClearGroup(thirdGroup, npc[i].allianceNpc.name);
                            iloscOsobW3Grupie--;
                        }
                        for (int o = 0; o < objInBrum.Length; o++)
                        {
                            if(objInBrum[o] == null)
                            {
                                objInBrum[o] = npc[i];
                                break;
                            }
                        }
                        EnableOrDisablePartSys(i, false);
                        npc[i].selfTransform.position = new Vector3(0, -10, 0);
                        npc[i].anim.SetBool("isWalk", false);
                        npc[i].agent.Stop();
                        npc[i].isHide = true;
                        actualInsideBrum++;
                    }
				}
                //Koniec bloku
			}
		}
		CheckAttendanceMission ();
	}
    private void CheckAttendanceMission()       //Obsluga misji
    {
        if (iloscOsobW1Grupie == 0 && iloscOsobW2Grupie == 0 && iloscOsobW3Grupie == 0 && allTake == false && actualInsideBrum == 0) {
        allTake = true;
        textCounting.enabled = false;
        countFirstGroup.enabled = false;
        countSecondGroup.enabled = false;
        countThirdGroup.enabled = false;
        }
	}
    private void RemoveObjInBrum ()
    {
        for(int i = 0; i < npc.Count; i++)
        {
            for(int j = 0; j < objInBrum.Length; j++)
            {
				if(npc[i] == objInBrum[j])
                {
                    float rand1 = Random.Range(7, 12);
                    float rand2 = Random.Range(7, 12);
                    npc[i].inBase = true;
                    npc[i].agent.enabled = false;
                    npc[i].selfTransform.position = new Vector3(brumTrans.position.x+rand1, brumTrans.position.y+1f, brumTrans.position.z+rand2);
                    npc[i].agent.enabled = true;
                    //Debug.Log("Wlaczylem obiekt "+ npc[i].allianceNpc.name+" w pozycji: "+ npc[i].selfTransform.position);
                    npc[i].done = true;
                    for(int k = 0; k < npc[i].SkinnedMeshR.Length; k++)
                    {
                        npc[i].SkinnedMeshR[k].enabled = true;
                    }
                    npc[i].isHide = false;
                }
            }
        }
        for(int i = 0; i < objInBrum.Length; i++)
        {
            objInBrum[i] = null;
        }
        actualInsideBrum = 0;
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
	private float Distance (Vector3 dist1, Vector3 dist2) //obliczanie dystansu miedzy 2 obiektami (int i - numerek indeksowy zolnierza
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
    private void ClearGroup(List<GameObject> lista, string nameObj)
    {
        for (int z = 0; z < lista.Count; z++)
        {
            if (lista[z].name == nameObj)
            {
                lista.RemoveAt(z);
            }
        }
    }
}
