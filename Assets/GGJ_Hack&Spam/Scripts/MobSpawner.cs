using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {

	public	AudioClip	onCreate;
	private	AudioSource	_audio;

	void Start(){
		_audio = GetComponent<AudioSource> ();
	}

	void InstantiateIA(IA toSpawn){
		_audio.PlayOneShot (onCreate);
		Instantiate (toSpawn, this.transform.position, this.transform.rotation);
	}
}
