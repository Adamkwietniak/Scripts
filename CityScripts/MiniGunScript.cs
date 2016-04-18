using UnityEngine;
using System.Collections;

public class MiniGunScript : MonoBehaviour {

	//BrumBrume
	private GameObject brumBrume;
	private Light [] brumLights;
	UseCameraScript ucs;
	RCCCarControllerV2 rcc;
	MissionCityScript mcs;

	//MachineGun
	string shtPath = "Prefabs/MiniGun/";
	public Transform childRoot;
	private Animator animOfGun;
	public Transform barrel;
	public GameObject shootingParticle;
	public GameObject bulletParticle;
	public GameObject [] smokeParticles;
	private bool checksmPart = false;
	private ParticleSystem ps;

	private GameObject [] bloodHit = new GameObject[2];
	private GameObject groundHit;
	private GameObject diffHit;


	//Shooting
	[HideInInspector]public bool isClick = false;
	[HideInInspector]public bool isShooting = false;
	[HideInInspector]public float timerToShoot = 0;


	//Ogolne
	private AudioSource audio;
	LayerMask mask = 0;
	private AudioClip shooting;
	private AudioClip plyUnshooting;


	//Camera
	[HideInInspector]public Camera camGun;
	public Transform ray;
	private float oX = 0; // os X
	private float oY = 0; // os Y
	public Vector2 rotationRange = new Vector3(70, 70);
	public float rotationSpeed = 5.0f;
	private Vector3 m_TargetAngles;
	private Vector3 m_FollowAngles;
	private Vector3 m_FollowVelocity;
	private Quaternion m_OriginalRotation;
	public bool relative = true;
	private float dampingTime = 0.2f;
	private Vector3 posContactRay;
	private Vector3 rotContactRay;


	AttendanceEnemy ae;
    [HideInInspector]public bool isTimeToShoot = false;
    public float powerOfShoot = 100;
	void Awake ()
	{
		//BrumBrume
		brumBrume = GameObject.Find("BrumBrume");
		ae = FindObjectOfType (typeof(AttendanceEnemy)) as AttendanceEnemy;
		brumLights = brumBrume.GetComponentsInChildren<Light>();
		rcc = brumBrume.GetComponent<RCCCarControllerV2>();
		ucs = brumBrume.GetComponent<UseCameraScript>();
		mcs = brumBrume.GetComponent<MissionCityScript>();

		camGun = GetComponentInChildren<Camera>();
		//camGunTr = camGun.GetComponent<Transform>();
		//MachineGun
		shooting = Resources.Load((shtPath+"shoot"), typeof (AudioClip)) as AudioClip;
		plyUnshooting = Resources.Load((shtPath+"UnShoot"), typeof (AudioClip)) as AudioClip;
		bloodHit[0] = Resources.Load ((shtPath + "blood1"), typeof(GameObject)) as GameObject;
		bloodHit[1] = Resources.Load ((shtPath + "blood2"), typeof(GameObject)) as GameObject;
		groundHit = Resources.Load ((shtPath + "HitFloor"), typeof(GameObject)) as GameObject;
		diffHit = Resources.Load ((shtPath + "DifferentHit"), typeof(GameObject)) as GameObject;
		animOfGun = this.GetComponent<Animator>();

		ps = smokeParticles [0].GetComponent<ParticleSystem> ();
		m_OriginalRotation = childRoot.localRotation;
		bulletParticle.SetActive (false);
	}
	void Start ()
	{
		camGun.enabled = false;
	}
	void Update () {
        if (isTimeToShoot == true)
        {
			if(camGun.enabled == false)
				camGun.enabled = true;
			
            if (Input.GetMouseButton(0) && isClick == false)
            {
                isClick = true;
            }
            if (Input.GetMouseButtonUp(0) && isClick == true)
            {
                isClick = false;
                timerToShoot = 0;
            }
            if (isClick == true && isShooting == false)
            {
                if (timerToShoot < 1)
                    timerToShoot += Time.deltaTime * 2;
                if (timerToShoot > 1)
                {
                    timerToShoot = 1;
                    isShooting = true;
                }
              //  MusicIs(audio.isPlaying, plyUnshooting);
                animOfGun.SetBool("isClick", true);
                animOfGun.SetBool("isShooting", false);
            }
            if (isClick == true && isShooting == true)
            {
                timerToShoot = 0;
             //   ae.miniGunIsShoot = true;
                animOfGun.SetBool("isClick", true);
                animOfGun.SetBool("isShooting", true);
				bulletParticle.SetActive (true);
				shootingParticle.SetActive (true);
             //   MusicIs(audio.isPlaying, shooting);
            }
            if (isClick == false && isShooting == true)
            {
                timerToShoot = 0;
                animOfGun.SetBool("isClick", false);
                animOfGun.SetBool("isShooting", true);
                isShooting = false;
				bulletParticle.SetActive (false);
				shootingParticle.SetActive (false);
				for (int i = 0; i < smokeParticles.Length; i++) {
					smokeParticles [i].SetActive (true);
				}

				if (checksmPart == false)
					checksmPart = true;
               // MusicIs(audio.isPlaying, plyUnshooting);
            }
			if(checksmPart == true && ps.isPlaying == false)
			{
				for(int i = 0; i < smokeParticles.Length; i++)
				{
					smokeParticles[i].SetActive(false);
				}
			}
            if (isClick == true)
            {
                switch (CheckRay())
                {
				case 0:
					
                        Debug.Log("Trafiasz w gowno jakies");
                        break;
				case 1:
						
                        Debug.Log("Trafiasz we wroga");
                        break;
                    case 2:
						
                        Debug.Log("Trafiasz w sojusznika");
                        break;
                    case 3:
						
                        Debug.Log("Trafiasz w teren");
                        break;

                    default:
                        break;
                }
            }
            oX = Input.GetAxis("Mouse X");
            oY = Input.GetAxis("Mouse Y");
            if (relative)
                CheckAngulars();
            else
                WithOutRelative();

            m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);

            // update the actual gameobject's rotation
            childRoot.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
        }
	}

	private int CheckRay ()
	{
		//Ray ray;// = camGun.ScreenPointToRay(Input.mousePosition);
		Ray rey = new Ray(ray.position, ray.forward);
		RaycastHit hit;
		LayerMask mask = 0;
		Debug.DrawRay(ray.position, ray.forward, Color.red);
		if(Physics.Raycast(rey, out hit, 250))
		{
			if (isShooting == true) {
				rotContactRay = hit.normal;
				posContactRay = hit.point;
				if (hit.collider.tag == "enemy") {		//Trafiasz we wroga
					mcs.nowKill = ae.EnemyIsShooted (hit.collider.transform.root.gameObject.name, hit.collider.name, rotContactRay, powerOfShoot, mcs.nowKill);
					int z = Random.Range (0, 10) % 2;
					InstaPref (bloodHit [z], posContactRay, Quaternion.Euler (rotContactRay));
					//Debug.Log("CelMaNazwe: "+ hit.collider.transform.root.gameObject.name);
					return 1; // trafiasz we wroga
				} else if (hit.collider.tag == "alliance") {	//Trafiasz w sojusznika 
					int h = Random.Range (0, 10) % 2;
					InstaPref (bloodHit [h], posContactRay, Quaternion.Euler (rotContactRay));
					//	ae.targetOfMinigun = hit.collider.transform.root.gameObject.name;
					return 2; //trafiasz w sojusznika lub zwyklego npc
				} else if (hit.collider.tag == "Teren") {		//Trafiasz w teren
					InstaPref (groundHit, posContactRay, Quaternion.Euler (new Vector3 (0, 1, 0)));
					//Debug.Log("Trafiasz we gowno");
					return 3; // trafiasz w Teren
				} else { //Trafiasz w inne obiekty
					InstaPref (diffHit, posContactRay, Quaternion.Euler (rotContactRay));
					return 0;
				}
			}

		}
		return 0;
	}
	private void MusicIs (bool plays, AudioClip clip)
	{
		if (audio.clip.name != clip.name)
			audio.clip = clip;
		if (plays == false) {
			audio.Play ();
		}
	}
	private void InstaPref(GameObject pref, Vector3 pos, Quaternion rot)
	{
		Instantiate (pref, pos, rot);
	}
	private void CheckAngulars ()
	{
		
			// wrap values to avoid springing quickly the wrong way from positive to negative
		if (m_TargetAngles.y > 180)
		{
			m_TargetAngles.y -= 360;
			m_FollowAngles.y -= 360;
		}
		if (m_TargetAngles.x > 180)
		{
			m_TargetAngles.x -= 360;
			m_FollowAngles.x -= 360;
		}
		if (m_TargetAngles.y < -180)
		{
			m_TargetAngles.y += 360;
			m_FollowAngles.y += 360;
		}
		if (m_TargetAngles.x < -180)
		{
			m_TargetAngles.x += 360;
			m_FollowAngles.x += 360;
		}
		m_TargetAngles.y += oY*rotationSpeed;
        m_TargetAngles.x += oX*rotationSpeed;


         // clamp values to allowed range
         m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y*0.5f, rotationRange.y*0.5f);
         m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x*0.5f, rotationRange.x*0.5f);
	}
	private void WithOutRelative ()
	{
			oX = Input.mousePosition.x;
			oY = Input.mousePosition.y;

			// set values to allowed range
			m_TargetAngles.y = Mathf.Lerp(-rotationRange.y*0.5f, rotationRange.y*0.5f, oY/Screen.width);
			m_TargetAngles.x = Mathf.Lerp(-rotationRange.x*0.5f, rotationRange.x*0.5f, oX/Screen.height);

	}
}
