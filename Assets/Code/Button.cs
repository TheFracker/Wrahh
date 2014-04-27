using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour 
{
	public static bool buttonActive = false; 								//bool used to check if the button is active and to send on to "Doors.cs"
	private bool triggerActive = false; 									//bool to check wether the trigger area have been entered 
	private AudioSource[] sounds; 											// creates an array "sounds" of type "AudioSource"
	private AudioSource buttonActivated;									// creates an variable "buttonActivated" of type "AudioSource"
	private AudioSource buttonDeActivated;									// creates an variable "buttonDeActivated" of type "AudioSource"	

	void Start () 													// Use this for initialization
	{
		sounds = GetComponents<AudioSource>();						//all audio source components on the object is put in the "sounds" array in the order they are listed on the object
		buttonActivated = sounds[0];								//the different audio sources in the "sounds" array is initialized in a variable to create a better overwiev
		buttonDeActivated = sounds[1];
	}
	
	void OnTriggerEnter2D(Collider2D other)							//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	{
		if(other.tag == "Player")									//checks if other gameobject (the coliding object) is the player
		{
			if(triggerActive == false)								//checks if the boolean "triggerAcvtive" is false
			{
				triggerActive = true; 								//Sets "triggerActive" to true so this if statement only are run once on OnTriggerEnter. (prvents the "bug" where Unity continualy trigger OnTriggerEnter)
				Animator anim = GetComponent<Animator>();			//The animator component on the object is accessed through anim

				if(anim.GetBool("push") == false)					//checks if boolean paremeter "push" in animator is false
				{
					anim.SetBool("push", true); 					//Set the boolean paremeter "push" to true (opening up for a animation transition)
					buttonActive = true; 							//Set "buttonActive" variable to true for use in "Doors.cs"
					buttonActivated.Play();							//Plays the sound in the AudioSource variable "buttonActivated"
				}

				else if(anim.GetBool("push") == true)				//if boolean paremeter "push" in animator is not false, then check if it is true
				{
					anim.SetBool("push", false); 					//Set the boolean paremeter "push" to false (opening up for the animation transition (opening up for a animation trasition)
					buttonActive = false; 							//Set "buttonActive" variable to false for use in "Doors.cs"
					buttonDeActivated.Play();						//Plays the sound in the AudioSource variable "buttonDeActivated"
				}
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)							//A OnTriggerExit, which runs when/if "other" exits the trigger area (remember to set colider to "is trigger")
	{
		if(other.tag == "Player"){									//checks if other gameobject (the coliding object) is the player
			triggerActive = false; 									//Sets "triggerActive" to false so the stuff in OnTriggerEnter can happen again if the trigger is entered again after exit
		}
	}
}
		
	