using UnityEngine;
using System.Collections;

public abstract class IA : MonoBehaviour 
{
	protected Player _player;
	protected Rigidbody2D _body;
	public float distance;
	public int hitpoint;
	public float invincibilityTime = 1.0f;
	public bool invincible;
	protected bool reversed = false;
	private TextMesh _name;


	public void Awake()
	{
		_player = FindObjectOfType<Player> ();
		_body = GetComponent <Rigidbody2D>();
		if(_name = GetComponentInChildren<TextMesh>())
			_name.text = "";
		invincible = false;
	}

	public void OnDamaged(int damage)
	{
		if (!invincible)
		{
			hitpoint -= damage;
			if (hitpoint <= 0) {
				Player.MobKilled += 1;
				Destroy (this.gameObject);
			}
			else
			{
				StartCoroutine(coroutine_Invincibility());
			}
		}
	}

	public void Flip()
	{
		reversed = !reversed;
		Vector3 s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void SetName(string name)
	{
		_name.text = name;
	}
	
	private IEnumerator coroutine_Invincibility()
	{
		invincible = true;
		
		float time = 0;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		
		while (time < invincibilityTime)
		{
			Color c = renderer.color;
			c.a = 1.0f - c.a;
			renderer.color = c;
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Color _c = renderer.color;
		_c.a = 1.0f;
		renderer.color = _c;
		invincible = false;
	}
}
