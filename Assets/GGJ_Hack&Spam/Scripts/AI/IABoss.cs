﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABoss : IA {
	
	private Transform _spawner;
	public bool moreFireBalls = false;
	public bool doubleSpeed = false;
	public bool doubleHP = false;
	public bool spawners = false;
	private List<Transform> _list;

	void Start()
	{
		_spawner = transform.Find("spawners");
		_list = new List<Transform> ();
		Init ();
	}

	void FixedUpdate()
	{
		if (!doubleSpeed)
			_spawner.Rotate (new Vector3 (0, 0, 10 * Time.deltaTime));
		else
			_spawner.Rotate (new Vector3 (0, 0, 20 * Time.deltaTime));
	}

	void Init()
	{
		_list.Add(_spawner.Find ("spawner_1"));
		_list.Add(_spawner.Find ("spawner_2"));
		_list.Add(_spawner.Find ("spawner_3"));
		_list.Add(_spawner.Find ("spawner_4"));
		if (moreFireBalls) 
		{
			_list.Add(_spawner.Find ("spawner_5"));
			_list.Add(_spawner.Find ("spawner_6"));
			_list.Add(_spawner.Find ("spawner_7"));
			_list.Add(_spawner.Find ("spawner_8"));
		}
		if (doubleHP)
			hitpoint *= 2;
		if (spawners)
			Spawn ();
		if (doubleSpeed)
			InvokeRepeating ("Shoot", 2, 0.5f);
		else
			InvokeRepeating ("Shoot", 2, 1);
	}

	void Spawn()
	{

	}

	void Shoot()
	{
		foreach (Transform spawn in _list) 
		{
			GameObject g = Resources.Load ("Prefabs/FireBall", typeof(GameObject)) as GameObject;
			GameObject s = Instantiate(g, spawn.position, spawn.rotation) as GameObject;
			s.GetComponent<IAFireBall>().BossShoot ();
		}
	}
}
