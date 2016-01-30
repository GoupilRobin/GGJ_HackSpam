using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MenuInGame : Menu
{
	protected void Update()
	{
		// HACK
		if (Input.GetKeyDown(KeyCode.Return))
		{
			OnVictory();
		}
	}

	public UnityEvent VictoryEvent;
	internal void OnVictory()
	{
		if (VictoryEvent != null) VictoryEvent.Invoke();
	}
}
