using UnityEngine;
using System.Collections;
//Skrypt jest odpowiedzialny za dym wylatujący z maski samochodu z dynamiczną ilością dymu
public class JointScript : MonoBehaviour {

	public float emizionRate = 0; //deklarujemy i przypisujemy domyślne wartości dla zmiennych
	public int maxParticle = 0;
	int zycie = 0;
	bool czyMozna = false; //Zmienna okresla czy ilość życia pojazdu spadła na tyle aby aktywować system cząstek
	PlayerHealth ph;

	public GameObject [] tab = new GameObject[1]; // Do tej tablicy przypisujemy manualnie elementy 
	// Domyślne przypisanie wartości				 systemu cząstek Unity.
	void Start () {
		ph = GetComponentInParent<PlayerHealth> ();
		zycie = ph.currentHealth;
		if (zycie < 60) {							//Jeśli ilość życia samochodu jest mniejsza niż 60 aktywujemy
			foreach (GameObject parti in tab) {		//pętlę w której włączamy obiekty w których zawarty jest system cząstek
				parti.SetActive(true);				//dymu
			}czyMozna = true;

		} else {
			foreach (GameObject parti in tab) {		//Jeśli warunek nie jest spełniony dym nie jest aktywowany
				parti.SetActive(false);			} 
			czyMozna = false;
		}
	}
	// Update is called once per frame
	void Update () {
		zycie = ph.currentHealth;
		if (zycie < 60 && czyMozna == false) {
			czyMozna = true;
			foreach (GameObject parti in tab) {
				parti.active = true;
			}
		}

		if (czyMozna == true) {
			 {
				emizionRate = (float)((40 / (zycie + 0.1f))+0.65f); //Dynamiczne przypisanie wartości dla dymu
				if(zycie > 0)
					maxParticle = (int)(1000 / (zycie))+300;
				else
					maxParticle = (int)(1000 / 1)+300;
			}
		}
	}
}

/* Działanie skryptu:
 * Skrypt na podstawie pobranej wartości życia playera (w naszym przypadku jest to BrumBrum(1)) określa czy dym spod maski
 * ma zostać aktywowany czy nie, oraz uzależnia niektóre wartości z particle systemu od aktualnego stanu życia pojazdu.
 * Wymaga podczepienia skryptu JointChildrenScript do obiektów "dzieci".
 * */