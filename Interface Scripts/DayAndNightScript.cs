using UnityEngine;
using System.Collections;

public class DayAndNightScript : MonoBehaviour {

	private Transform trans;

	public float speed = 5f;
	public Light sun;
	public float maxIntesity = 2f;
	public float minIntesity = 0f;
	public float maxAmbient = 0.7f;
	public float minAmbient = 0f;
	public Color dayColor = new Color (80, 80, 80, 80);
	public Color nightColor = new Color (30, 30, 30, 250);
	private Transform stars;
	public GameObject lightInBase;


	// Use this for initialization
	void Start () {

		trans = GetComponent<Transform> ();
		stars = trans.FindChild ("Stars");
	
	}
	
	// Update is called once per frame
	void Update () {

		trans.Rotate (0, 0, speed * Time.deltaTime);
		SetLights ();
	
	}

	public void SetLights (){

		if (trans.rotation.eulerAngles.z > 0 && trans.rotation.eulerAngles.z < 180) {
			sun.intensity = maxIntesity;
			RenderSettings.ambientIntensity = maxAmbient;
			RenderSettings.ambientLight = dayColor;

			stars.gameObject.SetActive (false);
			lightInBase.SetActive (false);

		} else {
			sun.intensity = minIntesity;
			RenderSettings.ambientIntensity = minAmbient;
			RenderSettings.ambientLight = nightColor;

			stars.gameObject.SetActive(true);
			lightInBase.SetActive (true);
		}	
	}
}
