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

	public class MobPackage
	{
		public string Name;
		public GameObject Prefab;
		public Sprite Thumbnail;
		public int KeyLength;
	}
	
	public static List<LevelPackage> Levels = new List<LevelPackage>() {
		new LevelPackage() { Name = "Level1", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level1", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level2", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level2", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level3", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level3", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level4", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level4", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level5", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level5", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level6", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level6", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level7", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level7", typeof(Sprite)) as Sprite },
		new LevelPackage() { Name = "Level8", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level8", typeof(Sprite)) as Sprite },
	};
	
	public static List<MobPackage> Mobs = new List<MobPackage>() {
		new MobPackage() { Name = "Bat", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/Bat", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/Batman", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "Gargoyle", KeyLength = 5, Prefab = Resources.Load("Prefabs/Monsters/Gargoyle", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/Gargoyle3", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "SlimeBlue", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeBlue", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/SlimeBlue", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "SlimeGreen", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeGreen", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/SlimeGreen", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "SlimeRed", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeRed", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/SlimeRed", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "Wizard", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/Wizard", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/Wizard", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "ZombieFemale", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/ZombieFemale", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/Zombie2", typeof(Sprite)) as Sprite },
		new MobPackage() { Name = "ZombieMale", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/ZombieMale", typeof(GameObject)) as GameObject, Thumbnail = Resources.Load("Sprites/Zombie1", typeof(Sprite)) as Sprite },
	};

	public Menu _currentMenu;
	
	// Use this for initialization
	protected void Start()
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
	
	public void LaunchMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
