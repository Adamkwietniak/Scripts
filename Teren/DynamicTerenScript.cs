using UnityEngine;
using System.Collections;

public class DynamicTerenScript : MonoBehaviour {


	//public int texDef = 1;


	private Terrain terrain;
	private TerrainData terrainData;
	private float[,] heightMapBackup;
	private float[, ,] alphaMapBackup;

	private Vector3 actualTerrainPos;
	private int heightMapHoleWidth;
	private int heightMapHoleLength;
	private int heightMapStartPosX;
	private int heightMapStartPosZ;
	private float deformationDepth;
	private float[,] heights;
	private float distanceFromCenter;
	private float depthMultiplier;

	private Vector3 alphaMapTerrainPos;
	private int alphaMapHoleWidth;
	private int alphaMapHoleLength;
	private int alphaMapStartPosX;
	private int alphaMapStartPosZ;
	private float tcirclePosX;
	private float tcirclePosY;
	private float tdistanceFromCenter;
	private float[, ,] alphas;
	

	protected int highMWidth;
	protected int highMHeight;
	protected int alphaMapWidth;
	protected int alphaMapHeight;
	protected int numOfAlphaLayers;
	protected const float depthConvert = 0.05F;
	protected const float textureSizeMult = 1.25F;

	// Use this for initialization
	void Start () {

		heightMapHoleWidth = 0;
		heightMapHoleLength = 0;
		heightMapStartPosX = 0;
		heightMapStartPosZ = 0;
		deformationDepth = 0F;
		distanceFromCenter = 0F;
		depthMultiplier = 0F;

		terrain = this.GetComponent<Terrain> ();
		terrainData = terrain.terrainData;

		alphaMapHoleWidth = 0;
		alphaMapHoleLength = 0;
		alphaMapStartPosX = 0;
		alphaMapStartPosZ = 0;
		tcirclePosX = 0F;
		tcirclePosY = 0F;
		tdistanceFromCenter = 0F;

		highMWidth = terrainData.heightmapWidth;
		highMHeight = terrainData.heightmapHeight;
		alphaMapWidth = terrainData.alphamapWidth;
		alphaMapHeight = terrainData.alphamapHeight;
		numOfAlphaLayers = terrainData.alphamapLayers;

		if (Debug.isDebugBuild) {
			heightMapBackup = terrainData.GetHeights(0, 0, highMWidth, highMHeight);
			alphaMapBackup = terrainData.GetAlphamaps(0, 0, alphaMapWidth, alphaMapHeight);
		}
	}

	void OnApplicationQuit()
	{
		if (Debug.isDebugBuild)
		{
			terrainData.SetHeights(0, 0, heightMapBackup);
			terrainData.SetAlphamaps(0, 0, alphaMapBackup);
		}
	}
	//Metoda wywoływana ze skryptu rozpoczynająca ciąg zdarzen majacych na celu zdeformowanie terenu
	//po którym porusza się nasz pojazd
	public void ShapeArea(Vector3 tirePos, float size, float multOfDef, int indi, bool checkTex)
	{

		DeformingTerrain(tirePos, size, multOfDef);
		if(checkTex == true)
			TexturingDeformation(tirePos, size*1.1f, indi);
	}
	//Metoda odpowiada za deformowanie terenu
	protected void DeformingTerrain(Vector3 tirePos, float size, float multOfDef)
	{
		actualTerrainPos = GetTerrainPositionWithUseTirePos (tirePos, terrain, alphaMapWidth, alphaMapHeight);
		heightMapHoleWidth = (int)(size * (highMWidth / terrainData.size.x));
		heightMapHoleLength = (int)(size * (highMHeight / terrainData.size.z));
		heightMapStartPosX = (int)(actualTerrainPos.x - (heightMapHoleWidth / 2));
		heightMapStartPosZ = (int)(actualTerrainPos.z - (heightMapHoleLength / 2));
		//Debug.Log ("Obliczono parametry");
		heights = terrainData.GetHeights(heightMapStartPosX, heightMapStartPosZ, heightMapHoleWidth, heightMapHoleLength);

		for (int i = 0; i < heightMapHoleLength; i++) //width
		{
			for (int j = 0; j < heightMapHoleWidth; j++) //height
			{

				if(heights[i, j] == heightMapBackup[i,j]){
					heights[i, j] -= multOfDef/10000;
					//Debug.Log("Obliczono parametry deformacji");
				}
				
			}
		}
		
		// set the new height
		terrainData.SetHeights(heightMapStartPosX, heightMapStartPosZ, heights);
		//Debug.Log ("Nastapila deformacja");
	}
	protected void TexturingDeformation (Vector3 tirePos, float size, int ind)
	{
		alphaMapTerrainPos = GetTerrainPositionWithUseTirePos(tirePos, terrain, alphaMapWidth, alphaMapHeight);
		alphaMapHoleWidth = (int)(size * (alphaMapWidth / terrainData.size.x));
		alphaMapHoleLength = (int)(size * (alphaMapHeight / terrainData.size.z));
		
		alphaMapStartPosX = (int)(alphaMapTerrainPos.x - (alphaMapHoleWidth / 2));
		alphaMapStartPosZ = (int)(alphaMapTerrainPos.z - (alphaMapHoleLength/2));
		
		alphas = terrainData.GetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphaMapHoleWidth, alphaMapHoleLength);
		

		
		for (int i = 0; i < alphaMapHoleLength; i++) //width
		{
			for (int j = 0; j < alphaMapHoleWidth; j++) //height
			{
				//tcirclePosX = (j - (alphaMapHoleWidth / 2)) / (alphaMapWidth / terrainData.size.x);
				//tcirclePosY = (i - (alphaMapHoleLength / 2)) / (alphaMapHeight / terrainData.size.z);
				
				//convert back to values without skew
				//tdistanceFromCenter = Mathf.Abs(Mathf.Sqrt(tcirclePosX * tcirclePosX + tcirclePosY * tcirclePosY));
				
				
				//if (tdistanceFromCenter < (size / 2.0f))
				//{
					for (int layerCount = 0; layerCount < numOfAlphaLayers; layerCount++)
					{
						//could add blending here in the future
						if (layerCount == ind)
						{
							alphas[i, j, layerCount] = 1;
						}
						else
						{
							alphas[i, j, layerCount] = 0;
						}
					}
				//}
			}
		} 
		
		terrainData.SetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphas);
	}
	protected Vector3 GetTerrainPositionWithUseTirePos(Vector3 pos,Terrain terrain, int mapWidth, int mapHeight)
	{
		Vector3 coord = GetNormalizeTerrainPositionWithUseTirePos(pos, terrain);
		// get the position of the terrain heightmap where this game object is
		return new Vector3((coord.x * mapWidth), 0, (coord.z * mapHeight));
	}     
	protected Vector3 GetNormalizeTerrainPositionWithUseTirePos(Vector3 pos, Terrain terrain)
	{
		// get the normalized position of this game object relative to the terrain
		Vector3 tempCoord = (pos - terrain.gameObject.transform.position);
		Vector3 coord;
		coord.x = tempCoord.x / terrainData.size.x;
		coord.y = tempCoord.y / terrainData.size.y;
		coord.z = tempCoord.z / terrainData.size.z;
		
		return coord;
	}
	

}
