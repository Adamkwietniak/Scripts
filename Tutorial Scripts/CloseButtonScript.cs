using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CloseButtonScript : MonoBehaviour {

	public Button btnClose;
	public Image message;
	public AudioClip clickSound;
	public AudioSource soundSource;
	public GameObject obj;
	VolumeAndMusicScript vms;
	// Use this for initialization
	MissionsScript m2fs;
	void Start () {
		vms = (VolumeAndMusicScript)FindObjectOfType(typeof(VolumeAndMusicScript));
		m2fs = obj.GetComponent<MissionsScript> ();
		btnClose = btnClose.GetComponent<Button> ();
		message = message.GetComponent<Image> ();
		m2fs = (MissionsScript)FindObjectOfType (typeof(MissionsScript)) as MissionsScript;
	}

	public void CloseButton (){

		Podmien ();
		if (soundSource != null) {
			soundSource.PlayOneShot (clickSound);
		}
		if(vms.isMsg == true)
			vms.isMsg = false;
		Time.timeScale = 1;
	}
	private void Podmien ()
	{
		if (message.enabled == true) {
			message.enabled = false;
			m2fs.DisableEnableMsg ();

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