using UnityEngine;
using System.Collections;

public class IAGargoyle : IAFlying {
	
	protected Transform spawn;
	
	public void Start () 
	{
		base.Start ();
		spawn = transform.Find ("Spawn");
		InvokeRepeating ("Move", 0, 0.5f);
	}
	
	public void Move()
	{
		base.Move ();
		if (transform.position.x <= _player.transform.position.x + 1
			&& transform.position.x >= _player.transform.position.x - 1) {
			Attack ();
		}
	}
	
	public void Attack()
	{
		GameObject g = Resources.Load ("Prefabs/FireBall", typeof(GameObject)) as GameObject;
		GameObject s = Instantiate(g, spawn.position, spawn.rotation) as GameObject;
		s.GetComponent<IAFireBall> ().GoForIt ();
	}
}
