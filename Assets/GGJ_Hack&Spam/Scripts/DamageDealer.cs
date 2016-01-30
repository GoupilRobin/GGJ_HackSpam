using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
	
	public	int	damage;
	public	bool	hero;
	public	bool	other;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (hero && coll.gameObject.GetComponent<Monster>()) {
			coll.gameObject.SendMessage("OnDamaged", damage);
		}
		else if (other && coll.gameObject.GetComponent<Player>()) {
			coll.gameObject.SendMessage("OnDamaged", damage);
		}
	}
}
