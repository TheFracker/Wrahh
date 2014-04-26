using UnityEngine;
using System.Collections;

public class jail : MonoBehaviour {


	Animator anim;							// creates an variable "anim" of type "Animator"
	AudioSource[] sounds; 					// creates an array "sounds" of type "AudioSource"
	AudioSource jailDoor;					// creates an variable "jailDoor" of type "AudioSource"

	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();		//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		jailDoor = sounds[0];						//the different audio sources in the "sounds" array is initialized in a variable to create a better overwiev
		anim = GetComponent<Animator>();			//The animator component on the object is accessed through anim
	}
	

	void OnTriggerEnter2D(Collider2D other)		//checks for collision
	{
		if(other.collider2D.tag == "HitProjectile" && anim.GetBool("DoorHit") == false)			//cheks if an object with "player" tag collides with the object
		{
			anim.SetBool("DoorHit", true);			//sets the boolean parameter "DoorHit" in the animator to true
			jailDoor.Play();					//plays the audio in the varible "jailDoor". Could also be played by sounds[0].PlayOneShot
		}
			
	}

}
