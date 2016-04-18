using UnityEngine;
using UnityEngine.UI;

public class LanguageHelperRiverScript : MonoBehaviour {

	GetOutFromWayScript gw;
	// Use this for initialization
	void Awake ()
	{
		gw = (GetOutFromWayScript)FindObjectOfType (typeof(GetOutFromWayScript)) as GetOutFromWayScript;
	}
	public void SetNewString (string[] tekst)
	{
		if(tekst[1] == "keepTextLanguageGetOutFromWay")
			gw.keepTextLanguageGetOutFromWay = tekst[0];
	}
}

