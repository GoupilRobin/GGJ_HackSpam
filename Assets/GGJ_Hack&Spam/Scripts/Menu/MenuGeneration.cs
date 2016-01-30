using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MenuGeneration : Menu
{
	public void Start()
	{
		StartCoroutine(coroutine_DelayLevelLoaded());
	}

	private IEnumerator coroutine_DelayLevelLoaded()
	{
		yield return new WaitForSeconds(0.5f);
		
		OnLevelLoaded();
	}

	public void DelayLoadSelectedLevel(MenuSelectMaps menuSelectMaps)
	{
		StartCoroutine(coroutine_DelayLoadSelectedLevel(menuSelectMaps));
	}

	private IEnumerator coroutine_DelayLoadSelectedLevel(MenuSelectMaps menuSelectMaps)
	{
		yield return new WaitForSeconds(2.0f);

		Application.LoadLevel(menuSelectMaps.SelectedMapName);
	}

	public UnityEvent LevelLoadedEvent;
	public void OnLevelLoaded()
	{
		if (LevelLoadedEvent != null) LevelLoadedEvent.Invoke();
	}
}
