using UnityEngine;
using System.Collections;

public class IAZombie : IAWalking {

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		InvokeRepeating ("Move", 0, 0.5f);
	}

	public void Move()
	{
		Debug.Log (currentPos);
		Debug.Log (_player.transform.position.x - startPos.x > 0 && _player.transform.position.x - startPos.x + max < 0);
		Debug.Log (reversed);
		if (currentPos > max && !reversed) 
			Flip ();
		else if (currentPos < 0 && reversed)
			Flip ();
		if (_player.transform.position.y - transform.position.y < 1 && _player.transform.position.y - transform.position.y > -1) 
		{
			if (_player.transform.position.x > startPos.x && _player.transform.position.x < startPos.x + max)
			{
				if(_player.transform.position.x < transform.position.x  && !reversed)
					Flip ();
				else if(_player.transform.position.x > transform.position.x && reversed)
					Flip ();
			}
		}
		if (!reversed)
			_body.AddForce (new Vector2 (speed, 0f));
		else
			_body.AddForce (new Vector2 (-speed, 0f));
	}
}
