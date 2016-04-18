using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionCityScript : MonoBehaviour {

	public int wpiszIloscTriggerow = 2; // Okresla ilosc triggerow Sets amount of triggers
	public GameObject[] trigger = new GameObject[1]; //tablica triggerow w ktora bd wpisywane kolejne 
	public Canvas message;
	public GameObject [] texts = new GameObject[1];
	[HideInInspector]public int i = 0; // ogolna zmienna pomocnicza pod triggery misji
	[HideInInspector]public int y = 0; // ogolna zmienna pomocniczya pod wiadomosci
	RCCCarControllerV2 rcc;
	private bool clearingBlackScreen = false;
	[HideInInspector]public bool firstOfSecondScript = false;
	PlayerHealth ph;
    AllianceCityScript acs;
    UseCameraScript ucs;
    MiniGunScript mgs;
	MissionCompleteCityScript mccs;
    private bool canDoIt = false;
	public Image engineWarning;
	public Text enemyToKills;
	AttendanceEnemy ae;
	private int allEnemys = 0;
	[HideInInspector]public int nowKill = 0;
	public int killToMC = 10;
	private List<bool> listOfObj = new List<bool> ();
	private bool changeMC = false; 
	private float timerToEnd = 0;
	private bool isOver = false;
	public Animation obiectWithMiniGun;
	public GameObject[] dustOfTank = new GameObject[2];
	private AnimationClip animClip;
	private bool isTimeToAnimationTank = false;
	private AudioClip tankClip;



	//Black screen
	private bool timeToBlack = false;
	private bool timeToWhite = false;
	private float timerScreenBlack = 0;
	public Image blackScreen;

	CursorLockMode cursorMode;
	[HideInInspector]public string keepTextFromMissionCity;

	void Awake ()
	{
		ph = GetComponentInChildren<PlayerHealth>();
		blackScreen.enabled = false;
		rcc = GetComponent<RCCCarControllerV2> ();
        acs = (AllianceCityScript)FindObjectOfType(typeof(AllianceCityScript)) as AllianceCityScript;
        mgs = (MiniGunScript)FindObjectOfType(typeof(MiniGunScript)) as MiniGunScript;
        ucs = GetComponentInChildren<UseCameraScript>();
		ae = (AttendanceEnemy)FindObjectOfType (typeof(AttendanceEnemy)) as AttendanceEnemy;
		mccs = (MissionCompleteCityScript)FindObjectOfType(typeof(MissionCompleteCityScript)) as MissionCompleteCityScript;
		tankClip = Resources.Load ("Prefabs/MiniGun/tankSound", typeof(AudioClip)) as AudioClip;
		engineWarning.enabled = false;
		obiectWithMiniGun.playAutomatically = false;
		enemyToKills.enabled = false;
		for (int i = 0; i < dustOfTank.Length; i++) {
			dustOfTank [i].SetActive (false);
		}
		GameObject.Find ("EngineHelp").SetActive (false);
		//goalMC.SetActive (false);
	}

	private void BlackScreen (bool white, bool black, float timer)
	{
		if(white == false && black == false)
		{
			if(blackScreen.enabled == false)
				blackScreen.enabled = true;
			timer += Time.deltaTime;
			if(timer>=1)
				black = true;
			else
			{
				float alpha = Mathf.Clamp(timer, 0, 255);
				blackScreen.color = new Color (blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
			}
		}
		if(black == true && white == false)
		{
			timer -= Time.deltaTime;
			if(timer<=0)
				white = true;
			else
			{
				float alpha = Mathf.Clamp(timer, 0, 255);
				blackScreen.color = new Color (blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
			}
		}
		if(white == true && black == true && blackScreen.enabled == true)
			blackScreen.enabled = false;

		timeToBlack = black;
		timeToWhite = white;
		timerScreenBlack = timer;
	}
	void Start () {
		message = message.GetComponent<Canvas> ();
		for(int z = 0; z==wpiszIloscTriggerow; z++) //petla for po tablicy
		{
			trigger[z] = GameObject.FindGameObjectWithTag("Trigger"); //wpisywanie do tablicy obiektow z gry
		}
		Messengery (y);
		Podmianka(i); // wywolanie metody podmianka


		//Debug.Log ("KillToMc wynosi: " + killToMC + " zas nowKill: " + nowKill + " allEnemys: "+allEnemys);
	}
	
	void Update()
	{
        if (canDoIt == false) {
			if (acs.allTake == true) {
				canDoIt = true;
				//Ładowanie niezbednych danych do masakry w city
				for (int u = 0; u < ae.mainEnemy.Count; u++) {
					if (ae.mainEnemy [u].isLife == true){
						allEnemys++;
					}
				}
				if (killToMC > allEnemys)
					killToMC = allEnemys;
			}

        }
        if(canDoIt == true && changeMC == false)
		{
			if(timeToWhite == false && timeToBlack == false)
				BlackScreen (timeToWhite, timeToBlack, timerScreenBlack);
            //Wyczekuje na wcisniecie klawisza F
            if (Input.GetKeyDown(KeyCode.F) && acs.playerInBase == true)
			{
                rcc.engineRunning = false;
                rcc.canControl = false;
				mgs.camGun.enabled = true;
				mgs.camGun.gameObject.GetComponent<AudioListener> ().enabled = true;
				for(int u = 0; u < ucs.camers.Length; u++)
                {
					if(ucs.camers [u].gameObject.GetComponent<AudioListener> ().enabled == true)
						ucs.camers [u].gameObject.GetComponent<AudioListener> ().enabled = false;
					if(ucs.camers[u].enabled == true)
                    	ucs.camers[u].enabled = false;

                }
				for (int i = 0; i < dustOfTank.Length; i++) {
					dustOfTank [i].SetActive (true);
				}
				ucs.blockCamera = true;
                mgs.isTimeToShoot = true;
				changeMC = true;
				isTimeToAnimationTank = true;
				obiectWithMiniGun.Play ();
				obiectWithMiniGun.gameObject.GetComponent<AudioSource> ().enabled = true;
				obiectWithMiniGun.gameObject.GetComponent<AudioSource> ().clip = tankClip;
				obiectWithMiniGun.gameObject.GetComponent<AudioSource> ().loop = true;
				obiectWithMiniGun.gameObject.GetComponent<AudioSource> ().Play ();
				mgs.camGun.gameObject.GetComponent<AudioListener> ().enabled = true;
				DisableAllCanvass ();

            }
			ae.isShooterNow = true;
            
        }

		if(changeMC == true)
		{
			if(timeToWhite == false && timeToBlack == true)
				BlackScreen (timeToWhite, timeToBlack, timerScreenBlack);
			if(timeToWhite == false)
				BlackScreen (timeToWhite, timeToBlack, timerScreenBlack);
			int suma = killToMC - nowKill;
			if(enemyToKills.enabled == false && suma > 0){
				enemyToKills.enabled = true;
				if (Cursor.visible == true) {
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Confined;
				}
			}
			else if(suma == 0 && enemyToKills.enabled == true && isOver == false){
				if (Cursor.visible == false) {
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.Confined;
				}
				isOver = true;
				enemyToKills.enabled = false;
			}
			if (suma > 0 && enemyToKills.enabled == true)
				enemyToKills.text = (suma.ToString () + " " + keepTextFromMissionCity);

			if (obiectWithMiniGun.isPlaying == false) {	//Jeślli animacja sie skonczy to co ma zrobic
				if (Cursor.visible == false) {
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
				}
				Debug.Log ("GameOver");
			}

		}
		if (y == 0) {
			Messengery (y);
		}
		if (isOver == true) {
			timerToEnd += Time.deltaTime;
			if(timerToEnd > 3)
				mccs.EnabledMissionComplete ();
		}
		DisEnbl ();

	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) //wykrywanie kolizji
	{
		if (other.tag == "Trigger") //sprawdzaj czy kolizja dotyczy obiektow o tagu Trigger
		{
			if (Zadania(i) == true)
			{
				i++; //zwieksz wartosc pomocnicza za kazdym razem gdy obiekt bedzie mial kontakt z triggerem
				Podmianka(i);//wywolanie metody podmianka i przeslanie wartosci i do metody
			}
		}
	}
	void Podmianka(int i) //metoda Podmianka
	{
		for (int z = 0; z < wpiszIloscTriggerow; z++) // jedz po elementach tablicy
		{
			if (i == z) //jesli wartosc zmiennej wyslanej z metody jest rowna wartosci zmiennej petli to:
				trigger[z].SetActive(true); //wlaczenie danego obiektu
			else
				trigger[z].SetActive(false);//wylaczenie danego obiektu
		}
	}
	
	void Messengery (int y)// funkcja w zaleznosci od wartosci indexu y wlacza msg lub go wylacza
	{
		for (int z=0; z<texts.Length; z++) {
			
			if (z == y)
			{
				Time.timeScale = 0; 	// Jeżeli gracz otrzymuje komunikat to gra się zatrzymuje. Po wciśnięciu
				texts[z].SetActive(true);//buttonu close gra wraca do standardowej prędkości.
				//Cursor.visible = true;
				
			}
			else
			{
				texts[z].SetActive(false);
			}
		}
	}
	void DisEnbl ()
	{
		if (Input.GetKeyUp (KeyCode.C)) {
			foreach (GameObject mess in texts) {
				if (mess.activeInHierarchy == true) {
				
					mess.SetActive (false);
					Time.timeScale = 1;
					y++;
				}
			}
		}
	}
	
	bool Zadania (int i) // funkcja odpowiedzialna za zapętlenie zadan w grze
	{
		switch (i) //case 0: - pierwszy prefab
		{
		case 1:
			Messengery (y);
			return true;
			break;
		case 2:
			if (canDoIt == true) {
				return true;
			}
			break;
		default:
			return true;
			break;
		}
		return false;
	}
	private void DisableAllCanvass ()
	{
		engineWarning.enabled = false;
		GameObject.Find("KMH").SetActive(false);
		GameObject.Find("RPM").SetActive(false);
		GameObject.Find("DirtyMirror").SetActive(false);
		GameObject.Find ("Group1").SetActive (false);
		GameObject.Find ("Group2").SetActive (false);
		GameObject.Find ("Group3").SetActive (false);
		GameObject.Find ("CarSpace").SetActive (false);
	}
}

/* Działanie skryptu
 * Skrypt pobiera obiekty odpowiedzialne misje. Podpiecie tego skryptu do playera umozliwia wrzucenie do tablic
 * kolejno następujących po sobie etapów misji, oraz wyświetlenie informacji zależnie od etapu w którym znajduje się gracz.
 * Do tablicy Trigger wrzucamy kolejno następujące po sobie etapy misji.
 * Uwaga triggery misyjne muszą mieć tag Trigger w przeciwnym razie skrypt nie bedzie ich uwzględniał.
 * Zmienna odpowiedzialna za "etapowanie" jest zmienna i.
 * Wszelkie istotne zmiany oraz dokładne warunki poszczególnych misji jak również wyświetlania informacji zapisujemy wewnątrz 
 * instrukcji warunkowej switch zawartej w funkcji zadania.
 * Do poprawnego działania niezbędne jest podpięcie Canvasa głównego wyświetlającego kolejne wiadomości oraz poszczególne 
 * wiadomosci "text" jako oddzielne obiekty. Wszelkie prawa zastrzeżone. All rights reserved.
 * */
 