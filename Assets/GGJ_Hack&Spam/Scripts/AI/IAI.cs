using UnityEngine;
using System.Collections;

public abstract class IA : MonoBehaviour 
{
	protected Player _player;
	protected Rigidbody2D _body;
	public float distance;
	public int hitpoint;
	protected bool reversed = false;


	public void Awake()
	{
		_player = FindObjectOfType<Player> ();
		_body = GetComponent <Rigidbody2D>();
	}

	public void OnDamaged(int damage)
	{
			hitpoint -= damage;
			if (hitpoint <= 0)
				Destroy (this.gameObject);
	}

	public void Flip()
	{
		reversed = !reversed;
		Vector3 s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}
}
