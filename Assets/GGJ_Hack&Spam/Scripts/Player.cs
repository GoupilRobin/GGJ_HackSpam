using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static int MapsDone = 0;

	public float jumpForce = 250f;
	public int life = 100;
	public int score { get; private set; }
	public string name = "SOSSIFLARD";

	public float maxSpeed;
	public Collider2D _damageBox;

	private Animator _animator;
	private int _grounded = 0;
	private bool _jump;
	private Rigidbody2D _body;
	private bool facingRight;
	private Transform _groundCheckStart;
	private Transform _groundCheck;
	private Time	  _timer;

	// Use this for initialization
	void Awake () 
	{
		_animator = GetComponent<Animator> ();
		_body = GetComponent<Rigidbody2D> ();
		_groundCheckStart = transform.Find("groundCheckStart");
		_groundCheck = transform.Find("groundCheck");
	}

	// Update is called once per frame
	void Update () 
	{
		if (Physics2D.Linecast(_groundCheckStart.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
			_grounded = 1;
		if (Input.GetKeyDown (KeyCode.Space) && _grounded > 0)
			_jump = true;
		if (Input.GetKeyDown (KeyCode.Return)) {
			_damageBox.enabled = true;
			_animator.Play("Attack");
			Invoke("DisableSword", 1);
		}
	}

	void	DisableSword(){
		_damageBox.enabled = false;
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		
		_body.velocity = new Vector2(h * maxSpeed, _body.velocity.y);

		if (h < 0 && !facingRight)
			Flip ();
		else if (h > 0 && facingRight)
			Flip ();
		if (_jump) 
		{
			if(_grounded == 1)
			{
				_body.velocity = new Vector2(_body.velocity.x , 0f);
			}
			float x = _body.velocity.x;
			_body.AddForce(new Vector2(0f, jumpForce));
			_body.velocity = new Vector2 (Mathf.Sign(x) * maxSpeed, _body.velocity.y);
			_grounded = 0;
			_jump = false;
		}
		_animator.SetFloat ("walkSpeed", _body.velocity.x);
	}

	public void OnDamaged(int damage)
	{
		life -= damage;
		if (life < 1)
			Death ();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void Death()
	{
		Application.LoadLevel ("MainMenu");
	}
}
