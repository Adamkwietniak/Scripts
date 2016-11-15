using UnityEngine;
using System.Collections;

public class SparkleScript : MonoBehaviour {

	public GameObject [] sparkles = new GameObject[2];

	private ParticleSystem [] sparklesPS = new ParticleSystem[5];
	private Transform[] transformPS = new Transform[5];
	[HideInInspector]public bool isDmgCar = false;
    [HideInInspector]
    public bool isDam = false;
	private ContactPoint contact;
	private Quaternion rot;
	private Vector3 pos = new Vector3(0, 0, 0);
	private int randSparkle = 0;
	string partToSparkle = "Prefabs/";
	// Use this for initialization
	void Start () {
		sparkles[0] = Resources.Load((partToSparkle+"Contact Sparkles"), typeof(GameObject)) as GameObject;
		sparkles[1] = Resources.Load((partToSparkle+"FlareMobile"), typeof(GameObject)) as GameObject;
		for (int i = 0; i < sparkles.Length; i++) {
			if(sparkles[i] != null)
			{
				sparklesPS[i] = sparkles[i].GetComponent<ParticleSystem>();
				transformPS[i]=sparkles[i].GetComponent<Transform>();
			}
		}
	}
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if (isDmgCar == false) {
			isDmgCar = true;
            contact = collision.contacts[0];
			SparkleFunction ();
		}
        if(isDam == false)
            isDam = true;
    }
	private void SparkleFunction ()
	{
			rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			pos = contact.point;
			if(sparkles.Length>1)
				randSparkle = Random.Range(0, sparkles.Length);
			else if(sparkles.Length == 1)
				randSparkle = 0;
			Instantiate(sparkles[randSparkle], pos, rot);
		Debug.Log("Uderzylem, co mi szkodzi "+randSparkle);
        isDmgCar = false;

	}
}
