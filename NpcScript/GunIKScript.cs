using UnityEngine;
using System.Collections;

public class GunIKScript : MonoBehaviour {

	private Animator anim;
	public GameObject R_IK_Object;
	public GameObject L_IK_Object;
	public GameObject miejsceWDloniR;
	public GameObject modelBroni;
	public float mocPrzyciagania = 0.3f;

	public Vector3 rotacjaoffset1 = new Vector3(0.5f , -0.8f, 1.0f);
	public Vector3 rotacjaoffset2 = new Vector3(1.2f , 3.0f, -1.0f);
	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		modelBroni.GetComponent<Transform> ().position = miejsceWDloniR.GetComponent<Transform> ().position;
		Quaternion pistoletRotacjaOffset = Quaternion.FromToRotation (rotacjaoffset1, rotacjaoffset2);
		modelBroni.GetComponent<Transform> ().rotation = R_IK_Object.GetComponent<Transform> ().rotation * pistoletRotacjaOffset;
	}

	void OnAnimatorIK(){
		//Prawa Dlon
		anim.SetIKPosition (AvatarIKGoal.RightHand, R_IK_Object.GetComponent<Transform> ().position);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, mocPrzyciagania);
		anim.SetIKRotation (AvatarIKGoal.RightHand, R_IK_Object.GetComponent<Transform> ().rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1.0f);
		//LewaDlon 
		anim.SetIKPosition (AvatarIKGoal.LeftHand, L_IK_Object.GetComponent<Transform> ().position);
		anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, mocPrzyciagania);
		anim.SetIKRotation (AvatarIKGoal.LeftHand, L_IK_Object.GetComponent<Transform> ().rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1f);

	}
}
/*
 * Działanie skryptu:
 * Skrypt ma za zadanie uruchomić możliwość oddziaływania na ruchy kończynami (w tym przypadku zaimplementowane są tylko prawa
 * i lewa dłoń) tak aby mimo wgranych animacji w postać można było dynamicznie sterować ruchem tych kończyn. 
 * Zaimplementowanie Kinematyki odwrotnej (Inverse Kinematic) wymaga odpowiednich obiektów do poprawnego działania:
 * - R_IK_Object - Obiekt do którego będzie przyciągana prawa dłoń,
 * - L_IK_Object - Obiekt do którego będzie przyciągana lewa dłoń,
 * - miejsceWDloniR - jest to obiekt który jest przyciągany do R(L)_IK_Object jak również osadzenie broni.
 * - modelBroni - to poprostu prefab broni
 * */