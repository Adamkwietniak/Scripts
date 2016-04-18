using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicsScript : MonoBehaviour {

	public static int qualityLevel;





	public void Button0 (bool setGraphic){
	
		QualitySettings.currentLevel = QualityLevel.Fastest;
		qualityLevel = 0;
	}
	public void Button1 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fast;
		qualityLevel = 1;
	}
	public void Button2 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Simple;
		qualityLevel = 2;
	}
	public void Button3 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Good;
		qualityLevel = 3;
	}
	public void Button4 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Beautiful;
		qualityLevel = 4;
	}

	public void Button5 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fantastic;
		qualityLevel = 5;
	}
}
