using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class MenuSelectMaps : MenuSelect
{
	private class ExposedMap
	{
		public string Name;
		public GameObject GameObject;
	}

	public GameObject Map1 = null;
	public GameObject Map2 = null;
	public GameObject Map3 = null;
	public string SelectedMapName { get; private set; }

	private List<ExposedMap> m_ShownMap = new List<ExposedMap>();

	public override void StartTimer()
	{
		base.StartTimer();

		SelectedMapName = "";
		
		m_ShownMap.Clear();
		PopulateMap(Map1);
		PopulateMap(Map2);
		PopulateMap(Map3);
	}

	protected override void HandleMessageRecievedEvent(MessageIRC message)
	{
		base.HandleMessageRecievedEvent(message);

		for (int i = 0; i < m_ShownMap.Count; i++)
		{
			string param = message.Parameters[1];
			if (param.Substring(0, param.Length - 2) == m_ShownMap[i].Name)
			{
				SelectedMapName = m_ShownMap[i].Name;
				break;
			}
		}
	}

	protected override void HandleSelectionTimerOver()
	{
		base.HandleSelectionTimerOver();

		if (string.IsNullOrEmpty(SelectedMapName))
		{
			int index = Random.Range(0, m_ShownMap.Count);
			SelectedMapName = m_ShownMap[index].Name;
		}
	}

	private void PopulateMap(GameObject mapObject)
	{
		int levelIndex = Random.Range(0, MainMenuManager.Levels.Count);
		MainMenuManager.LevelPackage level = MainMenuManager.Levels[levelIndex];
		mapObject.GetComponentInChildren<Image>().sprite = level.Thumbnail;
		mapObject.GetComponentInChildren<Text>().text = level.Name;
		m_ShownMap.Add(new ExposedMap() { Name=level.Name, GameObject=mapObject });
	}
}