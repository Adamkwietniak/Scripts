using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SprawdzTerenScript : MonoBehaviour
{
    private Transform trans;

	[HideInInspector]public GameObject gejmObject;
	private Collider gejmObjectCollider;
	private GameObject tempGO;

	public bool kolizjaZObjektem = false;
	public bool kolizjaZCzymkolwiek = false;
    public KurzDlaTekstury[] czyKurz;

    private KurzDlaTekstury aktualnyKurz;
	public GameObject obj;
	public PhysicMaterial [] materialPhiysics; // 1 element asfalt, 2gi element piasek, 3 element grass
	public GameObject terrainObj;
	public GameObject skidMarkAspalt;
	private TerrainCollider terrainCollider;
	[HideInInspector]public Terrain terrain;
	[HideInInspector]public TerrainData terrainData;

    bool temp = false;
	bool pobrane = false;
	bool mudIs = false;
	bool deformIs = false;

	DustScript dss;
	MudScript ms;
	DynamicTerenScript td;
	RCCCarControllerV2 rcc;

	private Texture activeTexture;
	private PhysicMaterial activeMaterial;
	private int indMat = 0;

	private float fricAsfalt = 0;
	private float statAsfalt = 0;
	private PhysicMaterialCombine fAsfalt;
	private PhysicMaterialCombine cAsfalt = 0;

	void Awake ()
	{
		terrain = terrainObj.GetComponent<Terrain> ();
		terrainData = terrain.terrainData;
		//Debug.Log (terrainData);
	}
    void Start()
    {

		skidMarkAspalt.SetActive(false);
		dss = GetComponentInChildren<DustScript>();
		ms = GetComponent<MudScript> ();
        trans = GetComponent<Transform>();
		td = terrainObj.GetComponent<DynamicTerenScript>();
		rcc = GetComponent<RCCCarControllerV2>();
		terrainCollider = terrainObj.GetComponent<TerrainCollider>();
		for (int i = 0; i < czyKurz.Length; i++) {
			czyKurz[i].collor = AverageOfColorInTexture((Texture2D)(czyKurz [i].tekstura));
			//Debug.Log(czyKurz[i].collor);
		}
		fricAsfalt = materialPhiysics[0].dynamicFriction;
		statAsfalt = materialPhiysics[0].staticFriction;
		fAsfalt = materialPhiysics[0].frictionCombine;
		cAsfalt = materialPhiysics[0].bounceCombine;
    }

	void Update ()
	{
		StartCoroutine (AttendanceScriptUpdate ());		
    }
	private IEnumerator AttendanceScriptUpdate ()
	{
		yield return new WaitForSeconds (1);
		if(kolizjaZObjektem == false && kolizjaZCzymkolwiek == true){
			AttendanceAllTextures ();
			if(tempGO != null)
				tempGO = null;
		}
		else if(kolizjaZObjektem == true && kolizjaZCzymkolwiek == true)
		{
			if(tempGO != gejmObject){
				gejmObjectCollider = GetColliderFromObj(gejmObject);
				//Debug.Log("Phisics to: "+gejmObjectCollider.sharedMaterial.name+ " a phisic 2: "+ materialPhiysics[0].name);
				if (gejmObjectCollider.sharedMaterial != null) {
					if (gejmObjectCollider.sharedMaterial.name == materialPhiysics [0].name + " (Instance)") {		//tu mozna dodac dzwiek jazdy po asfalcie
						rcc.AssignMaterial (1);
						skidMarkAspalt.SetActive (true);
					}
					tempGO = gejmObject; 
				}
			}
			if(gejmObjectCollider.material.name == materialPhiysics[0].name)		//tu mozna dodac dzwiek jazdy po asfalcie
			{
				if(skidMarkAspalt.activeInHierarchy == false){
					skidMarkAspalt.SetActive(true);
				}
			}

		}
	}
	private Collider GetColliderFromObj(GameObject go)
	{
		return go.GetComponent<Collider>();
	}
	private bool TexInPosition (int i, Vector3 pos)
	{
		if(czyKurz[i] != null && czyKurz[i].tekstura.name.Equals(PowierzchniaTerenu.NazwaTeksturyWPozycji(pos, terrain, terrainData)))
			return true;
		else
			return false;
	}
	private void AttendanceAllTextures ()
	{
		for (int i = 0; i < czyKurz.Length; i++) {
			if (TexInPosition (i, trans.position) == true) {
				if (activeTexture != czyKurz [i].tekstura) {
					SetMaterialTerrain (i);
					if (czyKurz [i].czyKurz == true) {
						Wykonaj (i);
					}
					if (czyKurz [i].czyBagno == true || czyKurz [i].changeTex == true) {
						AssignDeform (i);
					}
					if (czyKurz [i].wspolczynnikOporu != 0.1f) {
						SetOfDrag (i);
					}
					if (czyKurz [i].czyBloto == true) {
						AttendanceMudSrc (i);
					}
				}
			}
		}
	}
	private void SetMaterialTerrain (int i)
	{
		rcc.AssignMaterial ((int)czyKurz[i].choiceGround);
		activeTexture = czyKurz[i].tekstura;
		for(int z = 0; z < materialPhiysics.Length;z++){
			if((int)(czyKurz[i].choiceGround-1) == z){
				if(z != 0){
					activeMaterial = materialPhiysics[z]; 
					if(skidMarkAspalt.activeInHierarchy == true)
						skidMarkAspalt.SetActive(false);
				}
				else
				{
					materialPhiysics[z].dynamicFriction = fricAsfalt;
					materialPhiysics[z].staticFriction = statAsfalt;
					materialPhiysics[z].frictionCombine = fAsfalt;
					materialPhiysics[z].bounceCombine = cAsfalt;
					activeMaterial = materialPhiysics[z];
					if(skidMarkAspalt.activeInHierarchy == false)
						skidMarkAspalt.SetActive(true);
				}
			}
		}
		UstawFric (i); 
		terrainCollider.sharedMaterial = activeMaterial;
			
	}
	private void Wykonaj(int i)
    {
      
		aktualnyKurz = czyKurz[i];
		dss.sprawdzam = aktualnyKurz.czyKurz;
		activeMaterial.dynamicFriction = czyKurz[i].valDynFric;
		activeMaterial.staticFriction = czyKurz[i].valDynFric+0.05F;
		activeMaterial.bounciness = czyKurz[i].bauc;              
    }
	private void AssignDeform (int i)
	{
		if (rcc.speed>0) {
			deformIs = false;
			for (int z = 0; z < ms.psTire.Count; z++) {
				if (ms.psTire [z].currentWheelCollider.isGrounded == true && ms.psTire [z].onTheGround == false) {
					ms.psTire [z].onTheGround = true;
					//Debug.Log(ms.psTire [i].tire.name +" jest true");
				} else if (ms.psTire [z].currentWheelCollider.isGrounded == false && ms.psTire [z].onTheGround == true) {
					ms.psTire [z].onTheGround = false;
					//Debug.Log(ms.psTire [i].tire.name +" jest false");
				}
			}
				if (czyKurz [i].czyBagno == true && czyKurz [i].wspolczynnikBagiennosci > 0) {
					for (int j = 0; j < ms.psTire.Count; j++) {
						if (ms.psTire [j].onTheGround == true) {
						if (TexInPosition(i, ms.psTire [j].particle.transform.position)) {
								if(czyKurz[i].changeTex == true)
									td.ShapeArea (ms.psTire [j].trTire.position, ((czyKurz [i].wielkoscBageinnosci) / 10), czyKurz [i].wspolczynnikBagiennosci,czyKurz[i].teksturaDeformChangeIndex, true);
								else
									td.ShapeArea (ms.psTire [j].trTire.position, ((czyKurz [i].wielkoscBageinnosci) / 10), czyKurz [i].wspolczynnikBagiennosci,czyKurz[i].teksturaDeformChangeIndex, false);
								//Debug.Log("funkcja do zmiany terenu zostala wywolana");
						}
					}
				}
			}
		}
	}
	private void AttendanceMudSrc (int i)
	{
		mudIs = false;
		for (int j = 0; j < ms.psTire.Count; j++) {
			if (TexInPosition(i, ms.psTire [j].particle.transform.position)) {
				mudIs = true;
				ms.psTire [j].generateMud = true;
				ms.psTire[j].rend.material.color = czyKurz[i].collor;
				ms.psTire[j].rend.material.color = czyKurz[i].collor;
			}
			if(!mudIs) {
				ms.psTire [j].generateMud = false;
			}
		}
	}
	private void SetOfDrag (int i)
	{
		if(czyKurz[i].wspolczynnikOporu != 0)
		{
			rcc.changeDrag = czyKurz[i].wspolczynnikOporu;
		}
		else if(czyKurz[i].wspolczynnikOporu == 0 && rcc.changeDrag == 0)
		{
			rcc.changeDrag = 0.1f;
		}

	}
	private Color32 AverageOfColorInTexture (Texture2D tex)
	{
		Color32[] texColors = tex.GetPixels32 ();
		
		int total = texColors.Length;
		
		float r = 0;
		float g = 0;
		float b = 0;
		
		for(int i = 0; i < total; i++)
		{
			
			r += texColors[i].r;
			
			g += texColors[i].g;
			
			b += texColors[i].b;
			
		}
		
		return new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 255);
	}

	void UstawFric (int i)
	{
		for(int z = 0; z < materialPhiysics.Length; z++)
		{
			switch((int)czyKurz[i].combFri)
			{
			case 1:
				materialPhiysics[z].frictionCombine = PhysicMaterialCombine.Minimum;
				break;
			case 2:
				materialPhiysics[z].frictionCombine = PhysicMaterialCombine.Maximum;
				break;
			case 3:
				materialPhiysics[z].frictionCombine = PhysicMaterialCombine.Average;
				break;
			case 4:
				materialPhiysics[z].frictionCombine = PhysicMaterialCombine.Multiply;
				break;

			default:
				break;
			}
			switch((int)czyKurz[i].combBou)
			{
			case 1:
				materialPhiysics[z].bounceCombine = PhysicMaterialCombine.Minimum;
				break;
			case 2:
				materialPhiysics[z].bounceCombine = PhysicMaterialCombine.Maximum;
				break;
			case 3:
				materialPhiysics[z].bounceCombine = PhysicMaterialCombine.Average;
				break;
			case 4:
				materialPhiysics[z].bounceCombine = PhysicMaterialCombine.Multiply;
				break;

			default:
				break;
			}
			if(czyKurz[i].valDynFric <=1)
				materialPhiysics[z].dynamicFriction = czyKurz[i].valDynFric;
			else
				materialPhiysics[z].dynamicFriction = 1;
			if(czyKurz[i].valDynFric <=0.95f)
				materialPhiysics[z].staticFriction = czyKurz[i].valDynFric+0.05f;
			else 
				materialPhiysics[z].staticFriction = 1;
		}
	}
} 
/*
 * Optymalne parametry:
 * 				Lod Trawa   Metal Drewno Szuter Skala Guma Asfalt  Snieg
 * dynamic Fri	0.1  0.2    0.25   0.45    0.5   0.35   1    0.75   0.15
 * Static Fri   0.1  0.2    0.25   0.45    0.5   0.35   1 	 0.75   0.15
 * Bauciness	 0	0.001	 0	   0.1	    0	 0.1	0	 0.1     0
 * FriCom		min	 min	avg	   avg	   avg   max   min	 avg    min
 * BouCom		min  min    avg	   avg	   avg   avg   avg	 avg    min
*/