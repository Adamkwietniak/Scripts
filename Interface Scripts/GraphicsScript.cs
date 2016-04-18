using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicsScript : MonoBehaviour {

	public static int qualityLevel;





	public void Button0 (bool setGraphic){
	
		QualitySettings.currentLevel = QualityLevel.Fastest;
	}
	public void Button1 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fast;
	}
	public void Button2 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Simple;
	}
	public void Button3 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Good;
	}
	public void Button4 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Beautiful;
	}

	public void Button5 (bool setGraphic){

		QualitySettings.currentLevel = QualityLevel.Fantastic;
	}
}
