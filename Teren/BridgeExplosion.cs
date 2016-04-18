using UnityEngine;
using System.Collections;

public class BridgeExplosion : MonoBehaviour {

	public float radius = 15.0f;
	public float power = 2000.0f;

	public GameObject [] explosions = new GameObject[1];
	public RigBdyBridgeScript[] dzialaj;
	//private RigBdyBridgeScript[] aktualnyDzialaj;
	Vector3 moc;
	float zm = 1.0f;
	bool czy;
	Vector3 explosionPos;
	PlayerHealth ph;
	UseCameraScript ucs;
	//Rigidbody rd;

	private Transform trans;
	void Awake ()
	{
		ph = (PlayerHealth)FindObjectOfType(typeof(PlayerHealth));
		ucs = (UseCameraScript)FindObjectOfType(typeof(UseCameraScript));
		trans = GetComponent<Transform> ();
		czy = false;
	}
	// Use this for initialization
	void Start () {
		foreach (GameObject expl in explosions) {
			expl.SetActive(false);
		}
		explosionPos = explosions [0].transform.position;
		foreach (RigBdyBridgeScript item in dzialaj) {
				item.rb.useGravity = false;
				item.rb.isKinematic = true;
				item.rb.drag = 0.0025f;
				item.rb.angularDrag = 0.05f;
		}
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider _BridgeExplos) {
		if (_BridgeExplos.tag == "Player" && czy == false) {
			czy = true;
			foreach (GameObject expl in explosions) {
				expl.SetActive(true);
			}
			UruchomRB();
		}
	}
	void UruchomRB ()
	{
		Time.timeScale = 0.2f;
		ucs.ExplosiveCamera ();
		ph.currentHealth = 0;
		//Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (RigBdyBridgeScript item in dzialaj) {
			item.rb.useGravity = true;
			item.rb.isKinematic = false;
			item.rb.AddExplosionForce(power, explosionPos, radius, 10.0F);

		}

	}
}