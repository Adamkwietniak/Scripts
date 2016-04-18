using UnityEngine;
using System.Collections;


public class Mid : MonoBehaviour {

	public GameObject Object;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Trigger" && other.tag !=  "NonCollider" && other.tag !=  "Forest01Triggers")
		{
			PlayerHealth playDmg = GetComponentInParent<PlayerHealth>();
			playDmg.nameCollider = Object.name;
            playDmg.ifdamage = true;
			playDmg.checkStayInCollider = true;
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