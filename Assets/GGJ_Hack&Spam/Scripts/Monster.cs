using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public	int hitpoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public	void	OnDamaged(int damage){
		hitpoint -= damage;
		if (hitpoint <= 0)
			Destroy (this.gameObject);
	}
}
