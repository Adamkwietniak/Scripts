using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicsScript : MonoBehaviour {

	public static int qualityLevel;
	ChangesOfGraphicScript cogs;

	void Awake ()
	{
		cogs = (ChangesOfGraphicScript)FindObjectOfType (typeof(ChangesOfGraphicScript)) as ChangesOfGraphicScript;
	}


	public void Button0 (bool setGraphic){
	
		QualitySettings.currentLevel = QualityLevel.Fastest;
		qualityLevel = 0;
		cogs.SetNewParameters (0);
	}
	public void Button1 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fast;
		qualityLevel = 1;
		cogs.SetNewParameters (1);
	}
	public void Button2 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Simple;
		qualityLevel = 2;
		cogs.SetNewParameters (2);
	}
	public void Button3 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Good;
		qualityLevel = 3;
		cogs.SetNewParameters (3);
	}
	public void Button4 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Beautiful;
		qualityLevel = 4;
		cogs.SetNewParameters (4);
	}

	public void Button5 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fantastic;
		qualityLevel = 5;
		cogs.SetNewParameters (5);
	}
}
