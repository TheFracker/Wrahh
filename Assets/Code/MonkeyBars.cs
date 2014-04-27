/// <summary>
/// Script for the monkeybar. Enable the player to climb when in range
/// </summary>

using UnityEngine;
using System.Collections;

public class MonkeyBars : MonoBehaviour {
	
	private bool triggerActive = false; 			//bool to check wether the trigger area have been entered 
	public static bool onMonkeyBar; 				//bool used to check if the button is active and to send on to "Wrahh.cs"

	void Update ()									// Update is called once per frame 
	{
		// If "W" or "up arrow" is pressed and the trigger is active/the player is in the trigger zone
		if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && triggerActive == true)
		{
			onMonkeyBar = true; 					//bolean set to true and used in (Wrahh.cs) to enable crawling animations and adjust gravity and drag
		}

		// If "S" or "down arrow" is pressed
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{	
			onMonkeyBar = false; 					//bolean set to false and used in (Wrahh.cs) to disable crawling animations and set gravity and drag to standard
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) 		//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	{
		//checks if other gameobject (the coliding object) is the player and if the trigger is false
		if(other.tag == "Player" && triggerActive == false)
		{
			triggerActive = true; 					//Sets "triggerActive" to true so this if statement only are run once on OnTriggerEnter. (prvents the "bug" where Unity continualy trigger OnTriggerEnter)
		}
	}
	
	void OnTriggerExit2D(Collider2D other)			//A OnTriggerExit, which runs when/if "other" exits the trigger area (remember to set colider to "is trigger")
	{
		if(other.tag == "Player")					//checks if other gameobject (the coliding object) is the player 
		{
			triggerActive = false; 					//The player is not in the trigger zone
			onMonkeyBar = false;					//The player is not on the mokey bar
		}
	}
}
