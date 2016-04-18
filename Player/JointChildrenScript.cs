using UnityEngine;
using System.Collections;
//Jest to skrypt pomocniczy dodawany do podobiektów w obiekcie nadrzędnym
public class JointChildrenScript : MonoBehaviour {

	public float emiR = 0;
	public int maxim = 0;
	JointScript js;
	ParticleSystem ps;

	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
		js = GetComponentInParent<JointScript> ();
	}
	void Update()
	{
		maxim = js.maxParticle;	//Przypisanie nowej wartosci do zmiennych odpowiedzialnych za ilość
		emiR = js.emizionRate;	//cząstek obliczonych w skrypcie "matce"
		PodmienWartosci (maxim, emiR); //Wywołanie funkcji
	}

	void PodmienWartosci (int x, float r)
	{
		ps.maxParticles = x;	//Podmiana nowych wartości pod stare w 
		ps.emissionRate = r;	//systemie czastek
	}
}
/*Działanie skryptu:
 * Skrypt ten jest niezbędny do prawidłowego działania JointScript ponieważ podmienia wartości w poszczególnych obiektach.
 * Skrypt ten pobiera dane ze skryptu "matki" po czym podmienia wartości bezpośrednio w systemie cząstek.
 * */