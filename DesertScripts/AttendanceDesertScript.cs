using UnityEngine;
using System.Collections;

public class AttendanceDesertScript : MonoBehaviour {

	//public Button lostClose;
	public int numberOfTriggers = 2; // Okresla ilosc triggerow Sets amount of triggers
	public GameObject[] trigger = new GameObject[2]; //tablica triggerow w ktora bd wpisywane kolejne 
	public Canvas message;
	public Canvas radioFrame;

	//RCCCarControllerV2 carScript = gameObject.GetComponent<RCCCarControllerV2>();
	[HideInInspector]public int i = 0; // ogolna zmienna pomocnicza pod triggery misji
	//[HideInInspector]public int y = 0; // ogolna zmienna pomocniczya pod wiadomosci
	RCCCarControllerV2 rcc;
	// Use this for initialization
	void Start () {

		message = message.GetComponent<Canvas> ();
		radioFrame = radioFrame.GetComponent<Canvas> ();
		rcc = GetComponent<RCCCarControllerV2> ();

		//lostClose = lostClose.GetComponent<Button> ();
		for(int z = 0; z==numberOfTriggers; z++) //petla for po tablicy
		{
			trigger[z] = GameObject.FindGameObjectWithTag("Forest01Triggers"); //wpisywanie do tablicy obiektow z gry
		}
		Podmianka(i); // wywolanie metody podmianka
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
		for (int z = 0; z < numberOfTriggers; z++) // jedz po elementach tablicy
		{
			if (i == z) //jesli wartosc zmiennej wyslanej z metody jest rowna wartosci zmiennej petli to:
				trigger[z].SetActive(true); //wlaczenie danego obiektu
			else
				trigger[z].SetActive(false);//wylaczenie danego obiektu
		}
	}
		


	bool Zadania (int i) // funkcja odpowiedzialna za zapętlenie zadan w grze
	{
		switch (i) //case 0: - pierwszy prefab
		{

		case 0:
			return true;
			break;
		case 1: 
				return true;
			break;
		case 2:
			return true;
			break;
		case 3:
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