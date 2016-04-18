using UnityEngine;
using System.Collections;
//Skrypt odpowiedzialny za eksplozje oraz warunkowanie, czy eksplozja ma sie wywołać czy nie.
public class ExplosionScript : MonoBehaviour {

	public GameObject [] exploPrefab = new GameObject[1]; // Tablica ze wszystkicmi obiektami eksplozji
	public GameObject obj; //Podpiecie obiektu glownego z ktorego będziemy pobierać wartość życia
	public GameObject camExp;
	int aktualLive = 0; // do zmiennej przypisujemy wartość życia samochodu

	// Na poczatku wyłączamy wszystkie eksplozje
	void Start () { 

		foreach (GameObject expl in exploPrefab) {
			expl.SetActive(false);
		}
	}
	void Update () {
		//LiveScript ls = GetComponentInParent <LiveScript> ();
		PlayerHealth ph = obj.GetComponent<PlayerHealth> ();//Pobieramy komponent ze skryptu PlayerHealth
		aktualLive = ph.currentHealth;						//po czym przypisujemy wartość życia do naszej zmiennej

		if (aktualLive <= 0) {					//Jeśli ilość życia jest mniejsza lub równa 0
			StartCoroutine (Uzycie ());			//wywołujemy funkcję odpowiedzialną za wybuch
		}
	}
	public IEnumerator Uzycie ()						//Funkcja odpowiedzialna za wybuch
	{
		UseCameraScript ucs = camExp.GetComponent<UseCameraScript> ();
		ucs.ExplosiveCamera ();
		Time.timeScale = 0.2f;							//Zmniejszamy wartość time (efekt slow motion)
		foreach (GameObject expl in exploPrefab) {			//Włączamy prefaby z zawartymi eksplozjami
			expl.SetActive(true);
			}
			yield return new WaitForSeconds (2.5f);			//Czekamy
			Time.timeScale = 1.0f;							//Wyłączamy slow motion
	}
}
/* Działanie skryptu:
 * Do tablicy wrzucamy kolejne obiekty z zawartymi eksplozjami, po czym skrypt sam je uruchamia
 * kiedy nasz player będzie miał 0 życia.
 * Istotne jest to, aby do skryptu dodać obiekt zawierający skrypt życia (w naszym przypadku jest to truck_low2).
 * */