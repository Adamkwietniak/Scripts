using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CloseButtonForestScript : MonoBehaviour {
	
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
		MissionForestScript ms = obj.GetComponent<MissionForestScript> ();
		if (message.enabled == true) {
			message.enabled = false;
			ms.y++;
			if (soundSource != null) {
				soundSource.PlayOneShot (clickSound);
			}
			if(vms.isMsg == true)
				vms.isMsg = false;
			Time.timeScale = 1;
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