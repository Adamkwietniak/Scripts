using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DustFromDynamicObiect : MonoBehaviour {

	public List<Dusted> colliders = new List<Dusted>();
	[HideInInspector]public List<Dusted> collList = new List<Dusted> ();
	void Start ()
	{
		for(int i = 0; i < colliders.Count; i++)
		{
			collList.Add (new Dusted(colliders[i].colliderTree, colliders[i].isStone, colliders[i].colliderTree.GetComponentInParent<Rigidbody>(),
			                         colliders[i].colliderTree.GetComponent<Transform>(), 
			                         CreatePrefab (i),
			                         null, null, null, colliders[i].colliderTree.name, false, 0, colliders[i].colliderTree.GetComponent<Collider>()));
		}
		for(int i = 0; i < collList.Count; i++)
		{
			if(collList[i].prefTrans == null)
				collList[i].prefTrans = collList[i].prefabPartSys.GetComponent<Transform>();
			if(collList[i].ps == null)
				collList[i].ps = collList[i].prefabPartSys.GetComponent<ParticleSystem>();
			if(collList[i].psTr == null)
				collList[i].psTr = collList[i].ps.GetComponent<Transform>();
			if(collList[i].ps != null)
			{
				collList[i].ps.Pause();
			}
		}
	}
	void Update ()
	{
		for(int i = 0; i < collList.Count; i++)
		{
			if(collList[i].isDusting == true)
			{
				AttendanceDust(i);
			}
			else if(collList[i].isDusting == true && collList[i].ps.isPlaying == true){
				collList[i].ps.Pause();
			}
		}
	}
	private void AttendanceDust(int i)
	{
		if(collList[i].isStone == true)
		{
			if(collList[i].rb.IsSleeping() == false && collList[i].transCol.hasChanged == true && collList[i].ps.isPlaying == false)
			{
				collList[i].prefTrans.position = collList[i].transCol.position;
				collList[i].ps.Play();
			}
			else if(collList[i].rb.IsSleeping() == false && collList[i].transCol.hasChanged == true && collList[i].ps.isPlaying == true)
			{
				collList[i].prefTrans.position = collList[i].transCol.position;
			}
			if (collList[i].rb.IsSleeping() == true && collList[i].ps.isPlaying == true)
			{
				if(CountTime(i)>=5)
				{
					collList[i].ps.Stop();
					collList[i].colider.enabled = false;
					collList[i].isDusting = false;
				}
			}
		}
		else
		{
			if(CountTime(i)<5)
			{
				collList[i].prefTrans.position = collList[i].transCol.position;
				collList[i].ps.Play();
			}
			else
			{
				collList[i].ps.Stop();
				collList[i].colider.enabled = false;
				collList[i].isDusting = false;
			}
		}
	}
	private GameObject CreatePrefab (int i)
	{
		return (GameObject)Instantiate(GameObject.FindWithTag("Particle"), colliders[i].colliderTree.GetComponent<Transform>().position, colliders[i].colliderTree.GetComponent<Transform>().rotation);
	}
	private float CountTime (int i)
	{
		//Debug.Log("licze");
		return collList[i].countToEnd += Time.deltaTime;
	}
}
[Serializable]
public class Dusted
{
	public GameObject colliderTree;
	public bool isStone;
	[HideInInspector]public Rigidbody rb;
	[HideInInspector]public Transform transCol;
	[HideInInspector]public GameObject prefabPartSys;
	[HideInInspector]public Transform prefTrans;
	[HideInInspector]public ParticleSystem ps;
	[HideInInspector]public Transform psTr;
	[HideInInspector]public string idx;
	[HideInInspector]public bool isDusting;
	[HideInInspector]public float countToEnd;
	[HideInInspector]public Collider colider;
	public Dusted (GameObject coll, bool iStone, Rigidbody rigbd, Transform colTR, GameObject pref, Transform prefTr, ParticleSystem part, Transform partSysTr, string index, bool dusting,
	               float ctEnd, Collider coli)
	{
		this.colliderTree = coll;
		this.isStone = iStone;
		this.rb = rigbd;
		this.transCol = colTR;
		this.prefabPartSys = pref;
		this.prefTrans = prefTr;
		this.ps = part;
		this.psTr = partSysTr;
		this.idx = index;
		this.isDusting = dusting;
		this.countToEnd = ctEnd;
		this.colider = coli;
	}
}
















