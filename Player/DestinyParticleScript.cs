using UnityEngine;
using System.Collections;
using System;

public class DestinyParticleScript : MonoBehaviour {
	int zmienna = 12; //zmienna w ktorej regulujemy natezenie poszczegolnych wartosci dla particle systemu
    int speedy = 0; //zmienna przechowywująca wartość prędkości
//    bool gotow = false; // zmienna która przechwytuje wartość ze skryptu DustScript czyMozna
    public GameObject obj; // sztywne przypisanie obiektu do skryptu
    void Update ()
    {
        funkcja(speedy); //odpalanie funkcji, w której podmieniamy poszczególne wartości zależne od zmiennej speedy
    }
    void funkcja (float x)
	{
		DustScript dss = obj.GetComponent<DustScript>();
        RCCCarControllerV2 brum = GetComponentInParent<RCCCarControllerV2>();
        speedy = (int)brum.speed;
 //       gotow = dss.czyMozna; // przypisanie zmiennej czyMozna ze skryptu DustScript do zmiennej gotow

        {
            ParticleSystem ps = this.GetComponent<ParticleSystem>(); //przypisanie do zmiennej ps poszczegolnych dwolan do Particle System
            
            ps.emissionRate = x / zmienna; //obliczanie wartości emision rate
            ps.startLifetime = x / (zmienna / 2); // obliczanie wartości start lifetime ..//..
            ps.startSize = x / zmienna;
            ps.maxParticles = (int)(x / 2);

        }
    }
}
/* Działanie skryptu:
 * Skrypt dynamicznie zmienia wartości: Emision Rate, Start Life Time, Start Size, Max Particles w systemie czastek Unity
 * odpowiedzialne za kurz generowany przez WheelSlipSand. 
 * Istotnym elementem aby skrypt działał jest przypisanie obiektu nadrzędnego w którym znajduje się skrypt DustScript.
 * Skrypt również pobiera wartość prędkości od której to zależy ilość kurzu (w naszym przypadku pobiera ze zmiennej speed
 * zawartej w skrypcie RCCCarControllerV2).
 * */