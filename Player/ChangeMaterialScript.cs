using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeMaterialScript : MonoBehaviour {

	public GameObject[] materialsInCar = new GameObject[1];
	public GameObject[] materialsOfTires = new GameObject[4];
	public Texture textureToChangedCar;
	public Texture destroyedCarTexture;
	private Texture cleanCar;
	private Texture cleanTireMaterial;
	private float timeToChange = 250;
	private float timeToChanching = 4;
	private bool isTimerChanged = false;
	private bool timer1 = false;
	private float tim1 = 0;
	private float tim2 = 0;
	private bool counting = false;
	private bool timi = false;
	private bool chang = false;
	private bool isLife = true;
	private bool textureChanged = false;
	private Renderer[] truckBody = new Renderer[1];
	private Renderer[] tiresBody = new Renderer[4];
	MudScript ms;
	PlayerHealth ph;
	// Use this for initialization


	void Start () {
		cleanTireMaterial = materialsOfTires [0].GetComponent<Renderer> ().material.mainTexture;
		cleanCar = materialsInCar [0].GetComponent<Renderer> ().material.mainTexture;
		ms = this.GetComponent<MudScript> ();
		ph = GetComponentInChildren<PlayerHealth> ();
		for (int i = 0; i < materialsInCar.Length; i++) {
			truckBody[i] = materialsInCar[i].GetComponent<Renderer>();
		}
		for (int i = 0; i < materialsOfTires.Length; i++) {
			tiresBody[i] = materialsOfTires[i].GetComponent<Renderer>();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer1 == false) {
			tim1 += Time.deltaTime;
			//Debug.Log("timer wynosi: " + tim1);
			if(tim1>=timeToChange && chang == false)
			{
				isTimerChanged = true;
				timer1 = true;
				chang = true;
			}
		}

		if (counting == false) {
  			for(int i = 0; i < ms.psTire.Count; i++)
			{
				if(ms.psTire[i].generateMud == true){
					counting = true;
					timi = true;
					break;
				}
			}
		}
		if (counting == true && tim2 <=timeToChanching && timi == true) {
			tim2+=Time.deltaTime;
		}
		else if(counting == true && tim2 > timeToChanching){
			tim2 = timeToChanching;
			counting = false;
		}
		if (counting == false && tim2 > 0 && timi == true) {
			tim2-=Time.deltaTime;
		}
		if(isLife == true)
		{
			if(ph.currentHealth<=0)
				isLife = false;
		}
		/*else if(tim2 <=0 && timi == true){
			tim2 = 0;
			timi = false;
		}*/
		Changerer ();
	}
	private int AttMats()
	{
		if (timer1 == false && textureChanged == false && isLife == true)//zmienia sie material powoli na brudne
			return 1;
		if (timi == true && isLife == true)//zmienia sie material kol
			return 2;
		if (isLife == false)//zmienia sie tekstura na zniszczony
			return 3;


		return 0;
	}
	private void Changerer ()
	{
		switch (AttMats ()) {
		case 1:
			FromCleanTextureToDustTexture();
			break;
		case 2:
			TireTexture ();
			break;
		case 3:
			DeathCar();
			break;
		
		default:
			break;
		}
	}
	private void FromCleanTextureToDustTexture() //czysta tex czysta opona
	{
		for (int i = 0; i < materialsInCar.Length; i++) {
			if(chang == false){
				//materialsInCar[i].GetComponent<Renderer>().material.SetTexture("_mainTexture", MakeTexture ((Texture2D)cleanCar, (Texture2D)textureToChangedCar));
				truckBody[i].material.SetFloat("_Blend", tim1/timeToChange);
				//Debug.Log("nastepuje podmiana tekstury wartosc: " +tim1/timeToChange);
			}
			if(chang == true)
			{
				truckBody[i].material.SetTexture("_mainTexture", textureToChangedCar) ;
				chang = false;
				textureChanged = true;
				//Debug.Log("tekstura glowna calkowicie podmieniona");
			}
			//Debug.Log("ustawiam nowa tekstrue do 3");
		}
	}
	private void TireTexture ()
	{
		for (int i = 0; i < materialsOfTires.Length; i++) {
			if(tim2<timeToChanching)
				tiresBody[i].material.SetFloat("_Blend", tim2/timeToChanching);
			//Debug.Log("ustawiam teksture oponie wartosc: "+tim2/timeToChanching);
		}
	}


	private void DeathCar()
	{
		
		for (int i = 0; i < materialsInCar.Length; i++) {
			truckBody[i].material.SetTexture("_MainTex", destroyedCarTexture);
			truckBody[i].material.SetTexture("_Texture2", destroyedCarTexture);
			//Debug.Log("ustawiam nowa tekstrue do death");
		}
	}


}
/*
 * Materialy:
 * 1 - Domyslny
 * 2 - zakurzone i czyste opony
 * 3 - czysty i brudne opony
 * 4 - zakurzony i brudne opony
 * 5 - Zniszczony samochod
 * 6 - Bardzo brudna karoseria brudne opony
 * 7 - Bardzo brudna karoseria czyste opony
 * */
