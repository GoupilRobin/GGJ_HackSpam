using UnityEngine;
using System.Collections;

public class IAFireBall : IA {

	public void Launch(bool reversed)
	{
		Debug.Log (_body);
		if (!reversed)
			_body.AddRelativeForce (new Vector2 (5, 0), ForceMode2D.Impulse);
		else
			_body.AddRelativeForce (new Vector2 (-5, 0), ForceMode2D.Impulse);
	}

	public void GoForIt()
	{
		_body.AddRelativeForce (new Vector2 (0, -5), ForceMode2D.Impulse);
	}
}
