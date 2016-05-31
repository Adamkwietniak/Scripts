using UnityEngine;
using System.Collections;

public class FrameRateLimitScript : MonoBehaviour {

	public int frameRateLimit = 60;
	private int actualInt = 0;
	void Start () {
		actualInt = GraphicsScript.qualityLevel;
		StartCoroutine (FunctionLimit());
	}

	void LateUpdate ()
	{
		if (actualInt != GraphicsScript.qualityLevel) {
			ChangeVSync ();
			actualInt = GraphicsScript.qualityLevel;
		}
	}
	IEnumerator FunctionLimit ()
	{
		yield return new WaitForSeconds (1);
		Application.targetFrameRate = frameRateLimit;
	}
	private void ChangeVSync ()
	{
		if (GraphicsScript.qualityLevel < 3) {
			QualitySettings.vSyncCount = 0;
		} else if (GraphicsScript.qualityLevel > 2 && GraphicsScript.qualityLevel <= 4) {
			QualitySettings.vSyncCount = 1;
		} else {
			QualitySettings.vSyncCount = 2;
		}

	}
}
