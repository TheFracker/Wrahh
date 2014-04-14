﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GUISkin mySkin;
	bool hudOn;

	Wrahh wrahh = new Wrahh();

	void Start () {

	}

	void FixedUpdate () {
		OnGUI();
	}

	void OnGUI()
	{
		GUI.Box (new Rect(225 , Screen.height-600, 100,20),"Health: " + wrahh.Health);
		GUI.Box (new Rect(425 , Screen.height-600, 100,20),"Armor: " + wrahh.Armor);
		GUI.Box (new Rect(625 , Screen.height-600, 100,20),"Lobster Parts: " + wrahh.LobsterParts);
		GUI.Box (new Rect(825 , Screen.height-600, 100,20),"Rifles: " + wrahh.RiflesCollected);
		GUI.Box (new Rect(1025 , Screen.height-600, 100,20),"Guns: " + wrahh.GunsCollected);
	}
}
