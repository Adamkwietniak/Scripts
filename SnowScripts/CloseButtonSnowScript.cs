using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CloseButtonSnowScript : MonoBehaviour {

	private Button btnClose;
	public Image message;
	public AudioClip clickSound;
	public AudioSource soundSource;
	private GameObject obj;
	VolumeAndMusicScript vms;
	// Use this for initialization
	MissionsSnowScript m2fs;
	void Start () {
		obj = GameObject.Find ("BrumBrume");
		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));
		m2fs = obj.GetComponent<MissionsSnowScript> ();
		btnClose = GetComponent<Button> ();
		message = message.GetComponent<Image> ();
	}

	public void CloseButton (){

		if (message.enabled == true) {
			message.enabled = false;
			m2fs.DisableEnableMsg ();
			if (soundSource != null) {
				soundSource.PlayOneShot (clickSound);
			}
			if (vms.isMsg == true)
				vms.isMsg = false;
		}
	}
}
/* Działanie skryptu
 * Skrypt ma za zadanie obsługiwać zdarzenie kliknięcia na Close podczas wyświetlania informacji w poszczególnych
 * etapach misji.
 * Istotnym elementem do poprawnego działania skryptu jest podpięcie obiektu w którym zawarty jesy skrypt
 * MissionScript (w naszym przypadku jest to BrumBrum(1)).
 * Dzięki podcpięciu skrypt ten po kliknięciu na Close zwiększa wartość zmiennej pomocnicznej w MissionScript
 * odpowiedzialnej za wyświetlenie odpowiedniego komunikatu na ekranie.
 * */