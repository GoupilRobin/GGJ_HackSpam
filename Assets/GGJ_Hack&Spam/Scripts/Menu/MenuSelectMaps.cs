using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuSelectMaps : Menu
{
	public int SelectionTimer = 6000;
	public Text TimerText;
	public GameObject Map1 = null;
	public GameObject Map2 = null;
	public GameObject Map3 = null;
	public string SelectedMapName { get; private set; }

	private int m_CurrentSelectionTime = 0;
	private string m_TimerTextFormat;
	private bool m_TimerTriggered;

	protected void Start()
	{
		m_TimerTextFormat = TimerText.text;

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
			OnSelectionTimerOver();
		}
	}

	public void StartTimer()
	{
		m_CurrentSelectionTime = SelectionTimer;
		m_TimerTriggered = false;

		PopulateMap(Map1);
		PopulateMap(Map2);
		PopulateMap(Map3);
	}

	private void PopulateMap(GameObject mapObject)
	{
		int levelIndex = Random.Range(0, MainMenuManager.Levels.Count);
		MainMenuManager.LevelPackage level = MainMenuManager.Levels[levelIndex];
		mapObject.GetComponentInChildren<Image>().sprite = level.Thumbnail;
		mapObject.GetComponentInChildren<Text>().text = level.Name;
	}

	public UnityEvent SelectionTimerOverEvent;
	public void OnSelectionTimerOver()
	{
		if (SelectionTimerOverEvent != null) SelectionTimerOverEvent.Invoke();
	}
}