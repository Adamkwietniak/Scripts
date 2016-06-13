using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldiersRBScript : MonoBehaviour {

	private Rigidbody [] rb = new Rigidbody[13];
	private Animator anim;
	private float timer;
	public Canvas gameOver;
	public AudioSource soundSource;
	public AudioClip clickSound;
	private bool timerrek = false;
	public AudioSource soldierSource;
	public AudioClip deadSoldierClip;
    MenuScript ms;

	private bool isKinematic = false;

    void Awake ()
    {
        ms = (MenuScript)FindObjectOfType(typeof(MenuScript)) as MenuScript;
    }
	// Use this for initialization
	void Start () {

		rb = GetComponentsInChildren<Rigidbody> ();
		anim = GetComponent<Animator>();
		//gameOver = GetComponent<Canvas> ();
	
	}
	
	void OnTriggerEnter (Collider other) {

		if (other.tag == "Player") {
			isKinematic = true;
			}
	}



	void Update () {

		if (isKinematic == true) {
			SetKinematic ();
		}
		else if(timerrek == true){
				timer+=Time.deltaTime;
			//Debug.Log("timerek wynosi: " + timer);
			}
		if (timer>=3f){
			gameOver.enabled = true;
			Time.timeScale = 0;
			timer = 0f;
			timerrek = false;
			}
		}
	private void SetKinematic (){
		
		for (int i=0; i < rb.Length; i++) {
			rb [i].isKinematic = false;
		}
		//Debug.Log("dziala setKinematic");
		anim.enabled = false;
		isKinematic = false;
		timerrek = true;
		soldierSource.PlayOneShot (deadSoldierClip);
        ms.escUse = false;
	}

}


