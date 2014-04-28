/// <summary>
/// Script for ladder objects that enable the player to climb them then in range
/// </summary>

using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	private bool isTrigger;									//a private bool is created
	public static bool canClimb;  							//a bool that can be accessed by other script is created (used in wrahh.cs)
	Animator playerAnim;
	
	void Start()
	{
		canClimb = false;									//bool initialized to false
		isTrigger = false;									//bool initialized to false
		playerAnim = GameObject.Find("Player").GetComponent<Animator>();
	}

	void Update () 
	{
		// If "W" or "up arrow" is pressed and the trigger is active/the player is in the trigger zone
		if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isTrigger == true)
		{
			playerAnim.SetBool("isClimbing", true);
			canClimb = true; 								//boolean set to true and used in (Wrahh.cs) to enable crawling animations and adjust gravity and drag
		}
		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
		   playerAnim.SetBool("isClimbing", false);
	}
	
	void OnTriggerEnter2D (Collider2D other) 				//Is run when anything enters the trigger area 
	{
		if (other.tag == "Player" && isTrigger == false)	//Checks if the triggering object is the player and if the collider already have been entered 
		{

			isTrigger = true;								//boolean set to true which is used to check if the player can start climbing		
		}
	}

	void OnTriggerExit2D(Collider2D other) 					//Checks if anything is exiting trigger area.
	{
		if (other.tag == "Player")							//Checks if the triggering object is the player
		{
			canClimb = false;								//sets two booleans to false, so the player no longer can climb
			isTrigger = false;

		}
	}
}
