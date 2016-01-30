using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	public class LevelPackage
	{
		public string Name;
		public float Weight;
		public Sprite Thumbnail;
	}

	public static List<LevelPackage> Levels = new List<LevelPackage>() {
		new LevelPackage() { Name = "DevScene", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/ggj_pixelated", typeof(Sprite)) as Sprite },
	};

	public Menu _currentMenu;
	
	// Use this for initialization
	void Start()
	{
		ShowMenu(_currentMenu);
	}
	
	public void ShowMenu(Menu menu)
	{
		if (_currentMenu != null)
		{
			_currentMenu.CloseMenu();
		}
		_currentMenu = menu;
		if (_currentMenu != null)
		{
			_currentMenu.OpenMenu();
		}
	}
	
	public void LaunchTutorialLevel()
	{
		Application.LoadLevel("TrainingRoom");
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
