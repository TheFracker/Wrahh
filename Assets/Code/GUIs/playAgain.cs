using UnityEngine;
using System.Collections;

public class playAgain : MonoBehaviour
{
	public Texture2D playAgain_btn, playAgainHover_btn;

	void OnMouseEnter()
	{
		guiTexture.texture = playAgainHover_btn;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = playAgain_btn;
	}
	
	void OnMouseDown()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}