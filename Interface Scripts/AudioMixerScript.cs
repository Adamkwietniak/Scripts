using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class AudioMixerScript : MonoBehaviour
{

	public AudioMixer masterMixer;
	public GameObject brumBrum;
	public GameObject sliderr;
	RCCCarControllerV2 rc;
	private const int maxSounds = 8;
	private float[] carsVolume = new float[8];
	private float[] tempVolume;
	private float[] maxVolume;
	private Slider sl;

	void Start ()
	{
		//if (brumBrum == null) {
		brumBrum = GameObject.Find ("BrumBrume");
		rc = brumBrum.GetComponent<RCCCarControllerV2> ();
		//Debug.Log("Zaladowano bruma");
		//}
		//if (sliderr == null) {
		sliderr = GameObject.Find ("SoundSlider");
		sl = sliderr.GetComponentInChildren<Slider> ();
		//Debug.Log("Zaladowano slider");
		//}
		tempVolume = new float[maxSounds];
		maxVolume = new float[maxSounds];
		if (brumBrum != null) {
			SetDefault ();
		}
		for (int i = 0; i < maxSounds; i++) {
			maxVolume [i] = carsVolume [i];
		}
	}

	void Update ()
	{
		if (brumBrum == null) {
			brumBrum = GameObject.Find ("BrumBrume");
			rc = brumBrum.GetComponent<RCCCarControllerV2> ();
			SetDefault ();
			//Debug.Log("Zaladowano bruma");
		}
	}

	private void SetDefault ()
	{
		carsVolume [0] = rc.minEngineSoundPitch;
		carsVolume [1] = rc.maxEngineSoundPitch;
		carsVolume [2] = rc.minEngineSoundVolume;
		carsVolume [3] = rc.maxEngineSoundVolume;
		carsVolume [4] = rc.maxGearShiftingSoundVolume;
		carsVolume [5] = rc.maxCrashSoundVolume;
		carsVolume [6] = rc.maxWindSoundVolume;
		carsVolume [7] = rc.maxBrakeSoundVolume;

	}

	public void setSoundsVolume (float sfxLvl)
	{ // tu zmieniam dzwieki gry

		masterMixer.SetFloat ("SoundsVolume", sfxLvl); //sfxLvl zakres (-80 , 20) slider.
	}

	public void setMusicVolume (float musicLvl)
	{ // tu zmieniam opvje muzyki w grze

		masterMixer.SetFloat ("MusicVolume", musicLvl);
	}

	public void MuteCarSound ()
	{
		for (int i = 0; i < maxSounds; i++) {
			if (carsVolume [i] != 0)
				tempVolume [i] = carsVolume [i];
			carsVolume [i] = 0;
		}
		AssignSoundValue ();
	}

	public void UnMuteCarSound ()
	{
		for (int i = 0; i < maxSounds; i++) {
			carsVolume [i] = tempVolume [i];
			tempVolume [i] = 0;
		}
		AssignSoundValue ();
	}

	private void AssignSoundValue ()
	{
		rc.minEngineSoundPitch = carsVolume [0];
		rc.maxEngineSoundPitch = carsVolume [1];
		rc.minEngineSoundVolume = carsVolume [2];
		rc.maxEngineSoundVolume = carsVolume [3];
		rc.maxGearShiftingSoundVolume = carsVolume [4];
		rc.maxCrashSoundVolume = carsVolume [5];
		rc.maxWindSoundVolume = carsVolume [6];
		rc.maxBrakeSoundVolume = carsVolume [7];
		//Debug.Log ("Podmieniam volume bruma");
	}

	public void WriteCarSoundsFx ()
	{
		if (brumBrum != null && sliderr != null) {
			//Debug.Log("Wartosc poszczegolnych volume to: ");
			for (int i = 0; i < maxSounds; i++) {
				carsVolume [i] = (maxVolume [i] * ((sl.value + 80) / 100));
				//Debug.Log("Cars Volume["+i+"] wynosi: "+carsVolume [i]);
			}

			AssignSoundValue ();
		}
		//Debug.Log ("przypisana zostala wartosc");
	}
}

	


