using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionRiverScript : MonoBehaviour {

	public int wpiszIloscTriggerow = 2; // Okresla ilosc triggerow Sets amount of triggers
	public GameObject[] trigger = new GameObject[2]; //tablica triggerow w ktora bd wpisywane kolejne 
	public Canvas message;
	public Canvas radioFrame;
	public GameObject [] texts = new GameObject[1];
	[HideInInspector]public int i = 0; // ogolna zmienna pomocnicza pod triggery misji
	[HideInInspector]public int y = 0; // ogolna zmienna pomocniczya pod wiadomosci
	RCCCarControllerV2 rcc;
	public Image blackScreen;
	private float timer1;
	private bool blackScreenIs = false;
	private bool clearingBlackScreen = false;
	public GameObject obstacleToCleanWay;
	[HideInInspector]public bool firstOfSecondScript = false;
	PlayerHealth ph;
	GetOutFromWayScript gafw;
	public Text instructionsForBumper;
	public GameObject[] wounded = new GameObject[3];
	public Image engineWarning;

	//public GameObject goalMC;

	void Awake ()
	{
		ph = GetComponentInChildren<PlayerHealth>();
		blackScreen.enabled = false;
		radioFrame = radioFrame.GetComponent<Canvas> ();
		rcc = GetComponent<RCCCarControllerV2> ();
		gafw = GetComponent<GetOutFromWayScript> ();
		obstacleToCleanWay.SetActive(false);
		engineWarning.enabled = false;

		timer1 = 0;
		//goalMC.SetActive (false);
	}
	void Start () {
		message = message.GetComponent<Canvas> ();
		instructionsForBumper.enabled = false;
		for (int o = 0; o < wounded.Length; o++) {
			wounded [o].SetActive (false);
		}
		for(int z = 0; z==wpiszIloscTriggerow; z++) //petla for po tablicy
		{
			trigger[z] = GameObject.FindGameObjectWithTag("Trigger"); //wpisywanie do tablicy obiektow z gry
		}
		Messengery (y);
		Podmianka(i); // wywolanie metody podmianka
	}

	
	void Update()
	{
		if(firstOfSecondScript == false){
			if (y == 0) {					//Ify tu zostały przypisane ze względu na to, że pojawiają się one na samym
				Messengery (y);				//poczatku gry i sa niezależne od triggerów.
			}
			if(y == 6) 						//Włącz black screen
			{
				if (blackScreen.enabled == false) {
					blackScreen.enabled = true;
					blackScreenIs = true;

				}
				blackScreen.color = new Color(0,0,0,Mathf.Clamp(timer1, 0, 255));
			}
			if(blackScreenIs == true && clearingBlackScreen == false)
			{
				if(timer1<1)
					timer1 += Time.deltaTime/3;
				else 
				{
					timer1 = 1;
					ph.RepairCar();
					obstacleToCleanWay.SetActive(true);
					blackScreenIs = false;
					gafw.reachBase = true;
					clearingBlackScreen = true;



				}
			}
			if(clearingBlackScreen == true && blackScreenIs == false)
			{
				if(timer1>0)
					timer1 -= Time.deltaTime/3;
				else 
				{
					timer1 = 0;
					clearingBlackScreen = false;
					instructionsForBumper.enabled = true;
					blackScreen.enabled = false;
					firstOfSecondScript = true;
					engineWarning.enabled = false;
					for (int o = 0; o < wounded.Length; o++) {
						wounded [o].SetActive (true);
					
					}

				}
			}
			DisEnbl();
		}
		if (gafw.isComplete == true && i == wpiszIloscTriggerow-2) {
			i++;
			Podmianka (i);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) //wykrywanie kolizji
	{
		if (other.tag == "Trigger") //sprawdzaj czy kolizja dotyczy obiektow o tagu Trigger
		{
			if (Zadania(i) == true)
			{
				i++; //zwieksz wartosc pomocnicza za kazdym razem gdy obiekt bedzie mial kontakt z triggerem
				Podmianka(i);//wywolanie metody podmianka i przeslanie wartosci i do metody
			}
		}
	}
	void Podmianka(int i) //metoda Podmianka
	{
		for (int z = 0; z < wpiszIloscTriggerow; z++) // jedz po elementach tablicy
		{
			if (i == z) //jesli wartosc zmiennej wyslanej z metody jest rowna wartosci zmiennej petli to:
				trigger[z].SetActive(true); //wlaczenie danego obiektu
			else
				trigger[z].SetActive(false);//wylaczenie danego obiektu
		}
	}
	
	void Messengery (int y)// funkcja w zaleznosci od wartosci indexu y wlacza msg lub go wylacza
	{
		for (int z=0; z<texts.Length; z++) {
			
			if (z == y)
			{
				radioFrame.enabled = true;
				Time.timeScale = 0; 	// Jeżeli gracz otrzymuje komunikat to gra się zatrzymuje. Po wciśnięciu
				texts[z].SetActive(true);//buttonu close gra wraca do standardowej prędkości.
				
			}
			else
			{
				texts[z].SetActive(false);
			}
		}
	}
	void DisEnbl ()
	{
		if(Input.GetKeyUp (KeyCode.C))
		{
		foreach (GameObject mess in texts) {
			if (mess.activeInHierarchy == true)
				{
				mess.SetActive(false);
				radioFrame.enabled = false;
				Time.timeScale = 1;
				y++;
				}
			}
		}
	}
	
	bool Zadania (int i) // funkcja odpowiedzialna za zapętlenie zadan w grze
	{
		switch (i) //case 0: - pierwszy prefab
		{
		case 0:
			Messengery (y);
			return true;
			break;
		case 1: 
			Messengery (y);
			return true;
			break;
		case 2:
			Messengery (y);
			return true;
			break;
		case 3:
			Messengery (y);
			return true;
			break;
		case 4:
			Messengery (y);
			return true;
			break;
		case 5:
			Messengery (y);
			blackScreenIs = true;
			return true;
			break;
		case 6:
			if (gafw.isComplete == true) {
				return true;
			}
			break;

		default:
			return true;
			break;
		}
		return false;
	}
}

/* Działanie skryptu
 * Skrypt pobiera obiekty odpowiedzialne misje. Podpiecie tego skryptu do playera umozliwia wrzucenie do tablic
 * kolejno następujących po sobie etapów misji, oraz wyświetlenie informacji zależnie od etapu w którym znajduje się gracz.
 * Do tablicy Trigger wrzucamy kolejno następujące po sobie etapy misji.
 * Uwaga triggery misyjne muszą mieć tag Trigger w przeciwnym razie skrypt nie bedzie ich uwzględniał.
 * Zmienna odpowiedzialna za "etapowanie" jest zmienna i.
 * Wszelkie istotne zmiany oraz dokładne warunki poszczególnych misji jak również wyświetlania informacji zapisujemy wewnątrz 
 * instrukcji warunkowej switch zawartej w funkcji zadania.
 * Do poprawnego działania niezbędne jest podpięcie Canvasa głównego wyświetlającego kolejne wiadomości oraz poszczególne 
 * wiadomosci "text" jako oddzielne obiekty. Wszelkie prawa zastrzeżone. All rights reserved.
 * */