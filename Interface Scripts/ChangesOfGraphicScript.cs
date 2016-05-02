using UnityEngine;
using System.Collections;

public class ChangesOfGraphicScript : MonoBehaviour {

	[HideInInspector]public Terrain terrain;

	void Awake ()
	{
		terrain = SetTerrain ();
	}
	public void SetNewParameters(int i)
	{
		switch (i) {
		case 0:						//Fastest
			terrain.detailObjectDistance = 70;
			terrain.detailObjectDensity = 0.5f;
			terrain.treeDistance = 100;
			terrain.treeBillboardDistance = 35;
			terrain.treeCrossFadeLength = 25;
			break;
		case 1:						//Fast
			terrain.detailObjectDistance = 100;
			terrain.detailObjectDensity = 0.6f;
			terrain.treeDistance = 130;
			terrain.treeBillboardDistance = 55;
			terrain.treeCrossFadeLength = 34;
			break;
		case 2:						//Simple
			terrain.detailObjectDistance = 120;
			terrain.detailObjectDensity = 0.85f;
			terrain.treeDistance = 160;
			terrain.treeBillboardDistance = 95;
			terrain.treeCrossFadeLength = 38;
			break;
		case 3:						//Good
			terrain.detailObjectDistance = 140;
			terrain.detailObjectDensity = 1;
			terrain.treeDistance = 200;
			terrain.treeBillboardDistance = 105;
			terrain.treeCrossFadeLength = 43;
			break;
		case 4:						//Beautyfull
			terrain.detailObjectDistance = 200;
			terrain.detailObjectDensity = 1;
			terrain.treeDistance = 500;
			terrain.treeBillboardDistance = 200;
			terrain.treeCrossFadeLength = 100;
			break;
		case 5:						//Fantastic
			terrain.detailObjectDistance = 350;
			terrain.detailObjectDensity = 1;
			terrain.treeDistance = 700;
			terrain.treeBillboardDistance = 400;
			terrain.treeCrossFadeLength = 200;
			break;
		
		default:
			break;
		}
	}
	public Terrain SetTerrain ()
	{
		Terrain[] terr = Terrain.activeTerrains;
		for (int i = 0; i < terr.Length; i++) {
			if (terr [i].tag == "Teren") {
				return terr[i];
			}

		}
		return null;
	}
}
