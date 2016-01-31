﻿using UnityEngine;
using System.Collections;

public class MainMenuManager : MenuManager
{
	private GameObject m_MusicPlaylistPrefab;

	protected new void Start()
	{
		base.Start();

		m_MusicPlaylistPrefab = Resources.Load("Prefabs/MenuMusicPlayer", typeof(GameObject)) as GameObject;
		MusicPlaylist musicPlaylist = FindObjectOfType<MusicPlaylist>();
		if (musicPlaylist == null)
		{
			GameObject.Instantiate(m_MusicPlaylistPrefab);
		}
	}

	protected void OnDestroy()
	{
		MusicPlaylist musicPlaylist = FindObjectOfType<MusicPlaylist>();
		if (musicPlaylist != null)
		{
			DestroyImmediate(musicPlaylist.gameObject);
		}
	}
}
