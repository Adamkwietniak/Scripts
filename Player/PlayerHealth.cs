using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;


public class PlayerHealth : MonoBehaviour 
{
	SkinnedMeshRenderer skinned; //poprostu skinnedMeshRenderer wywolujemy przez skinned.
	Mesh srodek;

	public int startingHealth = 100; // ilosc zycia                          
	public int currentHealth; //ilosc zycia w danym momencie 
	[HideInInspector]public float dmg = 0f;
	[HideInInspector]public float timer;
	[HideInInspector]public string nameCollider;
	[HideInInspector]public bool ifdamage = false;
	[HideInInspector]public bool checkStayInCollider = false;
	public Image engineWarning;
	private bool sthOnScreen = false;
	private bool upColor = false;
	private float varOfAlpha = 0f;
	private Color engineWarningColor = new Color (0.58f, 0f, 0f, 0f);
	private float blinkingSpeed = 3f;
	private int maximusSpeedus = 0;
	public AudioClip hornSound;


	public Canvas gameOver;
	public Button quitBtn;
	public Button tryAgainBtn;
	public string sameLevel;

	public AudioSource soundSource;
	public AudioClip clickSound;
	
	public GameObject [] coli = new GameObject [14];
	protected string [] coliName = new string[14];
	private float [] speedTab = new float[2];
	private float [] speedMaxTab = new float[10];
	private int speedMaxIndex = 0;
	private int indexOfObstacleTag = 1000;
	int [] tabUszk = new int[14];
	public float mnoznikDoSpeed = 1;

	int obrazenia = 0;
    int predkosc = 0;
	int uszkodzenia = 0;
	int blendShapeCount; //ilosc colliderow animacyjnych
	private Transform trans;
	ObstacleTagScript ots;
	RCCCarControllerV2 brum;
	MenuScript mns;
	
	void Awake ()
	{	//Pobieranie komponentów
		brum = GetComponentInParent<RCCCarControllerV2>();
		skinned = GetComponent<SkinnedMeshRenderer> ();
		srodek = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		speedMaxTab [speedMaxIndex] = 0;
		// Przypisanie początkowego życia to życia gracza
		currentHealth = startingHealth;
	}

	void Start () 
	{
		engineWarning.enabled = false;
		//Przypisanie ilości blendShape do ilosci odpowiedzialnej za iterowanie operacji
		trans = this.GetComponent <Transform> ();
		ots = GetComponentInParent<ObstacleTagScript> ();
		blendShapeCount = srodek.blendShapeCount;
		mns = GameObject.Find ("GoodCanvas").GetComponentInChildren<MenuScript> ();
		for (int i=0; i<blendShapeCount;i++) //Przypisanie wszystkim shape wartosci 0
		
        {
			coliName[i] = coli[i].name; //Przypisanie nazw koliderów do pomocniczej tablicy nazw
			tabUszk[i] = 0;				//Przypisanie do pomocniczej tablicy uszkodzeń wartości 0 (nieoptymalne,
            skinned.SetBlendShapeWeight(i, 0f); // ale daje poczycie bezpieczeństwa, że nie wpisza się randomowe liczby).
        }//Przypisanie wartosci 0 do poszczególnych zmiennych odpowiedzialnych za wgniecenia samochodu.

		maximusSpeedus = (int)brum.maxspeed;
	
    }

	void Update ()
	{
		predkosc = (int)brum.speed;
		if (speedMaxIndex < speedMaxTab.Length - 1 ) {
			speedMaxIndex++;
			speedMaxTab [speedMaxIndex] = predkosc;
		} else {
			speedMaxIndex = 0;
			speedMaxTab [speedMaxIndex] = predkosc;
		}
		if(currentHealth <=0)	//Co się stanie jak samochód ma 0 życia?
		{						//nie będzie działać xD
			GameOver ();
		} 
	
		if (ifdamage == true) {					//Jeśli nastąpiły uszkodzenia, sprawd jakie a następnie odpowiednio się
			//ustosunkuj w zależności od tego, jakie one były a następnie przeslij
			int deltaPredkosc = (int)(SetDMG (speedMaxTab));

			if (CheckObstacle ()) {
				obrazenia = (int)((deltaPredkosc + (((ots.obstacleTab [indexOfObstacleTag].angularV + ots.obstacleTab [indexOfObstacleTag].speedD) * ots.obstacleTab [indexOfObstacleTag].rb.mass) / (mnoznikDoSpeed * 32))/**(predkosc/10)*/) / (mnoznikDoSpeed * 8));
				//Debug.Log ("Predkosc katowa: " + ots.obstacleTab [indexOfObstacleTag].angularV + " predskosc zwykla: " + ots.obstacleTab [indexOfObstacleTag].speedD + " masa: " + ots.obstacleTab [indexOfObstacleTag].rb.mass + " wyszlo: " + obrazenia);
			}
			else
			{
				if(deltaPredkosc > 10){
					obrazenia = (int)(deltaPredkosc / (mnoznikDoSpeed * 7f));	//max.speed
					ResetTab ();
				}
			}
			if (obrazenia < 0)
				obrazenia = obrazenia * (-1);
			if (brum.maxspeed > 90) {
				brum.maxspeed -= obrazenia*(mnoznikDoSpeed * 2f);
				//Debug.Log (obrazenia);
			} else if (brum.maxspeed <= 89 && brum.maxspeed > 60)
				brum.maxspeed -= obrazenia*(mnoznikDoSpeed * 1.25f);
			else
				brum.maxspeed -= obrazenia * (mnoznikDoSpeed*0.3f);
        
			CarDMG (obrazenia); 
			//Debug.Log (obrazenia);
		}
		ifdamage = false;
		checkStayInCollider = false;
		if(sthOnScreen == true)
			BlinkEngineWarning ();
		if (Input.GetKey (KeyCode.H) && soundSource.isPlaying == false) {
			soundSource.PlayOneShot (hornSound);
		}
	}
	public void RepairCar ()
	{
		brum.maxspeed = maximusSpeedus;
		engineWarning.enabled = false;
		currentHealth = startingHealth;
		JointScript.czyNaprawione = true;
		for(int i = 0; i < tabUszk.Length;i++)
		{
			tabUszk[i] = 0;
		}
	}
	private float [] CheckGoodDmg (float [] tab)
	{
		float tempPlus = 0;
		float tempMinus = 0;
		float [] tempTab= new float[2];
		for (int i = 0; i < tab.Length; i++) {
			if (tab [i] > tempPlus)
				tempPlus = tab [i];
			if (tab [i] < tempMinus)
				tempMinus = tab [i];
		}
		tempTab [0] = tempPlus;
		tempTab [1] = tempMinus;
		return tempTab;
	}
	private void BlinkEngineWarning (){   // metoda od pokazywania się na ekranie znaczka od silnika "WArning".

			engineWarning.enabled = true;
			if(varOfAlpha<=1 && upColor == true ) {
				varOfAlpha+=Time.deltaTime*blinkingSpeed;
				if(varOfAlpha >= 1)
				{
					upColor = false;
					varOfAlpha = 1;
				}
			}
			if(varOfAlpha>=0 && upColor == false){
				varOfAlpha-=Time.deltaTime*blinkingSpeed;
				if(varOfAlpha <= 0)
				{
					upColor = true;
					varOfAlpha = 0;
				}
			}
			engineWarning.color = new Color (0.58f, 0f, 0f, varOfAlpha);
	
	}

	public void CarDMG (int obrazenia)					//Metoda, która odpowiada za wyliczenie obrażeń oraz podstawienie
	{													//ich do odpowiedniej kolumny z blendShape samochodu (truck_low2)
		if (currentHealth > 30)
			currentHealth -= obrazenia;
		else
			currentHealth -= (int)obrazenia/2;
        uszkodzenia = obrazenia*5;

		if (currentHealth < 30)
			sthOnScreen = true;

		for (int i=0; i < blendShapeCount; i++)
        {
            if(coliName[i] == nameCollider)
            {
                skinned.SetBlendShapeWeight(i, tabUszk[i] += uszkodzenia);	//W tym miejscu następuje podstawienie wartości
            }																// uszkodzeń wizualnych w grze
        }
	}
	private bool CheckObstacle ()
	{
		if (brum.speed < 15) {
			for (int i = 0; i < ots.ss; i++) {
				if (ots.obstacleTab [i].angularV != 0 || ots.obstacleTab [i].speedD != 0 && nameCollider == ots.obstacleTab [i].gmob.name) {
					indexOfObstacleTag = i;
					return true;
				}
			}
		}
		return false;
	}
	private float SetDMG (float[] tab)
	{
		float[] temp = CheckGoodDmg (tab);
		return temp [0] - temp [1];
	}
	private void ResetTab ()
	{
		for (int i = 0; i < speedMaxTab.Length; i++) {
			speedMaxTab[i] = 0;
		}
		speedMaxIndex = 0;
	}

	public void QuitGame (){
		
		Application.LoadLevel ("SceneCanvas");
		if (mns.menuUI.enabled == false) 
		{
			mns.menuUI.enabled = true;
		}

		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}

	public void TryAgain (){
		
		gameOver.enabled = false;
		Application.LoadLevel(sameLevel);


		
		if (soundSource != null)
		{
			soundSource.PlayOneShot(clickSound);
		}
	}
	public void GameOver ()
	{
		brum.maxspeed = 0;
		//currentHealth = 0;
		brum.engineRunning = false;
		timer+=Time.deltaTime;
		if(timer>=1.5f){


			gameOver.enabled = true;


		}
		if(gameOver.enabled==true){
			timer=0f;
			Time.timeScale = 0;
		}

	}
}	
/*
 * Działanie skryptu:
 * Skrypt ten odpowiada za obliczanie ilości życia po uderzeniach, obliczanie wartości uszkodzeń i przypisywanie
 * uszkodzeń konkretnym elementom naszego playera.
 * Do tablicy Coli przenosimy elementy colliderów (musza być game objecty).
 * */
