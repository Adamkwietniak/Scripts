using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CloseButtonCityScript : MonoBehaviour {

	private Button btnClose;
	public Image message;
	public AudioClip clickSound;
	public AudioSource soundSource;
	private GameObject obj;
	VolumeAndMusicScript vms;
	// Use this for initialization
	MissionCityScript m2fs;
	void Start () {
		obj = GameObject.Find ("BrumBrume");
		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));
		btnClose = GetComponent<Button> ();
		m2fs = obj.GetComponent<MissionCityScript> ();
		//btnClose = btnClose.GetComponent<Button> ();
		message = message.GetComponent<Image> ();
		soundSource = soundSource.GetComponent<AudioSource>();
		m2fs = (MissionCityScript)FindObjectOfType (typeof(MissionCityScript)) as MissionCityScript;
	}
	
	public void CloseButton (){
		
		Podmien ();
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
		//if(vms.isMsg == true)
		//	vms.isMsg = false;
		Time.timeScale = 1;
	}
	private void Podmien ()
	{
		/*if (Application.loadedLevel == 1) {
		
			MissionsScript ms = obj.GetComponent<MissionsScript> ();
			if (message.enabled == true) {
				message.enabled = false;
				ms.y++;
			}
		}
		if (Application.loadedLevel == 2) {
		
			MissionForestScript mfs = obj.GetComponent<MissionForestScript> ();
			if (message.enabled == true) {
				message.enabled = false;
				mfs.y++;
			}
		}*/
		//if (Application.loadedLevel == 3) {
		
		//MissionRiverScript m2fs = obj.GetComponent<MissionRiverScript> ();
		if (message.enabled == true) {
			message.enabled = false;
			m2fs.DisableEnableMsg ();

			//Time.timeScale = 1;
		}
		//}
	}
}
