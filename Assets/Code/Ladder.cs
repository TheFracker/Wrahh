using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	bool canClimb = false; //Variable to detect wether or not Wrahh should be able to climb

	void OnTriggerStay2D (Collider2D other)//Checks if character stays within collision area and sets can climb to true.
	{
		canClimb = true; 
		//If canClimp evaluates to true and a button on the vertical axis is pressed, the character will move up.
		if (Input.GetAxis("Vertical") > 0 && canClimb == true){
			other.rigidbody2D.velocity = new Vector2(0,3f);
		}
	}
	void OnTriggerExit2D(Collider2D other) //Checks if characters is exiting trigger area and sets can climb to false.
	{
		canClimb = false;
	}
}
