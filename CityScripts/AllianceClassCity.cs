using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class AllianceClassCity{

	[HideInInspector]public Animator anim;
	[HideInInspector]public NavMeshAgent agent;
	[HideInInspector]public Rigidbody [] rBody = new Rigidbody[13];
	public GameObject allianceNpc;
	public bool missionNPC;
	[HideInInspector]public Transform selfTransform;
	public Transform defaultPos;
	[HideInInspector]public bool isLife;
	[HideInInspector]public SkinnedMeshRenderer [] SkinnedMeshR = new SkinnedMeshRenderer[3];
	public GameObject particleSys;
	[HideInInspector]public float discanceFromPlayer;
	[HideInInspector]public bool done;
	public bool isPatrol = false;
	public Transform[] followedTarget = new Transform[1];
    public int numberOfGroup;
    [HideInInspector]public bool inBase;
    [HideInInspector]public bool isWalk;
    [HideInInspector]public bool isHide;


	public AllianceClassCity (Animator animat, NavMeshAgent navMeshAgent, Rigidbody[] rigBody, GameObject soliderObj, bool misionNPC, Transform selfTrans, 
	                            Transform defPos, bool life, SkinnedMeshRenderer [] sMr, GameObject partSys, float distFrPlayer, bool donee, bool isPatrolled,
	                            Transform [] follTarg, int numberGroup, bool inbas, bool walking, bool hide){
		this.anim = animat;
		this.agent = navMeshAgent;
		this.rBody = rigBody;
		this.allianceNpc = soliderObj;
		this.missionNPC = misionNPC;
		this.selfTransform = selfTrans;
		this.defaultPos = defPos;
		this.isLife = life;
		this.SkinnedMeshR = sMr;
		this.particleSys = partSys;
		this.discanceFromPlayer = distFrPlayer;
		this.done = donee;
		this.isPatrol = isPatrolled;
		this.followedTarget = follTarg;
        this.numberOfGroup = numberGroup;
        this.inBase = inbas;
        this.isWalk = walking;
        this.isHide = hide;
	}
}
