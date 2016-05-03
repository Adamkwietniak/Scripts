using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionForestScript : MonoBehaviour {
	
	//public Button lostClose;
	public int wpiszIloscTriggerow = 2; // Okresla ilosc triggerow Sets amount of triggers
	public GameObject[] trigger = new GameObject[2]; //tablica triggerow w ktora bd wpisywane kolejne 
	public Canvas message;
	public Canvas radioFrame;
	public GameObject [] texts = new GameObject[1];
	public GameObject villageIsThere;
	private SkinnedMeshRenderer smr1;
	private SkinnedMeshRenderer smr2;
	public Text engineHelp;
	private bool engineHelpActive = false;
	
	//RCCCarControllerV2 carScript = gameObject.GetComponent<RCCCarControllerV2>();
	[HideInInspector]public int i = 0; // ogolna zmienna pomocnicza pod triggery misji
	[HideInInspector]public int y = 0; // ogolna zmienna pomocniczya pod wiadomosci
	bool predkosc = false;
	[HideInInspector]public bool czyDalej = false;
	private bool tempCzyDalej = false;
	AllianceSoliderForestEvent ase;
	RCCCarControllerV2 rcc;
	MissionCompleteForestScript mcfs;
	VolumeAndMusicScript vms;
	// Use this for initialization
	void Start () {

		message = message.GetComponent<Canvas> ();
		radioFrame = radioFrame.GetComponent<Canvas> ();
		rcc = GetComponent<RCCCarControllerV2> ();
		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));

		//lostClose = lostClose.GetComponent<Button> ();
		for(int z = 0; z==wpiszIloscTriggerow; z++) //petla for po tablicy
		{
			trigger[z] = GameObject.FindGameObjectWithTag("Trigger"); //wpisywanie do tablicy obiektow z gry
		}
		Messengery (y);
		Podmianka(i); // wywolanie metody podmianka
		villageIsThere.SetActive (false);
		mcfs = trigger [trigger.Length - 1].GetComponent<MissionCompleteForestScript> ();
	}
	
	void Update()
	{
		
		if (y == 1) {					//Ify tu zostały przypisane ze względu na to, że pojawiają się one na samym
			Messengery (y);				//poczatku gry i sa niezależne od triggerów.
		}
		if(i == 1 && czyDalej == true && tempCzyDalej == false){
			//Debug.Log("dzialam");
			Messengery (y);
			villageIsThere.SetActive (false);
			tempCzyDalej = true;
		}

		if (i == 4) {
			mcfs.EnabledMissionComplete();
		}

		if (Input.GetKeyDown (KeyCode.C) && radioFrame.enabled == true) {		//wywołujemy zamykanie canvasa
			DisableEnableMsg ();
		}

		if (rcc.engineRunning == false && engineHelpActive == false) {
			engineHelp.enabled = true;
			engineHelpActive = true;
		} else if(rcc.engineRunning == true && engineHelpActive == true) {
			engineHelp.enabled = false;
			engineHelpActive = false;
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
				vms.isMsg = true;
				Time.timeScale = 0; 	// Jeżeli gracz otrzymuje komunikat to gra się zatrzymuje. Po wciśnięciu
				texts[z].SetActive(true);//buttonu close gra wraca do standardowej prędkości.
				Cursor.visible = true;
				
			}
			else
			{
				texts[z].SetActive(false);
			}
		}
	}
	/*void DisEnbl ()
	{
		foreach (GameObject mess in texts) {
			//if (mess.activeInHierarchy == true)
			//{
				if(Input.GetKeyUp (KeyCode.C))
				{
					radioFrame.enabled = false;
					mess.SetActive(false);
					vms.isMsg = false;
					Time.timeScale = 1;
					y++;
				}
			//}
		}
	}*/
	public void DisableEnableMsg ()			//to kurwa jest funkcja ktorej od teraz uzywamy do zamykania canvasów
	{
		foreach (GameObject mess in texts) {
			if (mess.activeInHierarchy == true) {
				radioFrame.enabled = false;
				mess.SetActive (false);
				vms.isMsg = false;
				Time.timeScale = 1;
				y++;
			}
		}
	}

	bool Zadania (int i) // funkcja odpowiedzialna za zapętlenie zadan w grze
	{
		switch (i) //case 0: - pierwszy prefab
		{
		
		case 0:
			Messengery (y);
			villageIsThere.SetActive (true);
			return true;
			break;
		case 1: //vilidżer
			rcc.speed = 0;
			//Messengery (y);
			//villageIsThere.SetActive (false);
			if(tempCzyDalej == true)
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