﻿using UnityEngine;
using System.Collections;

public abstract class IAWalking : IA {
	
	public float max = 10;
	protected float currentPos;
	protected Vector2 startPos;
	public int speed;


	public void Start()
	{
		base.Start ();
		startPos = new Vector2 (transform.position.x, transform.position.y);
		currentPos = 0;
	}

	public void FixedUpdate()
	{
		currentPos = transform.position.x - startPos.x;
	}
	
	public void Move()
	{
		Debug.Log (currentPos);
		if (currentPos > max && !reversed) 
		{
			Flip ();
		}
		else if (currentPos < 0 && reversed)
			Flip ();
		if (!reversed)
			_body.AddForce (new Vector2 (speed, 0f));
		else
			_body.AddForce (new Vector2 (-speed, 0f));
	}
}
