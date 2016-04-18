using UnityEngine;
using System.Collections;

public class DestroyOfParticlesScript : MonoBehaviour {

	private GameObject partObj;
	public ParticleSystem [] partSys = new ParticleSystem[1];
	// Use this for initialization
	void Start () {
		partObj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(partPlay() == false)
		{
			Destroy(partObj);
		}
	}
	private bool partPlay()
	{
		for (int i = 0; i < partSys.Length; i++)
		{
			if(partSys[i].isPlaying == true)
			{
				return true;
				break;
			}
		}
		return false;
	}
}
