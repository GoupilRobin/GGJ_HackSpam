using UnityEngine;
using System.Collections;

public abstract class IA : MonoBehaviour 
{
	public	AudioClip onDamaged;
	public	AudioClip onDeath;

	protected Player _player;
	protected Rigidbody2D _body;
	public float distance;
	public int hitpoint;
	protected bool reversed = false;
	protected	AudioSource _audio;
	private TextMesh _name;

	public void Awake()
	{
		_audio = GetComponent<AudioSource> ();
		_player = FindObjectOfType<Player> ();
		_body = GetComponent <Rigidbody2D>();
		if(_name = GetComponentInChildren<TextMesh>())
			_name.text = "";
	}

	public void OnDamaged(int damage)
	{
		hitpoint -= damage;
		_audio.PlayOneShot (onDamaged);
		if (hitpoint <= 0) {
			AudioSource.PlayClipAtPoint(onDeath, this.transform.position);
			Player.MobKilled += 1;
			Destroy (this.gameObject);
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
}
