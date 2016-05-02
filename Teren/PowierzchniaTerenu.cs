using UnityEngine;
using System.Collections;

public class PowierzchniaTerenu : MonoBehaviour {
    

	private static float[] PobierzMixTextur (Vector3 pozycjaGracza, Terrain terrain, TerrainData terrainData)
	{
		
		Vector3 terrainPos = terrain.transform.position;

		int mapX = (int)(((pozycjaGracza.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
		int mapZ = (int)(((pozycjaGracza.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

		float[,,] splatmapData = terrainData.GetAlphamaps (mapX, mapZ, 1, 1);
		float [] cellMix = new float[splatmapData.GetUpperBound (2) + 1];

		for (int i = 0; i < cellMix.Length; ++i) {
			cellMix[i] = splatmapData[0,0,i];
		}

		return cellMix;
	}
	public static string NazwaTeksturyWPozycji (Vector3 pozycjaGracza, Terrain terrain, TerrainData terrainData)
	{
		//Debug.Log (terrain + " " + terrainData);
		float [] mix = PobierzMixTextur (pozycjaGracza, terrain, terrainData);
		float maxMix = 0;
		int maxIndex = 0;

		for (int i=0; i<mix.Length; ++i) {
			if(mix[i] > maxMix)
			{
				maxIndex = i;
				maxMix = mix[i];
			}
		}
		SplatPrototype [] sp = terrainData.splatPrototypes;
		return sp [maxIndex].texture.name;
	}
}
