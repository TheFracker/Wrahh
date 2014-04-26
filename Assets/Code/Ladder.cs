using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {


	private bool isTrigger = false;
	public static bool canClimb = false;
	

	void Update () {
		
		// If "W" or "up arrow" is pressed and the trigger is active/the player is in the trigger zone
		if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isTrigger == true){
			canClimb = true; //boolean set to true and used in (Wrahh.cs) to enable crawling animations and adjust gravity and drag

		}
	}
	
	void OnTriggerEnter2D (Collider2D other) //Checks if character stays within collision area and sets can climb to true.
	{
		//Debug.Log("im triggered");
		if (other.tag == "Player" && isTrigger == false){
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
