using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class SpamMenuManager : MenuManager
{
	public enum SpamType
	{
		None = 0,
		Maps,
		Mobs,
	}
	
	public MenuSelectMaps MapsMenu = null;
	public MenuSelectMobs MobsMenu = null;

	private SpamType m_SpamType;

	protected void Start()
	{
		ShowMenu(_currentMenu);
		MapsMenu.SelectionTimerOverEvent.AddListener(OnMapsSelectionFinished);
		MobsMenu.SelectionTimerOverEvent.AddListener(OnMobsSelectionFinished);
	}

	protected void OnDestroy()
	{
		MapsMenu.SelectionTimerOverEvent.RemoveListener(OnMapsSelectionFinished);
		MobsMenu.SelectionTimerOverEvent.RemoveListener(OnMobsSelectionFinished);
	}

	public void SetupSpamType(string typeStr)
	{
		SetupSpamType((SpamType)Enum.Parse(typeof(SpamType), typeStr));
	}
	public void SetupSpamType(SpamType type)
	{
		m_SpamType = type;
		if (type == SpamType.Maps)
		{
			ShowMenu(MapsMenu);
		}
		else if (type == SpamType.Mobs)
		{
			ShowMenu(MobsMenu);
		}
		else
		{
			ShowMenu(null);
		}
	}

	private void OnMapsSelectionFinished()
	{
		SetupSpamType(SpamType.Mobs);
	}

	private void OnMobsSelectionFinished()
	{
		OnSelectionFinished();
	}
	
	public UnityEvent SelectionFinishedEvent;
	internal void OnSelectionFinished()
	{
		if (SelectionFinishedEvent != null) SelectionFinishedEvent.Invoke();
	}
}
