using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnderwaterWorldScript : MonoBehaviour {
	
	public Transform waterTr; //pozycja wody
	public FogMode fogMode;
	public Color underWaterColorOfFogStart; // kolor dla mgly niedaleko powierzchni
	//public Color underWaterColorOfFogEnd; // kolor dla mgly w glebinach
	public float density;
	public float offsetOfPlayer; // Przesuniecie wlaczenia lub wylaczenia dzialania skryptu
	public float minDistanceFog; // Odleglosc od ktorej rozpoczynamy malowanie mgly
	public float maxDistanceFog; // odleglosc gdzie mgla ma wartosc maksymalna
	public float maxDeepDistance = 100;

	private Transform cameraTransform;
	private Color standardColor;
	private float standardMinDistanceFog;
	private float standardMaxDistanceFog;
	private FogMode odlFogMode;
	private float odlDensity;

	private bool underWater = false;
	private bool setValue = false;
	//wartości dla ogolnych kolorow mgly
	//wartosci kolorow dla plytiego zanurzenia
	//private float uR = 0;
	//private float uG = 0;
	//private float uB = 0;
	//wartosci kolorow dla glebokiego zanurzenia
	//private float dR = 0;
	//private float dG = 0;
	//private float dB = 0;
	//wartosci kolorow dla wynikowego zanurzenia
	//private float wR = 0;
	//private float wG = 0;
	//private float wB = 0;
	//private Color actualColor;
	public GameObject hudrantPref;
	private bool isHydrantOnScene = false;
	private Transform camZderzakTr;

	private List <Camera> camList = new List<Camera>();
	private List <Transform> camListTr = new List<Transform>();
	private Camera activeCam;
	private float valueOfDistance = 0; // dystans ktory okresla maksymalna odleglosc miedzy tafla wody a calkowitym przejsciem w glebiny
	private float heightOfWater = 0; // wysokosc tafli wody
	UseCameraScript ucs;
	RCCCarControllerV2 rcc;
	PlayerHealth ph;
	void Awake ()
	{
		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;
		ph = (PlayerHealth)FindObjectOfType (typeof(PlayerHealth)) as PlayerHealth;
		hudrantPref.SetActive (false);
	}
	void Start ()
	{
		ucs = GameObject.Find("BrumBrume").GetComponentInChildren<UseCameraScript>();
		cameraTransform = ucs.camers[0].GetComponent<Transform>();
		camZderzakTr = ucs.camers [3].GetComponent<Transform> ();
		//rcc = GameObject.Find ("BrumBrume").GetComponent<Rigidbody> ();
		for(int i = 0; i < ucs.camers.Length; i++)
		{
			camList.Add(ucs.camers[i]);
			camListTr.Add(ucs.camers[i].GetComponent<Transform>());
		}
		//AssignDefaultColorsColors(underWaterColorOfFogStart, underWaterColorOfFogEnd);
		LoadDefaultSettings(false);
		heightOfWater = waterTr.position.y;
		valueOfDistance = heightOfWater - maxDeepDistance;
	}
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.N))
			AssignActiveCamera ();

		if (camZderzakTr.position.y < heightOfWater && isHydrantOnScene == false) {
			hudrantPref.SetActive (true);
			Instantiate(hudrantPref, camZderzakTr.position, Quaternion.Euler(0,1,0));
			isHydrantOnScene = true;
			Time.timeScale = 0.8f;
		}
		if(cameraTransform.position.y - offsetOfPlayer < heightOfWater && underWater == false)
		{
			underWater = true;
		}
		else if(cameraTransform.position.y - offsetOfPlayer > heightOfWater && underWater == true){
			LoadDefaultSettings(true);
			underWater = false;
			setValue = false;
		}
		if(underWater == true)
		{
			if(setValue == false)
			{
				SetValueOfFog();
				setValue = true;

			}
			ph.GameOver ();
			//rcc.engineRunning = false;
			//rcc.canControl = false;

			rcc.maxspeed = 2;
			rcc.gameObject.GetComponent<Rigidbody> ().drag = 100;
			//rcc.gameObject.GetComponent<Rigidbody> ().mass = 0.01f;
			RenderSettings.fogColor = underWaterColorOfFogStart;
			//Przydałoby się jakieś gameover
		}
	}
	private void AssignActiveCamera ()
	{
		for(int i = 0; i < camList.Count; i++)
		{
			if(camList[i].enabled == true && activeCam != camList[i]){
				cameraTransform = camListTr[i];
				activeCam = camList[i];
			}
		}
	}
	private void SetValueOfFog()
	{
		RenderSettings.fogStartDistance = minDistanceFog;
		RenderSettings.fogEndDistance = maxDistanceFog;
		RenderSettings.fogDensity = density;
		RenderSettings.fogMode = fogMode;

	}
	private void LoadDefaultSettings(bool loaded)
	{
		if(loaded == false){
			standardColor = RenderSettings.fogColor;
			standardMinDistanceFog = RenderSettings.fogStartDistance;
			standardMaxDistanceFog = RenderSettings.fogEndDistance;
			odlFogMode = RenderSettings.fogMode;
			odlDensity = RenderSettings.fogDensity;
		}
		else
		{
			RenderSettings.fogColor = standardColor;
			RenderSettings.fogStartDistance = standardMinDistanceFog;
			RenderSettings.fogEndDistance = standardMaxDistanceFog;
			RenderSettings.fogMode = odlFogMode;
			RenderSettings.fogDensity = odlDensity;
		}
	}
	/*private float CountDeep ()
	//{
	//	return Mathf.Clamp((cameraTransform.position.y - offsetOfPlayer), waterTr.position.y, valueOfDistance);  
	}*/
	/*private Color CountColor (float valueOfDeep)
	{
		wR = Mathf.Lerp(uR, dR, valueOfDeep);
		wG = Mathf.Lerp(uG, dG, valueOfDeep);
		wB = Mathf.Lerp(uB, dB, valueOfDeep);
		return actualColor = new Color(wR, wG, wB);
	}
	private void AssignDefaultColorsColors (Color unDeep, Color deep)
	{
		uR = unDeep.r;
		uG = unDeep.g;
		uB = unDeep.b;

		dR = deep.r;
		dG = deep.g;
		dB = deep.b;
	}*/

}
