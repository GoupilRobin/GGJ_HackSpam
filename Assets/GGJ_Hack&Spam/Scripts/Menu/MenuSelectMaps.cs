using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class MenuSelectMaps : Menu
{
	private class ExposedMap
	{
		public string Name;
		public GameObject GameObject;
	}

	public int SelectionTimer = 6000;
	public Text TimerText;
	public GameObject Map1 = null;
	public GameObject Map2 = null;
	public GameObject Map3 = null;
	public string SelectedMapName { get; private set; }

	private int m_CurrentSelectionTime = 0;
	private string m_TimerTextFormat;
	private bool m_TimerTriggered;
	private List<ExposedMap> m_ShownMap = new List<ExposedMap>();

	protected void Start()
	{
		m_TimerTextFormat = TimerText.text;
		m_TimerTriggered = true;

		MenuOpenedEvent.AddListener(StartTimer);
	}

	protected void OnDestroy()
	{
		MenuOpenedEvent.RemoveListener(StartTimer);
	}

	protected void Update()
	{
		if (m_CurrentSelectionTime > 0)
		{
			m_CurrentSelectionTime -= (int)(Time.deltaTime * 1000.0f);
			int seconds = Mathf.Max(m_CurrentSelectionTime / 1000, 0);
			int decimals = Mathf.Max(m_CurrentSelectionTime - (seconds * 1000), 0);
			TimerText.text = string.Format(m_TimerTextFormat, seconds, decimals);
		}
		else if (!m_TimerTriggered && m_CurrentSelectionTime <= 0)
		{
			m_TimerTriggered = true;
			if (string.IsNullOrEmpty(SelectedMapName))
			{
				int index = Random.Range(0, m_ShownMap.Count);
				SelectedMapName = m_ShownMap[index].Name;
			}
			TwitchIRC twitchIrc = FindObjectOfType<TwitchIRC>();
			twitchIrc.MessageRecievedEvent -= HandleMessageRecievedEvent;
			OnSelectionTimerOver();
		}
	}

	public void StartTimer()
	{
		m_CurrentSelectionTime = SelectionTimer;
		m_TimerTriggered = false;
		SelectedMapName = "";

		m_ShownMap.Clear();
		PopulateMap(Map1);
		PopulateMap(Map2);
		PopulateMap(Map3);

		TwitchIRC twitchIrc = FindObjectOfType<TwitchIRC>();
		twitchIrc.MessageRecievedEvent += HandleMessageRecievedEvent;
	}

	void HandleMessageRecievedEvent(MessageIRC message)
	{
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

	private void PopulateMap(GameObject mapObject)
	{
		int levelIndex = Random.Range(0, MainMenuManager.Levels.Count);
		MainMenuManager.LevelPackage level = MainMenuManager.Levels[levelIndex];
		mapObject.GetComponentInChildren<Image>().sprite = level.Thumbnail;
		mapObject.GetComponentInChildren<Text>().text = level.Name;
		m_ShownMap.Add(new ExposedMap() { Name=level.Name, GameObject=mapObject });
	}

	public UnityEvent SelectionTimerOverEvent;
	public void OnSelectionTimerOver()
	{
		if (SelectionTimerOverEvent != null) SelectionTimerOverEvent.Invoke();
	}
}