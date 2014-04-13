using UnityEngine;
using System.Collections;

public class UpgradeStation : MonoBehaviour {

	public GUISkin mySkin;
	Rect mainWindow;
	bool menuShow = false;
	

	bool paused = false, playerEnter = true;

	void Start()
	{
		mainWindow = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 300,300);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		paused = true;

	if (paused)
		if (other.tag == "Player"&&playerEnter){
			menuShow = true;
			Time.timeScale = 0;
			playerEnter = false;
		}
	
	}
	void OnTriggerExit2D(Collider2D other)
	{
		playerEnter = true;
	}

	void OnGUI()
	{
		if(menuShow){
			mainWindow = GUI.Window(0, mainWindow, windowFunc, "Upgrade Station");
		}
	}
	
	void windowFunc(int id)
	{
		if(GUILayout.Button("Weapon Upgrades"))
		{
		
		}
		if(GUILayout.Button("Armor Upgrades"))
		{

		}
		if(GUILayout.Button("Upgrade Later!"))
		{
			paused = false;
			menuShow = false;
			Time.timeScale = 1;
		}
	}
	

}
