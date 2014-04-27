using UnityEngine;
using System.Collections;

public class gotoMainMenu : MonoBehaviour
{
	public Texture2D mainMenu_btn, mainMenuHover_btn;
	
	void OnMouseEnter()
	{
		guiTexture.texture = mainMenuHover_btn;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = mainMenu_btn;
	}
	
	void OnMouseDown()
	{
		Application.LoadLevel(0);
	}
}
