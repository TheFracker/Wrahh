﻿using UnityEngine;
using System.Collections;

public class exitButton : MonoBehaviour
{
	public Texture2D exitNormal,exitHover;

	void OnMouseEnter()
	{
		guiTexture.texture = exitHover;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = exitNormal;
	}

	void OnMouseDown()
	{
		Application.Quit();
	}
}
