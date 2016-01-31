using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuInGame : Menu
{
	public Player Player = null;
	public Text TextHP = null;
	public Text TextScore = null;
	
	private string m_HPFormat;
	private string m_ScoreFormat;

	public UnityEvent VictoryEvent;
	internal void OnVictory()
	{
		if (VictoryEvent != null) VictoryEvent.Invoke();
	}

	protected void Start()
	{
		if (Player == null)
		{
			Player = FindObjectOfType<Player>();
		}
		if (TextHP != null)
		{
			m_HPFormat = TextHP.text;
		}
		if (TextScore != null)
		{
			m_ScoreFormat = TextScore.text;
		}
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			OnVictory();
		}

		if (TextHP != null)
		{
			TextHP.text = string.Format(m_HPFormat, Player.life);
		}
		if (TextScore != null)
		{
			TextScore.text = string.Format(m_ScoreFormat, Player.score);
		}
	}
}
