using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class KurzDlaTekstury/* : MonoBehaviour */{

    //Takstura z która zostanie powiązany bool czyKurz.
    public Texture tekstura;
	public int teksturaDeformChangeIndex;
    //czyKurz jest to zmienna ładowana przykonkretnej teksturze
    public bool czyKurz;
	public bool czyBloto;
	public bool czyBagno;
	public bool changeTex;
	public float wspolczynnikBagiennosci;
	public float wielkoscBageinnosci;
	public float wspolczynnikOporu;
	public float valDynFric;
	public float bauc;
	public enum ChoiceComb{Minimum=1, Maximum=2, Average=3, Multiply=4};
	public ChoiceComb combFri;
	public ChoiceComb combBou;
	public enum ChoicePhis{RccAsphalt=1, RccSand=2, RccGrass=3};
	public ChoicePhis choiceGround;
	[HideInInspector]public Color32 collor;

	/*private PhysicMaterial ps;

	void Start()
	{
		ps = GetComponent<PhysicMaterial> ();
	}*/

}

