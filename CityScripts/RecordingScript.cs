using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecordingScript : MonoBehaviour {

	private GameObject vehicle;
	private Transform trVehicle;
	private float timer = 0.0f;
	private bool writeInformation = false;
	private bool isRecording = false;
	private static int counting = 0;
	private GameObject startOfMap;
	private Transform startOfMapTr;
	//private List<TransformClass> trClass = new List<TransformClass>();

	public AnimationClip animClip;
	public Image imageView;
	public float maxValue = 0.25f;	//częstotliwość keyFramow

	private AnimationCurve posX;
	private AnimationCurve posY;
	private AnimationCurve posZ;
	private AnimationCurve rotX;
	private AnimationCurve rotY;
	private AnimationCurve rotZ;
	private AnimationCurve rotW;

	void Awake ()
	{
		vehicle = this.gameObject;
		trVehicle = vehicle.GetComponent<Transform> ();
		startOfMap = new GameObject ();
		startOfMap.name = "Start of Map";
		startOfMapTr = startOfMap.GetComponent<Transform>();
		startOfMapTr.position = Vector3.zero;
		startOfMapTr.eulerAngles = Vector3.zero;

		trVehicle.SetParent(startOfMapTr);
		//animClip.ClearCurves();
		posX = new AnimationCurve ();
		posY = new AnimationCurve ();
		posZ = new AnimationCurve ();
		rotX = new AnimationCurve ();
		rotY = new AnimationCurve ();
		rotZ = new AnimationCurve ();
		rotW = new AnimationCurve ();
		//animClip.ClearCurves ();
	}
	void Start () {
		animClip.legacy = true;
	}

	// Update is called once per frame
	void Update () {

		if (isRecording == true) {
			if (writeInformation == true) {
				//trClass.Add (trVehicle.position, trVehicle.rotation);
				writeInformation = false;
				timer = 0;
				RecordToAnimationClip (counting, trVehicle.position, trVehicle.rotation);
				counting++;
			} else {
				timer += Time.deltaTime;
			}
			if (timer >= maxValue && writeInformation == false) {
				writeInformation = true;
			}
		}
		if(Input.GetKeyDown(KeyCode.K))
			RecordFunction();
	}
	void RecordToAnimationClip (int i, Vector3 pos, Quaternion rot)
	{
		posX.AddKey (i, pos.x);
		posY.AddKey (i, pos.y);
		posZ.AddKey (i, pos.z);
		rotX.AddKey (i, rot.x);
		rotY.AddKey (i, rot.y);
		rotZ.AddKey (i, rot.z);
		rotW.AddKey (i, rot.w);

		animClip.SetCurve("", typeof(Transform), "localPosition.x", posX);
		animClip.SetCurve("", typeof(Transform), "localPosition.y", posY);
		animClip.SetCurve("", typeof(Transform), "localPosition.z", posZ);

		animClip.SetCurve("", typeof(Transform), "localRotation.x", rotX);
		animClip.SetCurve("", typeof(Transform), "localRotation.y", rotY);
		animClip.SetCurve("", typeof(Transform), "localRotation.z", rotZ);
		animClip.SetCurve("", typeof(Transform), "localRotation.w", rotW);

	}
	public void RecordFunction()
	{
		isRecording = !isRecording;
		if (isRecording == true)
			imageView.enabled = true;
		else
			imageView.enabled = false;
	}
}
/*
[System.Serializable]
public class TransformClass : RecordingScript
{
	public Vector3 trPosition;
	public Quaternion trRotation;

	public TransformClass (Vector3 trPos, Quaternion trRot)
	{
		this.trPosition = trPos;
		this.trRotation = trRot;
	}
}*/
/*
Niezbedne info do nagrywania:
Aby nagrać obiekt nalezy dodać skrypt do obiektu nagrywanego, dodać component animation i do animation jak i do skryptu podlaczyc animation clip.
W animation ilość sampli ustawić w taki sposób, aby max Value wychodził w proporcji (jeśli chodzi o nagrywanie czołgu) 1 = max value -:- 8.33 sampla
(Przy testowanym przejezdzie ustawione było 0.6 max value, a 5 sampli nagrywania).
Animation Clip musi mieć dodane properties Location i Rotation ustawione na Quaternion
Po nagraniu wcisnąć curves, aby pojawily sie zmiany. Odtwarzanie tylko przez Component Animation, zaś należy zwiększyć ilość sampli odtwarzanie do 8 - 10.
Pozdro.
*/
