using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour {

	private bool triggerActive = false; //bool to check wether the trigger area have been entered 
	public static bool buttonActive = false; //bool used to check if the button is active and to send on to "Doors.cs"

	AudioSource[] sounds; 											// creates an array "sounds" of type "AudioSource"
	AudioSource buttonActivated;										// creates an variable "buttonPress1" of type "AudioSource"
	AudioSource buttonDeActivated;										


	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();						//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		buttonActivated = sounds[0];									//the different audio sources in the "sounds" array is initialized in a variable to create a better overwiev
		buttonDeActivated = sounds[1];
	}

	//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	void OnTriggerEnter2D(Collider2D other){

		//checks if other gameobject (the coliding object) is the player
		if(other.tag == "Player"){

			//checks if the boolean "triggerAcvtive" is false 
			if(triggerActive == false){
				triggerActive = true; 								//Sets "triggerActive" to true so this if statement only are run once on OnTriggerEnter. (prvents the "bug" where Unity continualy trigger OnTriggerEnter)
				Animator anim = GetComponent<Animator>();			//The animator component on the object is accessed through anim

				//checks if boolean paremeter "push" in animator is false
				if(anim.GetBool("push") == false){
					anim.SetBool("push", true); 					//Set the boolean paremeter "push" to true (opening up for a animation transition)
					buttonActive = true; 							//Set "buttonActive" variable to true for use in "Doors.cs"
					buttonActivated.Play();
				}

				//if boolean paremeter "push" in animator is not false, then check if it is true
				else if(anim.GetBool("push") == true){
					anim.SetBool("push", false); 					//Set the boolean paremeter "push" to false (opening up for the animation transition (opening up for a animation trasition)
					buttonActive = false; 							//Set "buttonActive" variable to false for use in "Doors.cs"
					buttonDeActivated.Play();
				}
			}
		}
	}

	//A OnTriggerExit, which runs when/if "other" exits the trigger area (remember to set colider to "is trigger")
	void OnTriggerExit2D(Collider2D other){

		//checks if other gameobject (the coliding object) is the player
		if(other.tag == "Player"){
			triggerActive = false; //Sets "triggerActive" to false so the stuff in OnTriggerEnter can happen again if the trigger is entered again after exit
		}
	}
}
		
	