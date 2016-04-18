using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Skrypt odpowiedzialny za ustawienie aktywnej kamery i przelaczanie miedzy nimi.
public class UseCameraScript : MonoBehaviour {

	public Camera [] camers = new Camera[1];
	private List<Transform> camersTr = new List<Transform> ();
	RCCCarCamera rcCam;
	private int c = 0; // zmienna pomocnicza
	private string defaultCamera; //string domyslnej kamery
	private Camera activeCamera;
	private Transform activeCameraTr;
	private Transform brumTr;
	private float changeDistFromBrum = 12;
	[HideInInspector]public float tempDist = 12;
	public float minDist = 5.0f;
	public bool isTimer = false;
	private float dzielnik = 1;
	private bool isCameraToDistance = false;
	private bool isDefaultCamera = false;
	private float temDistanceNonDef = 10.0f;
	private int indexOfActCam = 0;
	private Quaternion [] defRot = new Quaternion[2];
	private Vector3 [] defPos = new Vector3[2];
	// Wstępne ustawienie kamer. Domyslna 
	/*private void LoadDefaultCOllider(Camera camo)
	{
		ch.camera = camo;
		ch.collisionLayer = camo.cullingMask;
		if (camo == camers[0]){
			ch.adjusmentDistance = rcCam.distance;
			ch.adjustedCameraClipPoints = 
			}
	}*/
	void Start () {
		brumTr = GameObject.Find ("BrumBrume").GetComponent<Transform> ();
		rcCam = (RCCCarCamera)FindObjectOfType (typeof(RCCCarCamera));
		defaultCamera = camers [0].name;// przypisanie nazwy domyślnej kamery
		activeCamera = camers [0];
		for (int i = 0; i< camers.Length; i++) {	//wstepne ustawienie kamer (włączona domyślna, reszta wyłączona).
			if (camers[i].name == defaultCamera)
			{
				camers[i].enabled = true;
			}else
			{
				camers[i].enabled = false;
			}
			camersTr.Add(camers [i].GetComponent<Transform> ());
		}
		isCameraToDistance = CheckGoodCamera (activeCamera);
		defRot[0] = camersTr[1].localRotation;
		defRot[1] = camersTr[6].localRotation;
		defPos[0] = camersTr[1].localPosition;
		defPos[1] = camersTr[6].localPosition;
		//Debug.Log(defRot[0]+" ma byc rowne: "+camersTr[1].localRotation);
	}

	// Update is called once per frame
	void Update () {
		//Przycisk zmiany kamery.
		if ((Input.GetMouseButtonDown (2) || Input.GetKeyDown(KeyCode.X)) && c <= camers.Length-3) {	//Jeśli wcisniemy klawisz Y to zmieniamy widok z kamery
			ChangeCams (c); 										// wywolanie funkcji zmieniajacej akrywna kamere
			if(c<camers.Length-4)									// -2 oznacza, że ostatnia kamera nie jest brana pod uwagę
				c++;
			else
				c=0;												//jesli c jest zbyt duze to wracamy do poczatku petli
		}
		if(isCameraToDistance == true)
			CheckObstacleBetweenCamAndPlayer (activeCameraTr.position, new Vector3(brumTr.position.x, brumTr.position.y+3.0f, brumTr.position.z), 1, isDefaultCamera, indexOfActCam);
	}
	private bool CheckGoodCamera (Camera cam)
	{
		for(int i = 0; i < camers.Length; i++)
		{
			if(camers[i] == cam){
				if(i == 0 || i == 1 || i == 6){
					if(i == 0){
						isDefaultCamera = true;
						activeCameraTr = cam.GetComponent<Transform>();
					}
					else{
						isDefaultCamera = false;
						activeCameraTr = cam.GetComponent<Transform>();
						temDistanceNonDef = CountDistance (activeCameraTr.position);
						WriteAllDefRot();
						indexOfActCam = i;
					}
					return true;
				}
				break;
			}
		}
		return false;
	}
	private void CheckObstacleBetweenCamAndPlayer (Vector3 camTr, Vector3 carTr, int layMask, bool defCam, int indeksik) //
	{
		//Debug.Log("dzialam");
		RaycastHit hit;
		LayerMask mask = layMask;
		if(Physics.Linecast(carTr, camTr, out hit, mask.value))
		{
			//Debug.Log(hit.collider.name);
			if(hit.collider.tag != "Player" && hit.collider.tag != "Teren" && hit.collider.tag != "ObstacleTag") // dzieje sie kiedy jakis obiekt znajduje sie miedzy kamera a brumem
			{
				//Debug.Log("cos jest pomiedzy playerem a kamera a nazwa to: "+ hit.collider.name);
				if(isTimer == false)
					isTimer = true;
			}
		}
		else 
		{
			if(isTimer == true)
				isTimer = false;
		}
		//Obsluguje kamere RCC
		if(defCam == true){
			if(isTimer == true)
			{
				if(hit.distance > minDist && hit.distance < tempDist)
					rcCam.distance = hit.distance;
				//Debug.Log("dystans do obiektu: "+ hit.collider.name+" wynosi: " +hit.distance);
			}
			else
			{
				if(rcCam.distance != tempDist)
					rcCam.distance = tempDist;
			}
		}
		else
		{	//co sie dzieje kiedy kolizja zostala wykryta
			if(isTimer == true)
			{
				camersTr[indeksik].RotateAround(new Vector3(brumTr.position.x, brumTr.position.y+3.0f, brumTr.position.z), -transform.forward, Time.deltaTime*40);
				Debug.Log("Dzialam indeks to: "+indeksik);
			}
			else
			{
				if(indeksik == 1){
					if(camersTr[indeksik].localRotation != defRot[0] && camersTr[indeksik].localPosition != defPos[0]){
						camersTr[indeksik].RotateAround(new Vector3(brumTr.position.x, brumTr.position.y+3.0f, brumTr.position.z), transform.forward, Time.deltaTime*5);
						Debug.Log(defRot[0]+" ma byc rowne: "+camersTr[indeksik].localRotation);
						//Debug.Log(defPos[0]+" ma byc rowne: "+camersTr[indeksik].localPosition);
					}
				}
				else if(indeksik == 6){
					if(camersTr[indeksik].localRotation != defRot[1] && camersTr[indeksik].localPosition != defPos[1]){
						camersTr[indeksik].RotateAround(new Vector3(brumTr.position.x, brumTr.position.y+3.0f, brumTr.position.z), transform.forward, Time.deltaTime*40);
					}
					//WriteAllDefRot();
				}
			}
		}
	}
	public void ChangeCams (int c)
	{

		for (int i = 0; i < camers.Length; i++) {
			if (i == c)
			{
				camers [i].enabled = true;
				activeCamera = camers [i];
				isCameraToDistance = CheckGoodCamera (activeCamera);
				//Debug.Log(camers[i] + " nastapila zmiana");

			}
			else
			{
				camers [i].enabled = false;
			}
		}
	}
	public void ExplosiveCamera ()									//Funkcja ktora jest wywoływana z poziomu skryptu
	{																//ExplosionScript w przpadku osiagniecia poziomu zycia
		for (int i = 0; i<=camers.Length-1; i++) {						//0 lub mniej.
			if (i == camers.Length-1) {
				camers [i].enabled = true;
				activeCamera = camers [i];
				isCameraToDistance = CheckGoodCamera (activeCamera);
			} else {
				camers [i].enabled = false;
			}
		}
	}
	private float CountDistance (Vector3 cam)
	{
		return Vector3.Distance(new Vector3(brumTr.position.x, brumTr.position.y+3.0f, brumTr.position.z), cam);
	}
	private void WriteAllDefRot()
	{
		camersTr[1].localRotation = defRot[0];
		camersTr[6].localRotation = defRot[1];
		camersTr[1].localPosition = defPos[0]; 
		camersTr[6].localPosition = defPos[1]; 
	}

}
/*
 * Działanie skryptu:
 * Do tablicy Kamer dodawane są poszczególne kamery które są dziećmi obiektu nadrzędnego.
 * Istotna jest kolejność. 
 * Pierwszą kamerą powinna być kamera domyslna Element [0], osatnią zaś kamera eksplozji.
 * W skrypcie klawiszem odpowiedzialnym za zmianę kamery jest klawisz na klawiaturze Y.
 * Zmiana kamery przez klawisz nie obowiązuje kamery eksplozji (jest ona uruchamiana automatycznie 
 * z poziomu skryptu ExplosionScript). Z wyjątkiem pierwszego i ostatniego elementu możliwa jest dowolna 
 * ilości oraz kolejności kamer.
 * */