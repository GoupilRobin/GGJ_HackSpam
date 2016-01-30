using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	private CanvasGroup _canvas;
	private Animator _animator;

	public bool IsOpen
	{
		get { return _animator.GetBool("IsOpen");}
		set { _animator.SetBool ("IsOpen", value);}
	}

	// Use this for initialization
	void Start () {
		_canvas = GetComponent<CanvasGroup> ();
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) 
		{
			_canvas.alpha = 0;
			_canvas.blocksRaycasts = _canvas.interactable = false;
		} 
		else 
		{
			_canvas.alpha = 1;
			_canvas.blocksRaycasts = _canvas.interactable = true;
		}
	}


}
