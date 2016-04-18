using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CloseButtonDesertScript : MonoBehaviour {

	public Button btnClose;
	public Image message;
	public AudioClip clickSound;
	public AudioSource soundSource;
	public GameObject obj;
	VolumeAndMusicScript vms;

	// Use this for initialization
	void Start () {

		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));
		btnClose = btnClose.GetComponent<Button> ();
		message = message.GetComponent<Image> ();
	
	}
	
	public void CloseButton (){
		MissionDesertScript mds = obj.GetComponent<MissionDesertScript> ();
		if (message.enabled == true) {
			message.enabled = false;
			mds.y++;
			if (soundSource != null) {
				soundSource.PlayOneShot (clickSound);
			}
			if(vms.isMsg == true)
				vms.isMsg = false;
			Time.timeScale = 1;
		}
	}
}
