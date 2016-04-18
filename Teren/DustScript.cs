using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {

    public GameObject[] dustParticle = new GameObject[1]; // tabliza z particle systemami kurzu
    public bool czyMozna = false; // zmienna przechująca wartość sprawdzającą czy prędkość jest wystarczająca
								 // załączenia kurzu
    private int predkosc = 0;	// zmienna przechowująca wartość prędkości samochodu pobrana z RCCCarControllerV2
	public bool sprawdzam = false; // zmienna której wartość jest odbierana z SprawdzTerenScript w której przechowywane jest
								   // czy dana tekstura zezwala na kurz
   

    // Use this for initialization
    void Start () {
	    foreach (GameObject dust in dustParticle) //przy załadowaniu sceny ma wyłączyć kurz
        {
            dust.SetActive(false); 
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        if (czyMozna == true && sprawdzam == true) //jeśli warunki są spełnione włącza kurz
        {

            foreach (GameObject dust in dustParticle)
            {
                dust.SetActive(true);
            }
            UstawPredkosc(); //odpalenie funkcji sprawdzającej czy mamy wystarczającą prędkość pojazdu
        }
        else
        {
            foreach (GameObject dust in dustParticle) //jeśli warunki nie są spełnione wyłącza kurz
            {
                dust.SetActive(false);
            }
			UstawPredkosc(); //odpalenie funkcji sprawdzającej czy mamy wystarczającą prędkość pojazdu
        }
    }
    void UstawPredkosc () //funkcja w której sprawdzamy, czy prędkość jest wystarczająca do odpalenia kurzu
    {
        RCCCarControllerV2 brum = GetComponentInParent<RCCCarControllerV2>();
        predkosc = (int)brum.speed;
        if (predkosc > 15)
        {
            czyMozna = true;
        }

        else
            czyMozna = false;
    }
   
   
}



