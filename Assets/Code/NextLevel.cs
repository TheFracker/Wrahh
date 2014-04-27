/// <summary>
/// Is added to an empty gameobject with an trigger whereever it is wanted that the player should proceed to the next level
/// </summary>

using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour 
{	
	void OnTriggerEnter2D (Collider2D other) 						//Checks if anything enteres the trigger area
	{
		if (other.tag == "Player")									//Checks if the object entering the trigger area is the player
		{
			Application.LoadLevel (Application.loadedLevel + 1);	//Calls the built-in function to laod a new level. Sets it to load the next level by getting the current level + 1
			Button.buttonActive = false;							//Sets a boolean in the script "Button.cs" to lok alle buttons/doors again (so it does not stay true)
		}
	}
}
