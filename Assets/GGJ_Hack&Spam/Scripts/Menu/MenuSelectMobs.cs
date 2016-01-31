using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuSelectMobs : Menu
{
	public int SelectionTimer = 6000;
	public Text TimerText;
	public GameObject Mob1 = null;
	public GameObject Mob2 = null;
	public GameObject Mob3 = null;
	public GameObject Mob4 = null;
	public GameObject Mob5 = null;
	public GameObject Mob6 = null;
	public GameObject Mob7 = null;
	public GameObject Mob8 = null;
	
	private int m_CurrentSelectionTime = 0;
	private string m_TimerTextFormat;
	private bool m_TimerTriggered;
	
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
			OnSelectionTimerOver();
		}
	}
	
	public void StartTimer()
	{
		m_CurrentSelectionTime = SelectionTimer;
		m_TimerTriggered = false;
	}
	
	public UnityEvent SelectionTimerOverEvent;
	public void OnSelectionTimerOver()
	{
		if (SelectionTimerOverEvent != null) SelectionTimerOverEvent.Invoke();
	}
}
