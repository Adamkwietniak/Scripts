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
	public Vector3 playerPosition;
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
	private float [] speedMaxTab = new float[5]; 
	private int indexOfObstacleTag = 1000;
	int [] tabUszk = new int[14];


	int obrazenia = 0;
    int predkosc = 0;
	int uszkodzenia = 0;
	int blendShapeCount; //ilosc colliderow animacyjnych
	private Transform trans;
	ObstacleTagScript ots;
	RCCCarControllerV2 brum;
	
	void Awake ()
	{	//Pobieranie komponentów
		brum = GetComponentInParent<RCCCarControllerV2>();
		skinned = GetComponent<SkinnedMeshRenderer> ();
		srodek = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		
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
		playerPosition = this.trans.position;
		if (ifdamage == false) {
			for (int i = 0; i < 5; i++) {
				speedMaxTab [i] = predkosc;
				//Debug.Log("elementy tablidy predkosci to " + speedMaxTab[i]);
			}
		}
		if(currentHealth <=0)	//Co się stanie jak samochód ma 0 życia?
		{						//nie będzie działać xD
			brum.maxspeed = 0;
			currentHealth = 0;
			brum.engineRunning = false;
			timer+=Time.deltaTime;
			if(timer>=0.8f){

		
				gameOver.enabled = true;


			}
			if(gameOver.enabled==true){
				timer=0f;
				Time.timeScale = 0;
			}


		} 
	
		if (ifdamage == true) {					//Jeśli nastąpiły uszkodzenia, sprawd jakie a następnie odpowiednio się
									//ustosunkuj w zależności od tego, jakie one były a następnie przeslij
			timer += Time.deltaTime*2;
			if(timer > 0.07 && checkStayInCollider == true){
				AssignSpeedExit ();
				checkStayInCollider = false;
				timer = 0;}					//wyliczoną wartość do metody CarDMG, jak również podstawia nową wartość
			if(CheckObstacle())
				obrazenia = (int)((SetDMG() + (((ots.obstacleTab[indexOfObstacleTag].angularV + ots.obstacleTab[indexOfObstacleTag].speedD)*ots.obstacleTab[indexOfObstacleTag].rb.mass)/300)/**(predkosc/10)*/ )/8);
			else
				obrazenia = (int)((SetDMG()/**(predkosc/10)*/ )/7f);	//max.speed
			if(obrazenia < 0)
				obrazenia = obrazenia  * (-1);
			if(brum.maxspeed>90)
				brum.maxspeed = (brum.maxspeed * (int)currentHealth) / (int)startingHealth;
			else if(brum.maxspeed <= 89 && brum.maxspeed > 60)
				brum.maxspeed -= obrazenia;
			else
				brum.maxspeed -=obrazenia/2;
        
			CarDMG (obrazenia); 
			ResetTempTab ();
		} 
		ifdamage = false;
		checkStayInCollider = false;
		if(sthOnScreen == true)
			BlinkEngineWarning ();

		if (Input.GetKeyDown(KeyCode.CapsLock)){
			currentHealth = 1;
		}

		if (Input.GetKey (KeyCode.H) && soundSource.isPlaying == false) {
			soundSource.PlayOneShot (hornSound);
		}
	}
	public void RepairCar ()
	{
		brum.speed = maximusSpeedus;
		engineWarning.enabled = false;
		currentHealth = startingHealth;
		for(int i = 0; i < tabUszk.Length;i++)
		{
			tabUszk[i] = 0;
		}
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
	private void AssignSpeedEnter ()
	{
		speedTab [0] = SortMaxTab ();
		//Debug.Log ("Wypelniam tablice poczatkowa wartoscia " + speedTab [0]);
	}
	private void AssignSpeedExit ()
	{
		speedTab [1] = predkosc;
		//Debug.Log ("Wypelniam tablice koncowa wartoscia " + speedTab [1]);
	}
	private void ResetTempTab ()
	{
		for (int i = 0; i<speedTab.Length; i++) {
			speedTab [i] = 0;
		}
	}
	private float SetDMG()
	{
		return speedTab [0] - speedTab [1];
	}
	private float SortMaxTab ()
	{
		float temp = 0;
		for (int i = 0; i < speedMaxTab.Length; i++) {
			if(speedMaxTab[i] > temp)
				temp = speedMaxTab[i];
			else
				temp = temp;

		}
		return temp;
	}
	private float SortMinTab ()
	{
		float temp = 300;
		for (int i = 0; i < speedMaxTab.Length; i++) {
			if(speedMaxTab[i] < temp)
				temp = speedMaxTab[i];
			else
				temp = temp;
			
		}
		return temp;
	}

	public void QuitGame (){
		
		Application.Quit ();

		
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
}	
/*
 * Działanie skryptu:
 * Skrypt ten odpowiada za obliczanie ilości życia po uderzeniach, obliczanie wartości uszkodzeń i przypisywanie
 * uszkodzeń konkretnym elementom naszego playera.
 * Do tablicy Coli przenosimy elementy colliderów (musza być game objecty).
 * */
