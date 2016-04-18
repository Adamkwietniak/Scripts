using UnityEngine;
using System.Collections;

public class MenuInstanceScript : MonoBehaviour {

	public static MenuInstanceScript instance;

	public static bool respawn;

	public static string respawnPlace;

	void Awake (){

		if (!instance) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

}
