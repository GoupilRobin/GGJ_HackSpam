using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateIA(IA toSpawn){
		Instantiate (toSpawn, this.transform.position, this.transform.rotation);
	}
}
