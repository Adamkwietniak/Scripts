using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MudScript : MonoBehaviour {

	public List<TireParticle> attTire = new List<TireParticle> ();
	[HideInInspector]public List<TireParticle> psTire = new List<TireParticle> ();
	RCCCarControllerV2 rcc;
	private bool czyAWD = false;
	private bool odwr = false;
	void Awake ()
	{
		for (int i = 0; i < attTire.Count; i++) {
			psTire.Add(new TireParticle(attTire[i].tire, attTire[i].particle, false, false,
			                            attTire[i].tire.GetComponent<Transform>(), 0, attTire[i].tire.GetComponent<Rigidbody>(),
			                            attTire[i].currentWheelCollider, attTire[i].particle.GetComponent<Renderer>(), false
			                            ));
		}
	}
	// Use this for initialization
	void Start () {
		rcc = GetComponent<RCCCarControllerV2> ();
		if (rcc._wheelTypeChoise == RCCCarControllerV2.WheelType.AWD)
			czyAWD = true;
		foreach(TireParticle tp in psTire)
		{
			tp.particle.gravityModifier = 1;
			tp.particle.emissionRate = 0;
			tp.particle.maxParticles = 0;
			tp.particle.startSpeed = 0;
			tp.particle.emissionRate = 0;
			tp.particle.enableEmission = false;
			tp.particle.loop = true;
			//tp.particle.Stop();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Attendanceparts ();
	}
	private void Attendanceparts ()
	{
		for (int i = 0; i < psTire.Count; i++) {
			DisOrEnableMud (i);
		}
	}
	private void DisOrEnableMud (int i)
	{
		if (psTire [i].generateMud == true && rcc.speed > 1) {
			CheckReverse ();
			CountAngularV (i);
			TrunOn (i);
			psTire [i].particle.startSpeed = (Mathf.Abs(psTire [i].angularV/50));
			psTire [i].particle.enableEmission = true;
			psTire [i].particle.emissionRate = (UnityEngine.Random.Range (0, Mathf.Abs (psTire [i].angularV/10)));
			psTire [i].particle.maxParticles = (int)((UnityEngine.Random.Range (0, Mathf.Abs (psTire [i].angularV*20))));
		} else if(psTire [i].generateMud == false){
			CheckReverse ();
			psTire [i].particle.startSpeed =0;
			psTire [i].particle.enableEmission = false;
			psTire [i].particle.emissionRate =0;
			psTire [i].particle.maxParticles = 0;
			TurnOff(i);
		}
	}
	private void CountAngularV (int i)
	{
		if (rcc.minEngineRPM < rcc.engineRPM)
			psTire [i].angularV = (psTire [i].currentWheelCollider.rpm);
		else
			psTire [i].angularV = 0;
		//Debug.Log ("dzialam Angular wynosi: "+psTire [i].angularV);
	}
	private bool CheckReverse ()
	{
		if (rcc.reversing == true) {
			AssignReverse (true);
			return true;
		}
		else {
			AssignReverse (false);
		}
		return false;
	}
	private void AssignReverse (bool go)
	{
		if (go == true) {
			for(int i = 0; i < psTire.Count; i++)
			{	if(odwr == false){
					psTire[i].reverse = true;
					psTire[i].particle.gameObject.transform.Rotate(0f, 180f, 0f);
					//Debug.Log("nastepuje obrot tyl");
				}
			}
			if(odwr == false){
				odwr = true;
			}
		}
		if (go == false) {
			for(int i = 0; i < psTire.Count; i++)
			{
				if(odwr == true){
					psTire[i].reverse = false;
					psTire[i].particle.gameObject.transform.Rotate(0f, 180f, 0f);
					//Debug.Log("nastepuje obrot przod");
				}
			}
			if(odwr == true){
				odwr = false;
			}
		}
	}
	private void TrunOn (int i)
	{
		psTire [i].particle.gameObject.SetActive (true);
	}
	private void TurnOff(int i)
	{
		psTire [i].particle.gameObject.SetActive (false);
	}
}
[Serializable]
public class TireParticle {
	public GameObject tire;
	public ParticleSystem particle;
	[HideInInspector]public bool generateMud;
	[HideInInspector]public bool reverse;
	[HideInInspector]public Transform trTire;
	[HideInInspector]public float angularV;
	[HideInInspector]public Rigidbody rigBody;
	public WheelCollider currentWheelCollider;
	[HideInInspector]public Renderer rend;
	[HideInInspector]public bool onTheGround;

	public TireParticle (GameObject tireObj, ParticleSystem ps, bool geneMud, bool rever, Transform trTiree, float aV, Rigidbody rb, WheelCollider curWhColl, Renderer render, bool otGround){
		this.tire = tireObj;
		this.particle = ps;
		this.generateMud = geneMud;
		this.reverse = rever;
		this.trTire = trTiree;
		this.angularV = aV;
		this.rigBody = rb;
		this.currentWheelCollider = curWhColl;
		this.rend = render;
		this.onTheGround = otGround;
	}
}