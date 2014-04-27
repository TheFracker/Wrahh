/// <summary>
/// Script for ladder objects that enable the player to climb them then in range
/// </summary>

using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	private bool isTrigger;									//a private bool is created
	public static bool canClimb;  							//a bool that can be accessed by other script is created (used in wrahh.cs)
	
	void Start()
	{
		canClimb = false;									//bool initialized to false
		isTrigger = false;									//bool initialized to false
	}

	void Update () 
	{
		// If "W" or "up arrow" is pressed and the trigger is active/the player is in the trigger zone
		if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isTrigger == true)
		{
			canClimb = true; 								//boolean set to true and used in (Wrahh.cs) to enable crawling animations and adjust gravity and drag
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) 				//Checks if character stays within collision area and sets can climb to true.
	{
		if (other.tag == "Player" && isTrigger == false)	
		{
			isTrigger = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) //Checks if characters is exiting trigger area and sets can climb to false.
	{
		if (other.tag == "Player"){
			canClimb = false;
			isTrigger = false;
		}
	}
}
