using UnityEngine;
using System.Collections;

public class CreditsMovingScript : MonoBehaviour {
	
	//public GameObject allMoving;
	private Transform tr;
	private float y0 = 60;
	public string backToMenu;
	//public string backToMenu;
	//[HideInInspector]public bool tempBoolCredits = false;
	// Use this for initialization
	void Start () {
		tr = this.GetComponent<Transform> ();
		Time.timeScale = 1f;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		y0 += Time.deltaTime*40.0f;
		tr.position = new Vector3(699, y0, -10);
		//Debug.Log (tr.position.y);
		
		if (Input.GetKeyUp (KeyCode.Escape)) {
			
			Application.LoadLevel (backToMenu);
			//tempBoolCredits = true;
		}
		
		
	}
}
