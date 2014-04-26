using UnityEngine;
using System.Collections;

public class DolphineDeath : MonoBehaviour {


	public static bool isCrushed = false;
	Animator anim;

	AudioSource[] sounds; 
	AudioSource dolphineSplat;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();

		sounds = GetComponents<AudioSource>();		//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		dolphineSplat = sounds[0];
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		
		if(other.collider.tag == "Player" && Wrahh.canCrushEnemy == true && isCrushed == false)
		{
			anim.SetBool("Crushing", true);
			isCrushed = true;
			dolphineSplat.Play();
		}
	}



}
