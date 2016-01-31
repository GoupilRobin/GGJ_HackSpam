using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MenuInGame : Menu
{
	public UnityEvent VictoryEvent;
	internal void OnVictory()
	{
		if (VictoryEvent != null) VictoryEvent.Invoke();
	}
}
