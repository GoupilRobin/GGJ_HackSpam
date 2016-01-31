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
		Boss,
	}
	
	public MenuSelectMaps MapsMenu = null;
	public MenuSelectMobs MobsMenu = null;
	public MenuSelectBoss BossMenu = null;

	protected new void Start()
	{
		if (Player.MapsDone > 10 || true)
		{
			ShowMenu(BossMenu);
		}
		else
		{
			ShowMenu(_currentMenu);
		}
		MapsMenu.SelectionTimerOverEvent.AddListener(OnMapsSelectionFinished);
		MobsMenu.SelectionTimerOverEvent.AddListener(OnMobsSelectionFinished);
		BossMenu.SelectionTimerOverEvent.AddListener(OnBossSelectionFinished);
	}

	protected void OnDestroy()
	{
		MapsMenu.SelectionTimerOverEvent.RemoveListener(OnMapsSelectionFinished);
		MobsMenu.SelectionTimerOverEvent.RemoveListener(OnMobsSelectionFinished);
		BossMenu.SelectionTimerOverEvent.RemoveListener(OnBossSelectionFinished);
	}

	public void SetupSpamType(string typeStr)
	{
		SetupSpamType((SpamType)Enum.Parse(typeof(SpamType), typeStr));
	}
	public void SetupSpamType(SpamType type)
	{
		if (type == SpamType.Maps)
		{
			ShowMenu(MapsMenu);
		}
		else if (type == SpamType.Mobs)
		{
			ShowMenu(MobsMenu);
		}
		else if (type == SpamType.Boss)
		{
			ShowMenu(BossMenu);
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
	
	private void OnBossSelectionFinished()
	{
		OnSelectionFinished();
	}
	
	public UnityEvent SelectionFinishedEvent;
	internal void OnSelectionFinished()
	{
		if (SelectionFinishedEvent != null) SelectionFinishedEvent.Invoke();
	}
}
