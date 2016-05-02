using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
//[RequireComponent(typeof(Animator))] 
[Serializable]
public class EnemyClassScript {

	[HideInInspector]public Animator anim;
	[HideInInspector]public NavMeshAgent agent;
	public GameObject enemyObject;
	public GameObject defaultPositionObject;
	public int maxDistance;
	public int minDistance;
	public int maxDistanceFromDefaultPosition;
	public int dmgFromShoot;
	public bool enemyIsStatic;
	[HideInInspector]public bool isShooting;
	[HideInInspector]public bool isRun;
	[HideInInspector]public bool isWalk;
	[HideInInspector]public bool isWait;
	[HideInInspector]public bool isLife;
	[HideInInspector]public bool wantGetUp;
	[HideInInspector]public bool onTheGround;
	[HideInInspector]public float timerToGetUp;
	[HideInInspector]public float timerToShoot;
	[HideInInspector]public float distFromPlayer;
	[HideInInspector]public float distFromDefPos;
	[HideInInspector]public float actualSpeed;
	[HideInInspector]public int actualInt;
	[HideInInspector]public Rigidbody [] rigbodyList = new Rigidbody[13];
	[HideInInspector]public GameObject[] obiectToChangeTag = new GameObject[13];
	[HideInInspector]public Transform transDefPos;
	[HideInInspector]public Transform enemyTr;
	public GameObject PartSys;
	[HideInInspector]public AudioSource audiosorce;



	public EnemyClassScript (Animator anima, NavMeshAgent nma, GameObject enemyOb, GameObject go, 
	                         int maxDis, int minDis, int maxDisFDP, int dmgfs, bool eis, bool iS, 
	                         bool iR, bool iWalk, bool iW, bool iL, bool wGU, bool oTG, float ttgu, float ttS, 
		float distFrPl, float disFrDP, float actSp , int aktIn, Rigidbody[] rbb, GameObject[] tchangeTag,
							 Transform tr, Transform trEnemy, GameObject partSystem, AudioSource audiosorc
	                         ){
		this.anim = anima;
		this.agent = nma;
		this.enemyObject = enemyOb;
		this.defaultPositionObject = go;
		this.maxDistance = maxDis;
		this.minDistance = minDis;
		this.maxDistanceFromDefaultPosition = maxDisFDP;
		this.dmgFromShoot = dmgfs;
		this.enemyIsStatic = eis;
		this.isShooting = iS;
		this.isRun = iR;
		this.isWalk = iWalk;
		this.isWait = iW;
		this.isLife = iL;
		this.wantGetUp = wGU;
		this.onTheGround = oTG;
		this.timerToGetUp = ttgu;
		this.timerToShoot = ttS;
		this.distFromPlayer = distFrPl;
		this.distFromDefPos = disFrDP;
		this.actualSpeed = actSp;
		this.actualInt = aktIn;
		this.rigbodyList = rbb;
		this.obiectToChangeTag = tchangeTag;
		this.transDefPos = tr;
		this.enemyTr = trEnemy;
		this.PartSys = partSystem;
		this.audiosorce= audiosorc;
	}



}
