using UnityEngine;
using System.Collections;

public class BumperScript : MonoBehaviour {

	public GameObject bumper;
	public GameObject rootBumper;
	private Transform bumperRotation;
	private Transform rootBumperRotation;
	public float minAngle = 0;
	public float maxAngle = 20;
	private float timer1 = 0.05f;  // rotacja
	public float maxLocation = 0.2f;
	public float minLocation = -0.2f;
	private float timer2 = 0.8f; // wysokość

	// Use this for initialization
	void Start () {

		bumperRotation = bumper.GetComponent<Transform>();
		rootBumperRotation = rootBumper.GetComponent<Transform>();

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Minus) || Input.GetKey (KeyCode.KeypadMinus)) {
		
			if (timer2 < 1) {
				timer2 += Time.deltaTime / 5;
			}
			else if(timer2>1)
			{
				timer2 = 1;
			}
			AssignNewLocation (timer2);

		}
		if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus)) {

			if (timer2 <= 1) {
				timer2 -= Time.deltaTime / 5;
			}
			else if (timer2 < 0) {
				timer2 = 0;
			}
			AssignNewLocation (timer2);
				
		}									

		if (Input.GetKey(KeyCode.RightBracket)){

			if (timer1 < 1) {
				timer1 += Time.deltaTime / 2;
			}
			else if (timer1 >= 1) {
				timer1 = 1;
			}
			AssignNewRotation (timer1);


		}
		if (Input.GetKey(KeyCode.LeftBracket)){

			if (timer1 <= 1) {
				timer1 -= Time.deltaTime / 2;
			}
			else if (timer1 < 0) {
				timer1 = 0;
			}
			AssignNewRotation(timer1);
		}
	}
	private void AssignNewRotation(float timer1)
	{
		rootBumperRotation.localEulerAngles = new Vector3 (Mathf.LerpAngle (minAngle, maxAngle, timer1), rootBumperRotation.localRotation.y+180, 90);
	}
	private void AssignNewLocation(float y)
	{
		bumperRotation.localPosition = new Vector3 (bumperRotation.localPosition.x, bumperRotation.localPosition.y, Mathf.Lerp (minLocation, maxLocation, y));
	}
}
