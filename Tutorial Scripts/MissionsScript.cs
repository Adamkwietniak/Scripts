using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionsScript : MonoBehaviour
{
	
	//public Button lostClose;
	public int wpiszIloscTriggerow = 2;
	// Okresla ilosc triggerow Sets amount of triggers
	public GameObject[] trigger = new GameObject[2];
	//tablica triggerow w ktora bd wpisywane kolejne
	public Canvas message;
	public Canvas radioFrame;
	public Text hintAbouClose;
	public GameObject[] texts = new GameObject[1];
	private GameObject brumBrume;
	
	//RCCCarControllerV2 carScript = gameObject.GetComponent<RCCCarControllerV2>();
	public int i = 0;
	// ogolna zmienna pomocnicza pod triggery misji
	public int y = 0;
	// ogolna zmienna pomocniczya pod wiadomosci
	bool predkosc = false;
	bool checkPos = false;
	private Vector3 triggerTr;
	private Transform brumtr;
	RCCCarControllerV2 rcc;
	public Text kmh;
	public Text sideMirrors;
	public Text engineHelp;
	private bool engineHelpActive = false;
	VolumeAndMusicScript vms;
	// Use this for initialization
	void Start ()
	{
		brumBrume = GameObject.Find ("BrumBrume");
		radioFrame = radioFrame.GetComponent<Canvas> ();
		kmh.enabled = false;
		sideMirrors.enabled = false;
		vms = (VolumeAndMusicScript)FindObjectOfType (typeof(VolumeAndMusicScript));
		rcc = brumBrume.GetComponent<RCCCarControllerV2> ();
		message = message.GetComponent<Canvas> ();
		triggerTr = trigger [2].GetComponent<Transform> ().position;
		brumtr = brumBrume.GetComponent<Transform> ();
		//lostClose = lostClose.GetComponent<Button> ();
		for (int z = 0; z == wpiszIloscTriggerow; z++) { //petla for po tablicy
			trigger [z] = GameObject.FindGameObjectWithTag ("Trigger"); //wpisywanie do tablicy obiektow z gry
		}
		Messengery (y);
		Podmianka (i); // wywolanie metody podmianka
	}

	void Update ()
	{

		if ((int)rcc.speed > 60 && i == 3) {
			predkosc = true;
			i++;
			Podmianka (i);
		}

		if (y == 1) {					//Ify tu zostały przypisane ze względu na to, że pojawiają się one na samym
			Messengery (y);				//poczatku gry i sa niezależne od triggerów.
		}
		if (y == 2) {
			Messengery (y);
		}
		if (Input.GetKeyDown (KeyCode.C) && radioFrame.enabled == true) {		//wywołujemy zamykanie canvasa
			DisableEnableMsg ();
		}

		if (rcc.engineRunning == false && engineHelpActive == false) {
			engineHelp.enabled = true;
			engineHelpActive = true;
		} else if (rcc.engineRunning == true && engineHelpActive == true) {
			engineHelp.enabled = false;
			engineHelpActive = false;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) //wykrywanie kolizji
	{
		if (other.tag == "Trigger") { //sprawdzaj czy kolizja dotyczy obiektow o tagu Trigger
			if (Zadania (i) == true && i != 2) {
				i++; //zwieksz wartosc pomocnicza za kazdym razem gdy obiekt bedzie mial kontakt z triggerem
				Podmianka (i);//wywolanie metody podmianka i przeslanie wartosci i do metody
			}
			if (i == 2 && rcc.speed < 10 && checkPos == false) {
				checkPos = true;
				i++;
				Zadania (i);
				Podmianka (i);
			}
		}
	}

	void Podmianka (int i) //metoda Podmianka
	{
		for (int z = 0; z < wpiszIloscTriggerow; z++) { // jedz po elementach tablicy
			if (i == z) //jesli wartosc zmiennej wyslanej z metody jest rowna wartosci zmiennej petli to:
				trigger [z].SetActive (true); //wlaczenie danego obiektu
			else
				trigger [z].SetActive (false);//wylaczenie danego obiektu
		}
	}

	void Messengery (int y)// funkcja w zaleznosci od wartosci indexu y wlacza msg lub go wylacza
	{
		for (int z = 0; z < texts.Length; z++) {

			if (z == y) {
				radioFrame.enabled = true;
				hintAbouClose.enabled = true;
				vms.isMsg = true;
				Time.timeScale = 0; 	// Jeżeli gracz otrzymuje komunikat to gra się zatrzymuje. Po wciśnięciu
				texts [z].SetActive (true);//buttonu close gra wraca do standardowej prędkości.

			} else {
				texts [z].SetActive (false);
			}
		}
	}

	public void DisableEnableMsg ()			//to kurwa jest funkcja ktorej od teraz uzywamy do zamykania canvasów
	{
		foreach (GameObject mess in texts) {
			if (mess.activeInHierarchy == true) {
				radioFrame.enabled = false;
				hintAbouClose.enabled = false;
				mess.SetActive (false);
				vms.isMsg = false;
				Time.timeScale = 1;
				y++;
			}
		}
	}

	bool Zadania (int i) // funkcja odpowiedzialna za zapętlenie zadan w grze
	{
		switch (i) { //case 0: - pierwszy prefab
		case 0:
			sideMirrors.enabled = true;
			Messengery (y);
			return true;
			break;
		case 1:
			//if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			sideMirrors.enabled = false;
			Messengery (y);
			return true; 
			break;
		case 2:
			if (checkPos == true) {
				Messengery (y);
				return true;
			}
			break;
		case 3:
			kmh.enabled = true;
			Messengery (y);
			return true;
			break;
		case 4:
			kmh.enabled = false;
			Messengery (y);
			return true;
			break;
		case 5:
			Messengery (y);
			return true;
			break;
		case 18:
			Messengery (y);
			//return true;
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