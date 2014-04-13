using UnityEngine;
using System.Collections;



public class Button : MonoBehaviour {

	private bool triggerActive = false; //bool to check wether the trigger area have been entered 
	public static bool buttonActive = false; //bool used to check if the button is active and to send on to "Doors.cs"

	//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	void OnTriggerEnter2D(Collider2D other){

		//checks if other gameobject (the coliding object) is the object draged into the public GameObject variable "Wrahhh"
		if(other.tag == "Player"){

			//checks if the boolean "triggerAcvtive" is false 
			if(triggerActive == false){
				triggerActive = true; //Sets "triggerActive" to true so this if statement only are run once on OnTriggerEnter. (prvents the "bug" where Unity continualy trigger OnTriggerEnter)
				Animator anim = GetComponent<Animator>(); //Creates an local variable called "anim" of the unity type "Animator" to acces the Animator controller for this object	

				//checks if boolean paremeter "push" in animator is false
				if(anim.GetBool("push") == false){
					anim.SetBool("push", true); //Set the boolean paremeter "push" to true (opening up for a animation transition)
					buttonActive = true; //Set "buttonActive" variable to true for use in "Doors.cs"
				}

				//if boolean paremeter "push" in animator is not false, then check if it is true
				else if(anim.GetBool("push") == true){
					anim.SetBool("push", false); //Set the boolean paremeter "push" to false (opening up for the animation transition (opening up for a animation trasition)
					buttonActive = false; //Set "buttonActive" variable to false for use in "Doors.cs"
				}
			}
		}
	}

	//A OnTriggerExit, which runs when/if "other" ecits the trigger area (remember to set colider to "is trigger")
	void OnTriggerExit2D(Collider2D other){

		//checks if other gameobject is the object draged into the public GameObject variable "Wrahhh"
		if(other.tag == "Player"){
			triggerActive = false; //Sets "triggerActive" to false so the stuff in OnTriggerEnter can happen again if the trigger is entered again after exit
		}
	}
}
		
	