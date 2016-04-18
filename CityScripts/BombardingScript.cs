using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class BombardingScript : MonoBehaviour {

	public GameObject bomb;     //prefab of bomb gameobj
	public GameObject partSys;  //prefab of explosion
	public Terrain terrain;     //terrain
    public GameObject proje;
    public int offsetToUp = 50; //number of offset between make bomb and ground
	public float offsetOfDetectionCollision = 0.05f;
    //private TerrainData terrainData;
	private Transform brumTr;
	private float maxTimer = 1f;
	public Vector2 maxMin = new Vector2(50,50);         //range of offset count random place to drop a bomb
    public bool bombInScene = true;   					//temporarity bool to blocks the program
    private float timerToMakeBomb = 0;                  //temporarity float to count time to next bomb
    private int hzToNextBomb = 1;                      //max value for timerToMakeBomb
    private List<Bomber> bombs = new List<Bomber>();    //List of bombs
    public AudioClip dropTheBombClip;
    public AudioClip explodeClip;

	//To Draw Circle
	private float thetaScale = 0.01f;
	private int size = 14;
	private float radius = 8.0f;

	PlayerHealth ph;

	void Awake ()
	{
		brumTr = this.GetComponent<Transform> ();
		ph = this.GetComponentInChildren<PlayerHealth> ();
        //terrainData = terrain.terrainData;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(bombInScene == true)
        {
            if (timerToMakeBomb < hzToNextBomb)
            {
                timerToMakeBomb += Time.deltaTime;
            }
            else
            {
                timerToMakeBomb = 0;
                bombs.Add(CreateBomb());
                SetStartedParametrsBomb(bombs.Count - 1);
            }
			if (bombs.Count > 0) {
				AttendanceBombing ();
			}

        }
        if(bombInScene == false && bombs.Count > 0)
        {
            ClearAll();
        }
	}
	//Function to support of Bombarding
	private void AttendanceBombing ()
	{
		for (int i = 0; i < bombs.Count; i++) {
			if (bombs [i].takeDamage == false) {
				if (bombs [i].newBombTr.position.y <= bombs [i].ygrek+offsetOfDetectionCollision && 	//Checks whether the object drops below ground
					bombs[i].timerek < maxTimer) {

					bombs [i].takeDamage = true;

                }
			}
			if (bombs [i].takeDamage == true && bombs [i].timerek < maxTimer && bombs[i] != null) {	//Operation explosion and taking damage
				bombs [i].distanceToCar = DistBtwBAC (bombs [i].psTr.position);
				if (bombs [i].distanceToCar <= radius)
					ph.CarDMG ((int)ReturnDmg (bombs [i].distanceToCar));
				
				bombs [i].timerek += Time.deltaTime;

				//if(bombs [i].bomb.activeInHierarchy == true)
				//	bombs [i].bomb.SetActive (false);										//Deactivation clone of bomb
				if (bombs [i].partSys [0].isPlaying == false) {
                    bombs[i].audioSorc.PlayOneShot(explodeClip);

                    for (int z = 0; z < bombs [i].partSys.Length; z++) {
						bombs [i].partSys [z].Play ();										//Activate explosion
					}
				}

			} else if (bombs [i].timerek >= maxTimer && bombs [i].takeDamage == true) {
				bombs [i].takeDamage = false;
				ClearIt (i);																//Delete and clean list
			}
		}
	}
	private int ReturnDmg (float dist)
	{
        if (ph.currentHealth > 0)
            return (int)((1 / (dist + 0.001f)) * 12);
        else
            return 0;
	}
    //Function To create object in Scene
    private GameObject [] CreateBombs ()
    {
        GameObject[] xx = new GameObject[3];
        Vector3 pos = CountPosition(brumTr.position);
        xx[0] = (GameObject)Instantiate(bomb, new Vector3 (pos.x, pos.y + offsetToUp, pos.z), Quaternion.Euler(0, 1, 0));  //Create clone of bomb
        xx[1] = (GameObject)Instantiate(partSys, new Vector3 (pos.x, pos.y+0.15f, pos.z), Quaternion.Euler(0, 1, 0));
        xx[2] = (GameObject)Instantiate(proje, new Vector3(pos.x, pos.y + (offsetToUp / 2), pos.z), proje.GetComponent<Transform>().rotation);
        return xx;
    }
    //Function to use set dynamic parametrs of bomb {
    private void SetStartedParametrsBomb(int i) //Function to Set Started parametrs for bomb
    {
        for (int z = 0; z < bombs[i].partSys.Length; z++)
        {
            bombs[i].partSys[z].Stop();
        }
        bombs[i].audioSorc.clip = dropTheBombClip;
        bombs[i].audioSorc.Play();
    }
    //}
    //Functions to use in create bomb {
    private Vector3 CountPosition (Vector3 pos)
	{
		Vector3 toReturn;
		toReturn.x = Random.Range (pos.x - maxMin.x, pos.x + maxMin.x);
		toReturn.z = Random.Range (pos.z - maxMin.y, pos.z + maxMin.y);
		toReturn.y = terrain.SampleHeight (new Vector3(toReturn.x, 0, toReturn.z));
      
		return toReturn;
	}
	private Bomber CreateBomb ()
	{
        GameObject[] xx = new GameObject[3]; // temp tab to create Bomb from CreateBombs ()
        xx = CreateBombs();
        WypiszLog(xx);
        return new Bomber(xx[0], xx[1], xx[0].GetComponent<Transform>(), xx[1].GetComponent<Transform>(), xx[1].GetComponent<Transform>().position.y, maxMin.x, false, 0, 
                         xx[0].GetComponent<Rigidbody>(), RetPartSys(xx[1]), xx[0].GetComponent<AudioSource>(), xx[2]);
    }
	private ParticleSystem [] RetPartSys (GameObject partSys) // return tab of particle Systems
	{
		ParticleSystem[] i = partSys.GetComponentsInChildren<ParticleSystem> ();
		return i;
	}
    //}
    //Function to clear obiects in Scene
    private void ClearAll()
    {
        for (int i = 0; i < bombs.Count; i++)
        {
            Destroy(bombs[i].bomb);
            Destroy(bombs[i].ps);
            Destroy(bombs[i].project);
            bombs.RemoveAt(i);
        }
    }
	private void ClearIt (int i)
	{
		Destroy(bombs[i].bomb);
		Destroy(bombs[i].ps);
        Destroy(bombs[i].project);
        bombs.RemoveAt(i);
	}
    private void WypiszLog(GameObject [] xx)
    {
        for (int i = 0; i < xx.Length; i++)
        {
            Debug.Log(xx[i].name);
        }
    }
	private float DistBtwBAC(Vector3 bombTr)
	{
		return Vector3.Distance (bombTr, brumTr.position);
	}
}
public class Bomber {
	public GameObject bomb;
	public GameObject ps;
    public Transform newBombTr;
    public Transform psTr;
    public float ygrek;
    public float distanceToCar;
	public bool takeDamage;
	public float timerek;
	public Rigidbody rb;
	public ParticleSystem [] partSys = new ParticleSystem[8];
    public AudioSource audioSorc;
    public GameObject project;

	public Bomber(GameObject bm, GameObject pss, Transform newBombTrans, Transform psTrr, float highY, float distToCar, bool takeDmg,
				  float timr, Rigidbody rbb, ParticleSystem [] partS, AudioSource audioS, GameObject proj){
		this.bomb = bm;
		this.ps = pss;
        this.newBombTr = newBombTrans;
        this.psTr = psTrr;
        this.ygrek = highY;
		this.distanceToCar = distToCar;
		this.takeDamage = takeDmg;
		this.timerek = timr;
		this.rb = rbb;
		this.partSys = partS;
		this.audioSorc = audioS;
        this.project = proj;
	}
}

