using UnityEngine;
using System.Collections;

public class HouseAndGateScript : MonoBehaviour {

	public GameObject[] obiectsToDisableHouse = new GameObject[1];
	public GameObject[] obiectsToEnableHouse = new GameObject[1];

	public GameObject[] obiectsToDisableWall = new GameObject[1];
	public GameObject[] obiectsToEnableWall = new GameObject[1];

	public GameObject dust;
	public GameObject explosion;
	private Transform []tempTrans = new Transform[4];
	UseCameraScript ucs;
	public Camera [] cams;
	private Animation [] anims = new Animation[2];
	private AnimationClip[] animClip = new AnimationClip[2];
	private float[] timesOfAnimation = new float[2];
	private float[] actualOfAnimation = {0, 0};
	[HideInInspector]public bool isCamAnim = false;
	[HideInInspector]public string nameObiect = "ObslugaZdarzen";
	private string[] names = { "First", "DestroyWall" };
	private bool[] doneTab = { false, false };
	private bool[] doneAll = { false, false };
	// Use this for initialization
	private int tempCamIndex = 0;
	RCCCarControllerV2 rcc;
	public AudioSource []audioS;
	public AudioClip destroy;

	void Awake ()
	{
		ucs = (UseCameraScript)FindObjectOfType (typeof(UseCameraScript)) as UseCameraScript;
		tempTrans [0] = obiectsToDisableHouse [0].GetComponent<Transform> ();
		tempTrans [1] = obiectsToEnableHouse [0].GetComponent<Transform> ();
		tempTrans [2] = obiectsToDisableWall [0].GetComponent<Transform> ();
		tempTrans [3] = obiectsToEnableWall [0].GetComponent<Transform> ();
		rcc = (RCCCarControllerV2)FindObjectOfType (typeof(RCCCarControllerV2)) as RCCCarControllerV2;
	}
	void Start () {
		for (int i = 0; i < cams.Length; i++) {
			anims [i] = cams [i].GetComponent<Animation> ();
			cams [i].enabled = false;
			animClip [i] = anims [i].clip;
			//Debug.Log(anims[i]);
		}
		for (int i = 0; i < anims.Length; i++) {
			timesOfAnimation [i] = anims [i].clip.length;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isCamAnim == true) {
			//Debug.Log ("dzialam petla update");
			for (int i = 0; i < names.Length; i++) {
				//Debug.Log ("Sprawdzam: "+nameObiect+" z "+names [i]);
				if (nameObiect == names [i]) {
					//Debug.Log ("nazwy sie zgadzaja");
					if (audioS [i].isPlaying == false)
						audioS [i].PlayOneShot (destroy);
					actualOfAnimation [i] += Time.deltaTime;
					if (actualOfAnimation [i] < timesOfAnimation [i] / 2) {
						//Debug.Log ("actual animation jest w 1 fazie");
						if (cams [i].enabled == false)
							ChangeCamera (cams [i], anims [i]);
						rcc.gameObject.GetComponent<Rigidbody> ().Sleep ();
						if (names [i] == "First") {
							if (doneTab [i] == false) {
								CreateExplosionAndDust (tempTrans [0].position, true);
								CreateExplosionAndDust (tempTrans [1].position, false);
								DisableObiect (obiectsToDisableHouse, obiectsToEnableHouse, true, false);
								doneTab [i] = true;
							}
							for (int j = 0; j < obiectsToDisableHouse.Length; j++) {
								TransformObiect (obiectsToDisableHouse [j], false);
							}
							for (int j = 0; j < obiectsToEnableHouse.Length; j++) {
								TransformObiect (obiectsToEnableHouse [j], true);
							}
						} else if (names [i] == "DestroyWall") {
							if (doneTab [i] == false) {
								CreateExplosionAndDust (tempTrans [2].position, true);
								CreateExplosionAndDust (tempTrans [3].position, false);
								DisableObiect (obiectsToDisableWall, obiectsToEnableWall, true, false);
								doneTab [i] = true;
							}
							for (int j = 0; j < obiectsToDisableWall.Length; j++) {
								TransformObiect (obiectsToDisableWall [j], false);
							}
							for (int j = 0; j < obiectsToEnableWall.Length; j++) {
								TransformObiect (obiectsToEnableWall [j], true);
							}
						}
					}
					else if(actualOfAnimation [i] >= timesOfAnimation [i] / 2 && actualOfAnimation [i] < timesOfAnimation [i])
					{
						if (doneAll [i] == false) {
							if (names [i] == "First") {
								DisableObiect (obiectsToDisableHouse, obiectsToEnableHouse, false, true);
							} else if (names [i] == "DestroyWall") {
								DisableObiect (obiectsToDisableWall, obiectsToEnableWall, false, true);
							}
							doneAll [i] = true;
						}
					}
					else if (actualOfAnimation [i] > timesOfAnimation [i]) {
						BackCamera (cams [i]);
						//Debug.Log ("koniec");
						names [i] = "skonczonaBajkaDla" + i;
					}
				}
			}
		}
	}
	private void CreateExplosionAndDust (Vector3 place, bool ii)
	{
		if (ii == true) {
			Instantiate (explosion, place, Quaternion.Euler (0, 1, 0));
			Instantiate (dust, place, Quaternion.Euler (0, 1, 0));
		} else {
			Instantiate (dust, place, Quaternion.Euler (0, 1, 0));
		}
	}
	private void ChangeCamera (Camera cam, Animation anim)
	{
		tempCamIndex = checkActiveCameraIndex ();
		cam.enabled = true;
		cam.gameObject.GetComponent<AudioListener> ().enabled = true;
		ucs.camers [tempCamIndex].enabled = false;
		ucs.camers [tempCamIndex].gameObject.GetComponent<AudioListener> ().enabled = false;
		anim.Play ();
	}
	private void BackCamera (Camera cam)
	{
		ucs.ChangeCams (tempCamIndex);
		cam.gameObject.GetComponent<AudioListener> ().enabled = false;
		cam.enabled = false;
	}
	private void DisableObiect (GameObject[] tabDis, GameObject[] tabEnbl, bool isEnbl, bool isDisable)
	{
		
		//Instantiate (dust, tabDis [0].transform.position, Quaternion.Euler(0,1,0));
		if (isDisable == true) {
			for (int i = 0; i < tabDis.Length; i++) {
				tabDis [i].SetActive (false);
			}
		}
		//Instantiate(dust, tabEnbl[0].transform.position, Quaternion.Euler(0,1,0));
		if (isEnbl == true) {
			for (int i = 0; i < tabEnbl.Length; i++) {
				tabEnbl [i].SetActive (true);
			}
		}
	}
	private int checkActiveCameraIndex ()
	{
		for (int i = 0; i < ucs.camers.Length; i++) {
			if (ucs.camers [i].enabled == true) {
				return i;
				break;
			}
		}
		return -1;
	}
	private void TransformObiect (GameObject obj, bool uP)
	{
		if (uP == true) {
			obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y + 0.05f, obj.transform.position.z);
		} else {
			obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y - 0.05f, obj.transform.position.z);
		}
	}
}
