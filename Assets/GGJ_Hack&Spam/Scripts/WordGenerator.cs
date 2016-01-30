using UnityEngine;
using System.Collections;

public class WordGenerator : MonoBehaviour {

	private	static	WordGenerator	Singleton = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake() {
		if (!Singleton)
			Singleton = this;
		else
			DestroyImmediate (this);
	}

	void OnDestroy() {
		if (this == Singleton)
			Singleton = null;
	}

	public static WordGenerator	Fetch(){
		return (Singleton);
	}

	public string Generator(int numLetter){
		string res = "";
		string alpha = "abcdefghijklmnopqrstuvwxyz";
		int rand = 0;
		for (int i = 0; i< numLetter; i++) {
			rand = (int)(Random.value * 25);
			res += alpha[rand];
		}
		Debug.Log (res);
		return (res);
	}
}
