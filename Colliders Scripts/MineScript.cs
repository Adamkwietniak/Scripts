using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineScript : MonoBehaviour {

	public float power = 5000.0f;	//Wartość określająca moc wybichu
	public int radius = 20;			//Wartość określająca sasięg wybuchu
	public Vector3 minePos;
	public GameObject playerObjectwithHealth;
	private Transform trPosition;
	public Rigidbody PlayerRB;
	public bool checkTrigger = false;
	public GameObject prefabExplos;
	public string checkCollider = null;
	public GameObject [] minesTab = new GameObject[1];
	//private int helpTemp = 0;	//Zmienna pomocnicza która przechowuje wartość konkretnego kollidera
	public bool isMine = false;	//Zmienna pomocnicza mająca na celu stwierdzenie czy doszlo do kolizji
	public int lengthTerrain = 0;
	public int widthTerrain = 0;
	private int lengthOfTerrain = 0;
	private int widthOfTerrain = 0;
	private List<GameObject> firstQuarter = new List<GameObject>();
	private List<GameObject> secondQuarter = new List<GameObject>();
	private List<GameObject> thirdQuarter = new List<GameObject>();
	private List<GameObject> fourthQuarter = new List<GameObject>();



	PlayerHealth ph;

	//Light
	private List<Light> firstLightQuarter = new List<Light>();
	private List<Light> secondLightQuarter = new List<Light>();
	private List<Light> thirdLightQuarter = new List<Light>();
	private List<Light> fourthLightQuarter = new List<Light>();

	private List<Transform> firstTransformLightQuarter = new List<Transform>();
	private List<Transform> secondTransformLightQuarter = new List<Transform>();
	private List<Transform> thirdTransformLightQuarter = new List<Transform>();
	private List<Transform> fourthTransformLightQuarter = new List<Transform>();

	private List<float> firstDistanceQuarter = new List<float>();
	private List<float> secondDistanceQuarter = new List<float>();
	private List<float> thirdDistanceQuarter = new List<float>();
	private List<float> fourthDistanceQuarter = new List<float>();

	private List<bool> booling1 = new List<bool>();
	private List<bool> booling2 = new List<bool>();
	private List<bool> booling3 = new List<bool>();
	private List<bool> booling4 = new List<bool>();

	private List<bool> minMax1 = new List<bool>();
	private List<bool> minMax2 = new List<bool>();
	private List<bool> minMax3 = new List<bool>();
	private List<bool> minMax4 = new List<bool>();

	private List<float> intensity1 = new List<float>();
	private List<float> intensity2 = new List<float>();
	private List<float> intensity3 = new List<float>();
	private List<float> intensity4 = new List<float>();

	public float maxIntensity = 1.5f; // Określa maksymalną wartość graniczną dla intensity
	public float speedOfIntensity = 1; // Określa prędkość zmiany wartości w jednej jednostce czasu
	public float minDistance = 50;	// Określa odległość od której jest uzależniona widoczność światła
	// Use this for initialization
	void Awake ()
	{
		ph = playerObjectwithHealth.GetComponent<PlayerHealth> ();
		trPosition = playerObjectwithHealth.GetComponent<Transform>();
		lengthOfTerrain = lengthTerrain/2;
		widthOfTerrain = widthTerrain/2;
		for(int i = 0; i < minesTab.Length; i++)
		{
			if(minesTab[i].GetComponent<Transform>().position.x < lengthOfTerrain && // pierwsza cwiartka
				minesTab[i].GetComponent<Transform>().position.z < widthOfTerrain)
			{
				firstQuarter.Add(minesTab[i]);
				firstLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>());
				firstTransformLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>());
				firstDistanceQuarter.Add(CountDistance(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>().position)); // dopisz vector3 miny
				//timer1.Add(0);
				booling1.Add(false);
				intensity1.Add(0);
				minMax1.Add(false);
				//Debug.Log("Dodałem: "+minesTab[i].name+" i jestem w cwiarcte: 1");
			}
			else if(minesTab[i].GetComponent<Transform>().position.x >= lengthOfTerrain && // pierwsza cwiartka
				minesTab[i].GetComponent<Transform>().position.z < widthOfTerrain)
			{
				secondQuarter.Add(minesTab[i]);
				secondLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>());
				secondTransformLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>());
				secondDistanceQuarter.Add(CountDistance(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>().position));
				//timer2.Add(0);
				booling2.Add(false);
				intensity2.Add(0);
				minMax2.Add(false);
				//Debug.Log("Dodałem: "+minesTab[i].name+" i jestem w cwiarcte: 2");
			}
			else if(minesTab[i].GetComponent<Transform>().position.x < lengthOfTerrain && // pierwsza cwiartka
				minesTab[i].GetComponent<Transform>().position.z >= widthOfTerrain)
			{
				thirdQuarter.Add(minesTab[i]);
				thirdLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>());
				thirdTransformLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>());
				thirdDistanceQuarter.Add(CountDistance(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>().position));
				//timer3.Add(0);
				booling3.Add(false);
				intensity3.Add(0);
				minMax3.Add(false);
				//Debug.Log("Dodałem: "+minesTab[i].name+" i jestem w cwiarcte: 3");
			}
			else if(minesTab[i].GetComponent<Transform>().position.x >= lengthOfTerrain && // pierwsza cwiartka
				minesTab[i].GetComponent<Transform>().position.z >= widthOfTerrain)
			{
				fourthQuarter.Add(minesTab[i]);
				fourthLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>());
				fourthTransformLightQuarter.Add(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>());
				fourthDistanceQuarter.Add(CountDistance(minesTab[i].GetComponentInChildren<Light>().GetComponent<Transform>().position));
				//timer4.Add(0);
				booling4.Add(false);
				intensity4.Add(0);
				minMax4.Add(false);
				//Debug.Log("Dodałem: "+minesTab[i].name+" i jestem w cwiarcte: 4");
			}
		}

		minePos = new Vector3 (0, 0, 0);
	}

	void Start () {
		prefabExplos.SetActive (false);
	}
	void Update () {

		if (checkTrigger == true) // Wywolanie funkcji wybuchu
			ForcesRbs (ReturnPosition ());
		MakeLight ();
	}
	private void ForcesRbs (int helpingQuarter)
	{
		if (helpingQuarter == 1){
			for(int i = 0; i < firstQuarter.Count; i++)
			{
				if(firstQuarter[i].name == checkCollider && isMine == true){
					prefabExplos.transform.position = minePos;
					prefabExplos.SetActive (true);
					ForceRB ();
				}
			}
		}
		if (helpingQuarter == 2){
			for(int i = 0; i < secondQuarter.Count; i++)
			{
				if(secondQuarter[i].name == checkCollider && isMine == true){
					prefabExplos.transform.position = minePos;
					prefabExplos.SetActive (true);
					ForceRB ();
				}
			}
		}
		if (helpingQuarter == 3){
			for(int i = 0; i < thirdQuarter.Count; i++)
			{
				if(thirdQuarter[i].name == checkCollider && isMine == true){
					prefabExplos.transform.position = minePos;
					prefabExplos.SetActive (true);
					ForceRB ();
				}
			}
		}
		if (helpingQuarter == 4){
			for(int i = 0; i < fourthQuarter.Count; i++)
			{
				if(fourthQuarter[i].name == checkCollider && isMine == true){
					prefabExplos.transform.position = minePos;
					prefabExplos.SetActive (true);
					ForceRB ();
				}
			}
		}
	}
	private int ReturnPosition ()
	{
		if(trPosition.position.x < lengthOfTerrain && trPosition.position.z < widthOfTerrain)
		{
			return 1;
		}
		else if(trPosition.position.x >= lengthOfTerrain && trPosition.position.z < widthOfTerrain)
		{
			return 2;
		}
		else if(trPosition.position.x < lengthOfTerrain && trPosition.position.z >= widthOfTerrain)
		{
			return 3;
		}
		else
		{
			return 4;
		}
	}
	private void ForceRB () //Metoda zmieniająca poszczególne wartości dla rigBody samochodu w trakcje wjechania na mine.
	{
		PlayerRB.isKinematic = false;
		PlayerRB.useGravity = false;
		PlayerRB.angularDrag = 0.05f;
		PlayerRB.AddExplosionForce (power, minePos, radius, 3.0F); // MOC , POZYCJA EXPLOZJI, ZASIEG, 3.0F
		//PlayerRB.AddForce(minePos);
		//PlayerRB.isKinematic = false;
		PlayerRB.useGravity = true;
		ph.currentHealth = 0;
	}
	private float CountDistance (Vector3 mine)
	{
		return Vector3.Distance(trPosition.position, mine);
	}
	private void MakeLight ()
	{
		switch (ReturnPosition ())
		{
		case 1:
			BlinkingCount (1);
			break;
		case 2:
			BlinkingCount (2);
			break;
		case 3:
			BlinkingCount (3);
			break;
		case 4:
			BlinkingCount (4);
			break;
		
		default:
			break;
		}	
	}
	private void BlinkingCount (int querere)
	{
		if(querere == 1)
		{
			if(firstLightQuarter.Count>0){
				for(int i = 0; i < firstLightQuarter.Count; i++)
				{
					firstDistanceQuarter[i] = CountDistance (firstTransformLightQuarter[i].position);
					if(firstDistanceQuarter[i] < minDistance && booling1[i] == false){
						if(firstLightQuarter[i].enabled == false)
							firstLightQuarter[i].enabled = true;
						booling1[i] = true;
					}
					else if(firstDistanceQuarter[i] >= minDistance && booling1[i] == true){
						if(firstLightQuarter[i].enabled == true)
							firstLightQuarter[i].enabled = false;
						booling1[i] = false;
						if(intensity1[i] != 0)
							intensity1[i] = 0;
					}
					if(booling1[i] == true)
					{

						if(firstLightQuarter[i].intensity > maxIntensity)
							minMax1[i] = false;
						else if(firstLightQuarter[i].intensity <= 0)
							minMax1[i] = true;
						intensity1[i] = PulsedLight (intensity1[i], minMax1[i]);
						firstLightQuarter[i].intensity = intensity1[i];
					}
				}
			}
		}
		if(querere == 2)
		{
			if(secondLightQuarter.Count>0){
				for(int i = 0; i < secondLightQuarter.Count; i++)
				{
					secondDistanceQuarter[i] = CountDistance (secondTransformLightQuarter[i].position);
					if(secondDistanceQuarter[i] < minDistance && booling2[i] == false){
						if(secondLightQuarter[i].enabled == false)
							secondLightQuarter[i].enabled = true;
						booling2[i] = true;
					}
					else if(secondDistanceQuarter[i] >= minDistance && booling2[i] == true){
						if(secondLightQuarter[i].enabled == true)
							secondLightQuarter[i].enabled = false;
						booling2[i] = false;
						if(intensity2[i] != 0)
							intensity2[i] = 0;
					}
					if(booling2[i] == true)
					{
						if(secondLightQuarter[i].intensity > maxIntensity)
							minMax2[i] = false;
						else if(secondLightQuarter[i].intensity <= 0)
							minMax2[i] = true;
						intensity2[i] = PulsedLight (intensity2[i], minMax2[i]);
						secondLightQuarter[i].intensity = intensity2[i];
					}
				}
			}
		}
		if(querere == 3)
		{
			if(thirdLightQuarter.Count>0){
				for(int i = 0; i < thirdLightQuarter.Count; i++)
				{
					thirdDistanceQuarter[i] = CountDistance (thirdTransformLightQuarter[i].position);
					if(thirdDistanceQuarter[i] < minDistance && booling3[i] == false){
						if(thirdLightQuarter[i].enabled == false)
							thirdLightQuarter[i].enabled = true;
						booling3[i] = true;
					}
					else if(thirdDistanceQuarter[i] >= minDistance && booling3[i] == true){
						booling3[i] = false;
						if(thirdLightQuarter[i].enabled == true)
							thirdLightQuarter[i].enabled = false;

						if(intensity3[i] != 0)
							intensity3[i] = 0;
					}
					if(booling3[i] == true)
					{
						if(thirdLightQuarter[i].intensity > maxIntensity)
							minMax3[i] = false;
						else if(thirdLightQuarter[i].intensity <= 0)
							minMax3[i] = true;
						intensity3[i] = PulsedLight (intensity3[i], minMax3[i]);
						thirdLightQuarter[i].intensity = intensity3[i];
					}
				}
			}
		}
		if(querere == 4)
		{
			if(fourthLightQuarter.Count>0){
				for(int i = 0; i < fourthLightQuarter.Count; i++)
				{
					fourthDistanceQuarter[i] = CountDistance (fourthTransformLightQuarter[i].position);
					if(fourthDistanceQuarter[i] < minDistance && booling4[i] == false){
						if(fourthLightQuarter[i].enabled == false)
							fourthLightQuarter[i].enabled = true;
						booling4[i] = true;
					}
					else if(fourthDistanceQuarter[i] >= minDistance && booling4[i] == true){
						booling4[i] = false;
						if(fourthLightQuarter[i].enabled == true)
							fourthLightQuarter[i].enabled = false;
						if(intensity4[i] != 0)
							intensity4[i] = 0;
					}
					if(booling4[i] == true)
					{
						if(fourthLightQuarter[i].intensity > maxIntensity)
							minMax4[i] = false;
						else if(fourthLightQuarter[i].intensity <= 0)
							minMax4[i] = true;
						intensity4[i] = PulsedLight (intensity4[i], minMax4[i]);
						fourthLightQuarter[i].intensity = intensity4[i];
					}
				}
			}
		}
	}
	private float PulsedLight (float i, bool minMax) //Metoda do obsługiwania intensywoności światła w jednej jednostce czasu (tworzy iluzję pulsowania)
	{
		
		if (minMax == true) {
			i += Time.deltaTime * speedOfIntensity;
		} else if (minMax == false) {
			i -= Time.deltaTime * speedOfIntensity;
		}
		return i;
	}
}
