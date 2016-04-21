using UnityEngine;
using UnityEngine.UI;

public class LanguageHelperCityScript : MonoBehaviour {

	MissionCityScript mcs;
	AllianceCityScript acs;
	// Use this for initialization
	void Awake ()
	{
		mcs = (MissionCityScript)FindObjectOfType (typeof(MissionCityScript)) as MissionCityScript;
		acs = (AllianceCityScript)FindObjectOfType (typeof(AllianceCityScript)) as AllianceCityScript;
	}
	public void SetNewString (string [] temp)
	{
		//Debug.Log ("Przeslalem: " + temp [1]); 
		if(temp[1] == "keepTextFromMissionCity")
			mcs.keepTextFromMissionCity = temp[0];
		else if(temp[1] == "keepTextAllianceCityScript")
			acs.keepTextAllianceCityScript = temp[0];
		else if(temp[1] == "cont1")
			acs.cont1 = temp[0];
		else if(temp[1] == "cont2")
			acs.cont2 = temp[0];
		else if(temp[1] == "cont3")
			acs.cont3 = temp[0];
	}
}
