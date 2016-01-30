using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public Menu _currentMenu; 

	// Use this for initialization
	void Start () 
	{
		ShowMenu (_currentMenu);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShowMenu(Menu menu)
	{
		_currentMenu.CloseMenu();
		_currentMenu = menu;
		_currentMenu.OpenMenu();
	}

	public void LaunchGame()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
