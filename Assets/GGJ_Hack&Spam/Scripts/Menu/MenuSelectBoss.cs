using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MenuSelectBoss : MenuSelect
{
	public class MobKey
	{
		public string Key;
		public int RevealedCount;
		
		public string ExposedKey { get { return string.Concat(Key.Substring(0, RevealedCount), new string('?', Key.Length - RevealedCount)); } }
	}

	public GameObject BossUI = null;

	private MobKey m_CurrentKey;
	private MenuManager.MobPackage ExposedBoss = null;
	private Queue<int> m_PendingUIUpdates = new Queue<int>();
	private Queue<int> m_PendingReplaces = new Queue<int>();
	private int m_UpgradeLevel = 0;

	public override void StartTimer()
	{
		base.StartTimer();

		m_UpgradeLevel = 0;
		MenuManager.MobPackage bossPrefab = MenuManager.Bosses[Random.Range(0, MenuManager.Bosses.Count - 1)];
		ExposedBoss = bossPrefab;
		GenerateKey();
		UpdateUIElement();
	}
	
	protected new void Update()
	{
		base.Update();
		
		while (m_PendingUIUpdates.Count > 0)
		{
			m_PendingUIUpdates.Dequeue();
			UpdateUIElement();
		}
		
		while (m_PendingReplaces.Count > 0)
		{
			m_PendingReplaces.Dequeue();
			GenerateKey();
			UpdateUIElement();
		}
	}
	
	protected override void HandleMessageRecievedEvent(MessageIRC message)
	{
		base.HandleMessageRecievedEvent(message);
		
		string word = message.Parameters[1];
		word = word.Substring(0, word.Length - 2);

		int matched = WordComparator.Comparator(word, m_CurrentKey.Key);
		if (matched > m_CurrentKey.RevealedCount)
		{
			m_CurrentKey.RevealedCount = matched;
			m_PendingUIUpdates.Enqueue(0);
			if (m_CurrentKey.RevealedCount == m_CurrentKey.Key.Length)
			{
				m_PendingReplaces.Enqueue(0);
				m_UpgradeLevel++;
			}
		}
	}

	protected override void HandleSelectionTimerOver()
	{
		base.HandleSelectionTimerOver();
		
		List<IA> aiList = new List<IA>();
		aiList.Add(ExposedBoss.Prefab.GetComponent<IA>());
		MasterSpawner.monsters = aiList;
	}

	private void GenerateKey()
	{
		m_CurrentKey.Key = WordGenerator.Generate(ExposedBoss.KeyLength);
		m_CurrentKey.RevealedCount = 0;
	}
	
	protected void UpdateUIElement()
	{
		float progress = (1.0f / 4.0f) * m_UpgradeLevel;

		BossUI.transform.FindChild("Panel/Text").GetComponent<Text>().text = ExposedBoss.Name;
		BossUI.transform.FindChild("Panel/Text_key").GetComponent<Text>().text = m_CurrentKey.ExposedKey;
		BossUI.transform.FindChild("Panel/Image").GetComponent<Image>().sprite = ExposedBoss.Thumbnail;
		BossUI.transform.FindChild("Slider").GetComponent<Slider>().value = progress;
		ColorBlock cblock = BossUI.transform.FindChild("Slider").GetComponent<Slider>().colors;
		cblock.normalColor = Color.Lerp(Color.green, Color.red, progress);
		BossUI.transform.FindChild("Slider").GetComponent<Slider>().colors = cblock;
	}
}
