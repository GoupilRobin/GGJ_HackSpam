using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class WordComparator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<string> Comparator(string chat, List<string> list)
	{
		List<string> listRes = new List<string>();
		StringBuilder res = new StringBuilder();
		char[] ar = chat.ToCharArray();

		foreach (string elem in list) {
			res.Remove(0, res.Length);
			for (int i = 0; i < ar.Length; ++i) {
				if (chat.Length > i + 1 && ar[i].Equals(elem[i]))
					res.Append(ar[i]);
				else
					break;
			}
			listRes.Add(res.ToString());
		}
		return (listRes);
	}
}
