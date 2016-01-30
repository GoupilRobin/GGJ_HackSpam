using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
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
		Application.LoadLevel("room_tutorial");
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
