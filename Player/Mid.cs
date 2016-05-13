using UnityEngine;
using System.Collections;


public class Mid : MonoBehaviour {

	public GameObject Object;
	PlayerHealth playDmg;

	void Awake ()
	{
		playDmg = GetComponentInParent<PlayerHealth>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Trigger" && other.tag !=  "NonCollider" && other.tag !=  "Forest01Triggers")
		{
			playDmg.nameCollider = Object.name;
			if (playDmg.ifdamage == false) {
				playDmg.poObrazeniach = false;
				playDmg.ifdamage = true;
				playDmg.checkStayInCollider = true;
			}
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (playDmg.checkStayInCollider == true) {
			playDmg.ifdamage = false;
			playDmg.checkStayInCollider = false;
			if (playDmg.poObrazeniach == false) {
				playDmg.poObrazeniach = true;
			}
		}
	}
}
/*
 * Skrypt ten jest pomocniczym skryptem do określania tego, w którym koliderze nastąpiła kolicja.
 * Pod GameObject Object dokładnie ten kolider, do którego przypisaliśmy skrypt. Kolider wychwytuje 
 * wszystkie kolizje z wyłączeniem obiektów oznaczonych tagami Trigger lub NonCollider, 
 * dlatego istotne jest odpowiednie otagowanie wszelkich triggerów własnie tymi nazwami.
 * Trigger - są to triggery odpowiedzialne za etapy misji.
 * NonCollider - to obiekty niezależne od misji (np. trigger otwierania bramy).
 * */