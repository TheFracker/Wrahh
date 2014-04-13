using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	void OnTriggerEnter2D(Collider2D other){

		//checks if other gameobject (the coliding object) is the object draged into the public GameObject variable "Wrahhh" AND if the boolean "buttonActive" from "Button.cs" is true 
		if(other.tag == "Player" && Button.buttonActive == true){
			Animator anim = GetComponent<Animator>(); //Creates an local variable called "anim" of the unity type "Animator" to acces the Animator controller for this object
			anim.SetBool("open", true); //Set the boolean paremeter "open" to true (opening up for a animation transition)
		}
	}
}
