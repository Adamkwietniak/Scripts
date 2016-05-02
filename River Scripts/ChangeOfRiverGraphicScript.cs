using UnityEngine;
using System.Collections;

public class ChangeOfRiverGraphicScript : MonoBehaviour {

	ChangesOfGraphicScript cogs;
	private Light directLight;
	private int actualSetG = 0;
	WaterBase wb;
	SpecularLighting sl;
	PlanarReflection pr;
	GerstnerDisplace gd;
	void Awake ()
	{
		cogs = (ChangesOfGraphicScript)FindObjectOfType (typeof(ChangesOfGraphicScript)) as ChangesOfGraphicScript;
		cogs.terrain = cogs.SetTerrain ();
		directLight = GameObject.Find ("Directional Light").GetComponent<Light> ();
		wb = (WaterBase)FindObjectOfType (typeof(WaterBase)) as WaterBase;
		sl = (SpecularLighting)FindObjectOfType (typeof(SpecularLighting)) as SpecularLighting;
		pr = (PlanarReflection)FindObjectOfType (typeof(PlanarReflection)) as PlanarReflection;
		gd = (GerstnerDisplace)FindObjectOfType (typeof(GerstnerDisplace)) as GerstnerDisplace;
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
			wb.waterQuality = WaterQuality.Low;
			wb.edgeBlend = false;
			sl.enabled = false;
			pr.enabled = false;
			gd.enabled = false;
			break;
		case 1:						//Fast
			directLight.shadows = LightShadows.None;
			directLight.shadowStrength = 0;
			wb.waterQuality = WaterQuality.Low;
			wb.edgeBlend = false;
			sl.enabled = true;
			pr.enabled = false;
			gd.enabled = false;
			RenderSettings.fog = false;
			break;
		case 2:						//Simple
			directLight.shadows = LightShadows.Hard;
			directLight.shadowStrength = 1;
			RenderSettings.fog = false;
			wb.waterQuality = WaterQuality.Medium;
			wb.edgeBlend = false;
			sl.enabled = true;
			pr.enabled = false;
			gd.enabled = false;
			break;
		case 3:						//Good
			directLight.shadows = LightShadows.Hard;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			wb.waterQuality = WaterQuality.Medium;
			wb.edgeBlend = true;
			sl.enabled = true;
			pr.enabled = false;
			gd.enabled = false;
			break;
		case 4:						//Beautyfull
			directLight.shadows = LightShadows.Soft;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			wb.waterQuality = WaterQuality.High;
			wb.edgeBlend = true;
			sl.enabled = true;
			pr.enabled = false;
			gd.enabled = true;
			break;
		case 5:						//Fantastic
			directLight.shadows = LightShadows.Soft;
			directLight.shadowStrength = 1;
			RenderSettings.fog = true;
			wb.waterQuality = WaterQuality.High;
			wb.edgeBlend = true;
			sl.enabled = true;
			pr.enabled = true;
			pr.reflectSkybox = true;
			gd.enabled = true;
			break;

		default:
			break;
		}
	}
}
