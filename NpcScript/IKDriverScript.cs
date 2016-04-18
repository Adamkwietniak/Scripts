using UnityEngine;
using System.Collections;

public class IKDriverScript : MonoBehaviour {

	private Animator anim;
	public GameObject R_IK_Hand;
	public GameObject L_IK_Hand;
	public GameObject R_IK_Foot;
	public GameObject L_IK_Foot;
	public GameObject IK_Head;
	private Transform R_IK_Hand_TR;
	private Transform L_IK_Hand_TR;
	private Transform R_IK_Foot_TR;
	private Transform L_IK_Foot_TR;
	private Transform IK_Head_TR;

	public float mocPrzyciagania = 0.3f;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		R_IK_Hand_TR = R_IK_Hand.GetComponent<Transform> ();
		L_IK_Hand_TR = L_IK_Hand.GetComponent<Transform> ();
		R_IK_Foot_TR = R_IK_Foot.GetComponent<Transform> ();
		L_IK_Foot_TR = L_IK_Foot.GetComponent<Transform> ();
		IK_Head_TR = IK_Head.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	
	void OnAnimatorIK(){
		//Prawa Dlon
		anim.SetIKPosition (AvatarIKGoal.RightHand, R_IK_Hand_TR.position);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, mocPrzyciagania);
		anim.SetIKRotation (AvatarIKGoal.RightHand, R_IK_Hand_TR.rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1.0f);
		//LewaDlon 
		anim.SetIKPosition (AvatarIKGoal.LeftHand, L_IK_Hand_TR.position);
		anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0.9f);
		anim.SetIKRotation (AvatarIKGoal.LeftHand, L_IK_Hand_TR.rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1.0f);

		//PrawaNoga
		anim.SetIKPosition (AvatarIKGoal.RightFoot, R_IK_Foot_TR.position);
		anim.SetIKPositionWeight (AvatarIKGoal.RightFoot, mocPrzyciagania);
		anim.SetIKRotation (AvatarIKGoal.RightFoot, R_IK_Foot_TR.rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.RightFoot, 1.0f);

		//LewaNoga
		anim.SetIKPosition (AvatarIKGoal.LeftFoot, L_IK_Foot_TR.position);
		anim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, mocPrzyciagania);
		anim.SetIKRotation (AvatarIKGoal.LeftFoot, L_IK_Foot_TR.rotation);
		anim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, 1.0f);

		//Glowa
		anim.SetLookAtPosition(IK_Head_TR.position);
		anim.SetLookAtWeight(0.1f);

	}
}
