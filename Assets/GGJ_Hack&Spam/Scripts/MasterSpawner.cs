using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterSpawner : MonoBehaviour {

	public	List<IA>	monsters = null;
	private	MobSpawner[]	spawners;

	// Use this for initialization
	void Awake () {
		spawners = this.GetComponentsInChildren<MobSpawner> ();
		if (monsters.Count > 0 && monsters != null)
			InvokeRepeating ("Spawn", 1, 1);
	}
	
	void	Spawn(){
		if (monsters.Count > 0 && monsters != null) {
			IA current = monsters [0];
			monsters.RemoveAt (0);
			int rand = Random.Range (0, spawners.GetLength (0));
			int i = 0;
			foreach (MobSpawner spawner in spawners) {
				if (i == rand) {
					spawner.SendMessage ("InstantiateIA", current);
					break;
				}
				i++;
			}
		}
		if (monsters.Count <= 0 || monsters == null)
			CancelInvoke ();
	}

	// Update is called once per frame
	void Update () {

	}
}
