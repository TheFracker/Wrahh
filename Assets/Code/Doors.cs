using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	Animator anim;							// creates an variable "anim" of type "Animator";
	AudioSource[] sounds; 											// creates an array "sounds" of type "AudioSource"
	AudioSource doorSound;											// creates an variable "buttonPress1" of type "AudioSource"


	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();						//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		doorSound = sounds[0];										//the different audio sources in the "sounds" array is initialized in a variable to create a better overwiev
		anim = GetComponent<Animator>();							//The animator component on the object is accessed through anim
	}



	//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	void OnTriggerEnter2D(Collider2D other){

		//checks if other gameobject (the coliding object) is the object draged into the public GameObject variable "Wrahhh" AND if the boolean "buttonActive" from "Button.cs" is true 
		if(other.tag == "Player" && Button.buttonActive == true && anim.GetBool("open") == false){ 
			anim.SetBool("open", true); 							//Set the boolean paremeter "open" to true (opening up for a animation transition)
			doorSound.Play ();
		}
	}
}
