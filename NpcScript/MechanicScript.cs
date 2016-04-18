using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MechanicScript : MonoBehaviour {

	PlayerHealth ph;
	RCCCarControllerV2 rcc;
	public Text mechanicAdvice;
	public Image blackScreen;
	public float fadeSpeed = 1.5f;
	public bool blackScreenActive = false;
	public bool clearingBlackScreen = false;
	public bool blackScreenDone = false;
	public float timerDelay;



	void Start ()
	{
		mechanicAdvice.enabled = false;
		ph = GetComponentInChildren<PlayerHealth> ();
		rcc = GetComponent<RCCCarControllerV2> ();

	}

	void OnTriggerEnter (Collider other)
	{

		if (other.tag == "Player") {
			
			mechanicAdvice.enabled = true;
			blackScreen.enabled = true;

			if(blackScreen == true) 						
				{
					if (blackScreen.enabled == false) {
						blackScreen.enabled = true;
					blackScreenActive = true;

					}
				blackScreen.color = new Color(0,0,0,Mathf.Clamp(timerDelay, 0, 255));
				if(blackScreenActive == true && clearingBlackScreen == false)
				{
					if(timerDelay<1)
						timerDelay += Time.deltaTime/3;
					else 
					{
						timerDelay = 1;
						ph.RepairCar ();
						blackScreenActive = false;
						clearingBlackScreen = true;



					}
				}

				if(clearingBlackScreen == true && blackScreenActive == false)
				{
					if(timerDelay>0)
						timerDelay -= Time.deltaTime/3;
					else 
					{
						timerDelay = 0;
						clearingBlackScreen = false;
						blackScreen.enabled = false;
						blackScreenDone = true;
					}
				}
			}
		}
	}
}
		
