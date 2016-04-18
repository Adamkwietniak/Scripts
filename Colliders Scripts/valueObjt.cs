using UnityEngine;
using System.Collections;

public class valueObjt {
	
	
	public GameObject gmob;
	public Transform trans;
	public Rigidbody rb;
	public float angularV;
	public float speedD;
	public float Distance;
	public bool iSleep;
	public bool useCount;
	
	public valueObjt(GameObject go, Transform tr, Rigidbody rigb, float av, float sd, int dis, bool iS, bool uS)
	{
		this.gmob = go;
		this.trans = tr;
		this.rb = rigb;
		this.angularV = av;
		this.speedD = sd;
		this.Distance = dis;
		this.iSleep = iS;
		this.useCount = uS;
	}
	
}