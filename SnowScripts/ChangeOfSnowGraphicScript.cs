using UnityEngine;
using System.Collections;

public class ChangeOfSnowGraphicScript : MonoBehaviour {

	ChangesOfGraphicScript cogs;
	private Light directLight;
	private int actualSetG = 0;
	void Awake ()
	{
		cogs = (ChangesOfGraphicScript)FindObjectOfType(typeof(ChangesOfGraphicScript)) as ChangesOfGraphicScript;
		cogs.terrain = cogs.SetTerrain ();
		directLight = GameObject.Find ("Directional light").GetComponent<Light> ();
	}
	void Start () {
		actualSetG = GraphicsScript.qualityLevel;
		cogs.SetNewParameters (actualSetG);
		ChangeParamsOnTutorial (actualSetG);
	}

	// Update is called once per frame
	void Update () {
		if (actualSetG != GraphicsScript.qualityLevel) {
			actualSetG = GraphicsScript.qualityLevel;
			cogs.SetNewParameters (actualSetG);
			ChangeParamsOnTutorial (actualSetG);
		}
	}

	private void ChangeParamsOnTutorial (int i)
	{
		switch (i) {
		case 0:						//Fastest
			directLight.shadows = LightShadows.None;
			directLight.shadowStrength = 0;
			RenderSettings.fog = false;
			break;
		case 1:						//Fast
			directLight.shadows = LightShadows.None;
			directLight.shadowStrength = 0;
			RenderSettings.fog = false;
			break;
		case 2:						//Simple
			directLight.shadows = LightShadows.Hard;
			directLight.shadowStrength = 1;
			RenderSettings.fog = false;
			break;
		case 3:						//Good
			directLight.shadows = LightShadows.Hard;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			break;
		case 4:						//Beautyfull
			directLight.shadows = LightShadows.Soft;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			break;
		case 5:						//Fantastic
			directLight.shadows = LightShadows.Soft;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			break;

		default:
			break;
		}
	}
}
