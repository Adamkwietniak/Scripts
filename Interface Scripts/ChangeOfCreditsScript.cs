using UnityEngine;
using System.Collections;

public class ChangeOfCreditsScript : MonoBehaviour {

	ChangesOfGraphicScript cogs;
	private int actualSetG = 0;
	// Use this for initialization
	void Awake ()
	{
		cogs = (ChangesOfGraphicScript)FindObjectOfType (typeof(ChangesOfGraphicScript)) as ChangesOfGraphicScript;
		cogs.terrain = cogs.SetTerrain ();
	}
	void Start () {
		actualSetG = GraphicsScript.qualityLevel;
		cogs.SetNewParameters (actualSetG);
		//Debug.Log ("Ustawilem wsio");
	}
}
