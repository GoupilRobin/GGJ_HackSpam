using UnityEngine;
using System.Collections;

public class HeroCharacter : MonoBehaviour {

	public	int hitpoint;
	public	int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		WordGenerator current = WordGenerator.Fetch();
		if (Input.GetButtonDown("Fire1")){
		    current.Generator(5);
		}
	}

	public	void	OnDamaged(int damage){
		hitpoint -= damage;
	}
}
