using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnowSplashOnScreenScript : MonoBehaviour {

	public Canvas canvasSnowSplash;
	public Image [] images = new Image[3]; 	//first for cleanSnow, second for full screen on the snow left side, three right side
	public Image [] helpImages = new Image[2]; //first left second right
	public GameObject[] wipers = new GameObject[2];	//first left wirpe, second right wiper
	private float actualTimeClean = 1;

	[HideInInspector]public bool inSnowM = false;
	[HideInInspector]public bool needToEscape = true;
	private bool snwOnScreen = false;
	private bool cleaningScreen = false;
	public Vector2 minMaxRotation;
	private bool upOrDown = true;
	public float mnoznik = 1;
	private float actualRotationZ = 0;
	// Use this for initialization
	void Start () {
		actualRotationZ = minMaxRotation.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (inSnowM == true) {
			if(snwOnScreen == false)
				snwOnScreen = true;
			if (snwOnScreen == true && needToEscape == true) {
				needToEscape = false;
				images [1].enabled = true;
				images [2].enabled = true;
				if (helpImages [0].enabled == false) {
					for (int i = 0; i < helpImages.Length; i++) {
						helpImages [i].enabled = true;
					}
				}
				//images [1].color = new Color (images [1].color.r, images [1].color.g, images [1].color.b, 255);
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad0) /*&& snwOnScreen == true && cleaningScreen == false*/) {
			cleaningScreen = true;
		}
		if (cleaningScreen == true) {
			/*if (actualTimeClean <= 1 && actualTimeClean > 0 && upOrDown == false) {
				actualTimeClean -= Time.deltaTime / 2;
			} else if (actualTimeClean <= 1 && actualTimeClean > 0 && upOrDown == true) {
				actualTimeClean += Time.deltaTime / 2;
			}
			else {
				snwOnScreen = false;
				actualTimeClean = 1;
				cleaningScreen = false;
			}*/
			CleaningScreen ();
		}
	}
	private void CleaningScreen ()
	{
		//images [1].color = new Color (images [1].color.r, images [1].color.g, images [1].color.b, actT);
		if (actualRotationZ > minMaxRotation.y && upOrDown == true) {		//gdy jest za duza wartosc
			upOrDown = false;
			images [1].enabled = false;
			images [2].enabled = false;
			actualRotationZ = minMaxRotation.y;
		} else if (actualRotationZ < minMaxRotation.x && upOrDown == false) {	//gdy jest za mala wartosc
			upOrDown = true;
			actualRotationZ = minMaxRotation.x;
			cleaningScreen = false;
			if (helpImages [0].enabled == true) {
				for (int i = 0; i < helpImages.Length; i++) {
					helpImages [i].enabled = false;
				}
			}
		} else {											//gdy wartosc jest odpowiednia
			if (upOrDown == true) {							//gdy wycieraczki zwiekszaja kąt
				actualRotationZ += Time.deltaTime * mnoznik;
			} else {										//gdy wycieraczki zmniejszają
				actualRotationZ -= Time.deltaTime * mnoznik;
			}
		}
		for (int i = 0; i < wipers.Length; i++) {
			wipers [i].transform.localRotation =  Quaternion.Euler(new Vector3 (wipers [i].transform.localRotation.x, wipers [i].transform.localRotation.y, actualRotationZ));
		}
	}
}
