using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
	public int Delay;
	public int Duration;
	public float From = 1.0f;
	public float To = 0.0f;

	private RawImage m_Image = null;

	public void Start()
	{
		m_Image = GetComponent<RawImage>();
		StartCoroutine(coroutine_Fade());
	}

	private IEnumerator coroutine_Fade()
	{
		yield return new WaitForSeconds(Delay);
		Color color = m_Image.color;
		color.a = To;
		m_Image.CrossFadeColor(color, Duration, false, true);
	}
}
