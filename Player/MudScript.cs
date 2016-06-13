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
    private int lengthOfPs;
    private int lenghOfPart;
    [Tooltip("Dzielnik dla startSpeed")]
    public int dzielnikAvS = 50;
    [Tooltip("Dzielnik dla emissionRate")]
    public int dzielnikAVE = 10;
    [Tooltip("Mnoznik dla maxParticles")]
    public int mnoznikMP = 20;
    [Tooltip("Wartosci minimalne dla: x-startSpeed, y-emissionRate, z-maxParticles")]
    public Vector3 minValue = new Vector3(0, 0, 0);

    private DefParams [] WriteDefparam (ParticleSystem[] tempPS)
    {
        DefParams[] tempDef = new DefParams[6];
        for(int i = 0; i < tempPS.Length; i++)
        {
            tempDef[i] = new DefParams(tempPS[i].enableEmission, tempPS[i].emissionRate, tempPS[i].maxParticles, tempPS[i].startSpeed,
                tempPS[i].gravityModifier, new Vector3(tempPS[i].startSpeed * dzielnikAvS, tempPS[i].emissionRate * dzielnikAVE, tempPS[i].maxParticles * mnoznikMP));
            /*
            tempDef[i].isEmision = tempPS[i].emission.enabled;
            tempDef[i].emisRate = tempPS[i].emissionRate;
            tempDef[i].maxPart = tempPS[i].maxParticles;
            tempDef[i].startSpeed = tempPS[i].startSpeed;
            tempDef[i].grafModif = tempPS[i].gravityModifier;
            tempDef[i].maxValueAV.x = tempPS[i].startSpeed * dzielnikAvS;
            tempDef[i].maxValueAV.y = tempPS[i].emisRate * dzielnikAVE;
            tempDef[i].maxValueAV.z = tempPS[i].maxPart / mnoznikMP;
            */

        }
        return tempDef;
    }
    void Awake()
    {
        for (int i = 0; i < attTire.Count; i++)
        {
            ParticleSystem[] temporPartSys = new ParticleSystem[6];
            temporPartSys = LoadPS(attTire[i].particleGO);
            psTire.Add(new TireParticle(attTire[i].tire, attTire[i].particleGO, temporPartSys, false, false,
                                        attTire[i].tire.GetComponent<Transform>(), 0, attTire[i].tire.GetComponent<Rigidbody>(),
                                        attTire[i].currentWheelCollider, LoadRenderer(temporPartSys), false, WriteDefparam(temporPartSys)
                                        ));
        }
        lengthOfPs = attTire.Count;
        //Debug.Log("Ilosc wynosi: " + psTire[0].particle.Length);
        
        lenghOfPart = psTire[0].particle.Length;
    }
	// Use this for initialization
    private void AssignNewValue (int i, int j, bool isEmit, float emissionRate, int maxParticles, float startSpeed)
    {
        psTire[i].particle[j].enableEmission = isEmit;
        psTire[i].particle[j].emissionRate = emissionRate;
        psTire[i].particle[j].maxParticles = maxParticles;
        psTire[i].particle[j].startSpeed = startSpeed;
    }

	void Start () {
		rcc = GetComponent<RCCCarControllerV2> ();
		if (rcc._wheelTypeChoise == RCCCarControllerV2.WheelType.AWD)
			czyAWD = true;
       /* for(int i = 0; i < psTire.Count; i++)
        {
            Debug.Log("Wartosci dla: " + psTire[i].particleGO.name);
            for(int j = 0; j < psTire[i].defParam.Length; j++)
            {
                Debug.Log("EmissionRate: " + psTire[i].defParam[j].emisRate);
                Debug.Log("Maximum particles: " + psTire[i].defParam[j].maxPart);
                Debug.Log("maxValueAV: " + psTire[i].defParam[j].maxValueAV);
                Debug.Log("StartSpeed: " + psTire[i].defParam[j].startSpeed);
                Debug.Log("CzyEmisjeDaje: " + psTire[i].defParam[j].isEmision);
            }
        }*/
        for (int i = 0; i < lengthOfPs; i++)
        {
            for(int j = 0; j < lenghOfPart; j++)
            {
                AssignNewValue(i, j, false, 0, 0, 0);
                if (psTire[i].particle[j].loop == false)
                    psTire[i].particle[j].loop = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		Attendanceparts ();
	}
	private void Attendanceparts ()
	{
		for (int i = 0; i < lengthOfPs; i++) {
			DisOrEnableMud (i);
		}
	}
	private void DisOrEnableMud (int i)
	{
		if (psTire [i].generateMud == true && rcc.speed > 1) {
			CheckReverse ();
			CountAngularV (i);
			TrunOn (i);
            float actValueAV = psTire[i].angularV;
            //Vector3 tempIt;  //x.startSpeed, y.emissionRate, z.maxParticles
            //tempIt.x = (Mathf.Abs(actValueAV / 50));//start speed
			//psTire [i].particle.enableEmission = true;
            //tempIt.y = UnityEngine.Random.Range (0, Mathf.Abs (actValueAV / 10));//emission rate
            //tempIt.z = UnityEngine.Random.Range (0, Mathf.Abs (actValueAV * 20));//max particles
            for(int j = 0; j < lenghOfPart; j++)
            {
                if (psTire[i].defParam[j].isEmision == true)
                {
                    Vector3 tempSeparate;
                    tempSeparate.x = actValueAV / psTire[i].defParam[j].maxValueAV.x;
                    tempSeparate.y = actValueAV / psTire[i].defParam[j].maxValueAV.y;
                    tempSeparate.z = actValueAV / psTire[i].defParam[j].maxValueAV.z;
                    Vector3 coutAcrual;
                    float startSp = actValueAV / dzielnikAvS;
                    float emmiR = Mathf.Abs(actValueAV / dzielnikAVE);
                    int maxim = (int)Mathf.Abs(actValueAV * mnoznikMP);

                    if (maxim > psTire[i].defParam[j].maxPart)
                        maxim = psTire[i].defParam[j].maxPart;
                    if (emmiR > psTire[i].defParam[j].emisRate)
                        emmiR = psTire[i].defParam[j].emisRate;
                    if (startSp > psTire[i].defParam[j].startSpeed)
                        startSp = psTire[i].defParam[j].startSpeed;


                    coutAcrual.x = Mathf.Lerp(minValue.x * CountValue(rcc.speed, psTire[i].defParam[j].startSpeed), startSp, tempSeparate.x);
                    coutAcrual.y = Mathf.Lerp(minValue.y * CountValue(rcc.speed, psTire[i].defParam[j].emisRate), UnityEngine.Random.Range((psTire[i].defParam[j].emisRate/2 < emmiR) ? psTire[i].defParam[j].emisRate / 2 : emmiR, (psTire[i].defParam[j].emisRate < emmiR) ? psTire[i].defParam[j].emisRate : emmiR), tempSeparate.y);
                    coutAcrual.z = Mathf.Lerp(minValue.z * CountValue(rcc.speed, psTire[i].defParam[j].maxPart), UnityEngine.Random.Range((psTire[i].defParam[j].maxPart/2 < maxim) ? psTire[i].defParam[j].maxPart / 2 : maxim, (psTire[i].defParam[j].maxPart < maxim) ? psTire[i].defParam[j].emisRate : maxim), tempSeparate.z);
                    AssignNewValue(i, j, psTire[i].defParam[j].isEmision, coutAcrual.y, (int)(coutAcrual.z + rcc.speed), coutAcrual.x);
                }
            }
		} else if((psTire [i].generateMud == false && psTire[i].particle[lenghOfPart-1].maxParticles != 0) || rcc.speed <=1)
        {
            for(int j = 0; j < lenghOfPart; j++)
            {
                AssignNewValue(i, j, false, 0, 0, 0);
            }
			TurnOff(i);
		}
	}
    private float CountValue (float speed, float value)
    {
        float tempV;
        if(speed < 25)
        {
            tempV = value * 0.1f;
        }
        else if(speed >= 25 && speed < 75)
        {
            tempV = value * 0.15f;
        }
        else
        {
            tempV = value * 0.2f;
        }
        return tempV;
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
			for(int i = 0; i < lengthOfPs; i++)
			{	if(odwr == false){
					psTire[i].reverse = true;
					psTire[i].particleGO.transform.Rotate(0f, 180f, 0f);
					//Debug.Log("nastepuje obrot tyl");
				}
			}
			if(odwr == false){
				odwr = true;
			}
		}
		if (go == false) {
			for(int i = 0; i < lengthOfPs; i++)
			{
				if(odwr == true){
					psTire[i].reverse = false;
                    psTire[i].particleGO.transform.Rotate(0f, 180f, 0f);
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
		psTire [i].particleGO.SetActive (true);
	}
	private void TurnOff(int i)
	{
		psTire [i].particleGO.SetActive (false);
	}
    private ParticleSystem[] LoadPS(GameObject go)
    {
        ParticleSystem[] temp = new ParticleSystem[6];
        temp[0] = go.GetComponent<ParticleSystem>();
        temp[1] = go.transform.GetChild(0).GetComponent<ParticleSystem>();
        temp[2] = go.transform.GetChild(1).GetComponent<ParticleSystem>();
        temp[3] = go.transform.GetChild(2).GetComponent<ParticleSystem>();
        temp[4] = go.transform.GetChild(3).GetComponent<ParticleSystem>();
        temp[5] = go.transform.GetChild(4).GetComponent<ParticleSystem>();
        return temp;
    }
    private Renderer[] LoadRenderer(ParticleSystem[] partS)
    {
        Renderer[] tempR = new Renderer[6];
        for (int i = 0; i < partS.Length; i++)
        {
            tempR[i] = partS[i].GetComponent<Renderer>();
        }
        return tempR;
    }
}
[Serializable]
public class TireParticle {
	public GameObject tire;
    public GameObject particleGO;
	[HideInInspector]public ParticleSystem []particle = new ParticleSystem[6];
	[HideInInspector]public bool generateMud;
	[HideInInspector]public bool reverse;
	[HideInInspector]public Transform trTire;
	[HideInInspector]public float angularV;
	[HideInInspector]public Rigidbody rigBody;
	public WheelCollider currentWheelCollider;
	[HideInInspector]public Renderer []rend = new Renderer[6];
	[HideInInspector]public bool onTheGround;
    [HideInInspector]public DefParams []defParam = new DefParams[6];


    public TireParticle (GameObject tireObj, GameObject partGO, ParticleSystem []ps, bool geneMud, bool rever, Transform trTiree, float aV, Rigidbody rb, WheelCollider curWhColl, Renderer []render, bool otGround, DefParams []defPam){
		this.tire = tireObj;
        this.particleGO = partGO;
		this.particle = ps;
		this.generateMud = geneMud;
		this.reverse = rever;
		this.trTire = trTiree;
		this.angularV = aV;
		this.rigBody = rb;
		this.currentWheelCollider = curWhColl;
		this.rend = render;
		this.onTheGround = otGround;
        this.defParam = defPam;
	}
}
public class DefParams
{
    [HideInInspector]
    public bool isEmision;
    [HideInInspector]
    public float emisRate;
    [HideInInspector]
    public int maxPart;
    [HideInInspector]
    public float startSpeed;
    [HideInInspector]
    public float grafModif;
    [HideInInspector]
    public Vector3 maxValueAV; //okresla maxymalne wartosci dla startSpeed, emisionRate, maxParticles, według podanych wzorów

    public DefParams (bool isEnim, float eMiRate, int maxPartic, float startingSpeed, float gModif, Vector3 maxValues)
    {
        this.isEmision = isEnim;
        this.emisRate = eMiRate;
        this.maxPart = maxPartic;
        this.startSpeed = startingSpeed;
        this.grafModif = gModif;
        this.maxValueAV = maxValues;
    }
}