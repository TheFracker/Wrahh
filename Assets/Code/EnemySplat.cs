/// <summary>
///This scipts make the enemies splat out when the player jumps down on them. The script should be attached to the child/sprite of the enemie
///prefabs and not the parant that controlls the movement. 
/// </summary>

using UnityEngine;
using System.Collections;

public class EnemySplat : MonoBehaviour {

	Animator anim;														//creates a Animator variable
	public static bool isCrushed;										//creates a bool which can be accesed in other scripts
	AudioSource[] sounds; 												//creates an AudioSource array
	AudioClip ac;														//creates a AudioClip variable
	
	void Start () 														//Use this for initialization
	{
		isCrushed = false;												//initializes bool to false 
		anim = GetComponent<Animator>();								//The animator component on the gameobject is initilized in the Animator variable

		this.gameObject.AddComponent<AudioSource>();					//An AudioSource component is added to the gameobject 
		sounds = GetComponents<AudioSource>();							//The AudioSource components on the object is initialized in a array
		sounds[0].clip = Resources.Load("sounds/splat") as AudioClip;	//The 1st AudioSource component in the array initialize a sound file from the folder "sounds" in "Recourses"
		sounds[0].playOnAwake = false;									//The 1st AudioSource component in the array initializes different attributes in the AudioSource component
		sounds[0].rolloffMode = AudioRolloffMode.Linear;
		sounds[0].pitch = 1.0f;
		sounds[0].volume = 1.0f;
	}
	
	void OnTriggerEnter2D (Collider2D other)							//A OnTriggerEnter
	{
		//Checks on game object tagged "Player", a boolean from the "Wrahh" script and a seond boolean
		//this is true if the player hits this gameobject, wrahh is falling and the object has not been crushed yet
		if(other.tag == "Player" && Wrahh.canCrushEnemy == true && isCrushed == false)
		{
			Destroy(transform.parent.gameObject.collider2D);			//Removes the collider on the parent object so the player can no longer collide with the object
			Destroy(transform.parent.gameObject.rigidbody2D);			//Removes the rigidbody from the parent object so it is not affected by physics, while it then would fall out of the level due to the missing collider
			anim.SetBool("Crushing", true);								//Sets a parameter in the animator to "true", so the splat animation can be played											
			sounds[0].Play();											//Plays the sound in the AudioSource component that is placed 1st in the "sound" array
			isCrushed = true;											//a boolean is set to true, so this if statement is not accessed again  (hint: it is allready crushed)
		}
	}
}
