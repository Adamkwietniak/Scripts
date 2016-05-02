using UnityEngine;
using System.Collections;

public class ChangeResolutionScript : MonoBehaviour {

	MenuScript ms;
	public static int resolution = 2;
	void Awake ()
	{
		ms = (MenuScript)FindObjectOfType (typeof(MenuScript)) as MenuScript;

	}
	public void ChangeRes (int i)
	{
		switch (i)
		{
		case 0:
			Screen.SetResolution (640, 480, isWindowed ());
			resolution = 0;
			break;
		case 1:
			Screen.SetResolution (800, 600, isWindowed ());
			resolution = 1;
			break;

		case 2:
			Screen.SetResolution (1366, 768, isWindowed ());
			resolution = 2;
			break;

		case 3:
			Screen.SetResolution (1600, 900, isWindowed ());
			resolution = 3;
			break;

		case 4:
			Screen.SetResolution (1920, 1080, isWindowed ());
			resolution = 4;
			break;

		default:
			break;
		}
	}
	private bool isWindowed ()
	{
		return ms.isFullScreen;
		//return true;
	}
	public void ChangeWindow ()
	{
		ms.isFullScreen = !ms.isFullScreen;
		ChangeRes (resolution);
	}
}
