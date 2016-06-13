using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AllianceSoliderEvent : MonoBehaviour {
	private Animator anim;
	private NavMeshAgent agent;
	private Rigidbody [] rb = new Rigidbody[13];
	public Canvas gameOver;
	public bool wantTableToAnim = false;
	public Transform [] destPoints = new Transform[1];
	private Transform trans;
	private bool allianceDead = false;
	private bool timerrek = false;
	private float timer = 0;
	private float dist;
	private int n;
	private GameObject player;
	private Transform playerTr;
	//private float distance;
	private bool isWalk = true;
	public AudioSource soundSource;
	public AudioClip clickSound;
	public AudioSource soldierSource;
	public AudioClip deadSoldierClip;
    MenuScript ms;

    void Awake ()
    {
        ms = (MenuScript)FindObjectOfType(typeof(MenuScript)) as MenuScript;
    }
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("BrumBrume");
		playerTr = player.GetComponent<Transform>();
		allianceDead = false;
		anim = GetComponent<Animator> ();
		anim.enabled = true;

		agent = GetComponent<NavMeshAgent> ();
		trans = GetComponent<Transform> ();
		rb = GetComponentsInChildren<Rigidbody> ();
		for (int i = 0; i < destPoints.Length; i++) {
			destPoints[i] = destPoints[i].GetComponent<Transform>();
		}
		anim.SetBool("isWalk", true);
		n = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (allianceDead == true) { //Ten if dziala wtedy kiedy pieprznie go brum
			SetKinematic ();
			allianceDead = false;
            ms.escUse = false;
		}
		if (allianceDead == false && wantTableToAnim == true) {
			dist = Vector3.Distance(this.trans.position, destPoints[n].position);
			//Debug.Log("Dist wynosi: "+dist+" "+n);
			if(dist <= 1.0f && n < destPoints.Length-1){
				n=n+1;
			}
			else if(destPoints.Length-1 >= n && dist <= 1.0f)
			{
				//Debug.Log("fffff");
				n = 0;
			}
			this.agent.SetDestination(destPoints[n].position);
		}
		if (timerrek == true){
			timer+=Time.deltaTime;
		}
		if (timer > 5.0f && timerrek == true) {
			timerrek=false;
			AttendanceGameOver ();
		}
		if(CountDistance () < 7 && isWalk == true)
		{
			isWalk = false;
			//Debug.Log("licze");
			this.anim.SetBool("isWalk", false);
			this.agent.speed = 0f;
		}
		else if(CountDistance () >= 7 && isWalk == false)
		{
			isWalk = true;
			this.anim.SetBool("isWalk", true);
			this.agent.speed = 0.05f;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			allianceDead = true;
		}
	}
	private void SetKinematic ()
	{
		for (int i = 0; i < rb.Length; i++) {
			rb[i].isKinematic = false;
		}
		anim.enabled = false;
		isWalk = false;
		agent.Stop ();
		timerrek = true;
		soldierSource.PlayOneShot (deadSoldierClip);
	}
	private void AttendanceGameOver (){
		gameOver.enabled = true;
		timer = 0;

		if (gameOver.enabled == true) {
			Time.timeScale = 0f;
		}
		timerrek = false;
	}
	private float CountDistance ()
	{
		return Vector3.Distance(playerTr.position, trans.position);
	}
}
