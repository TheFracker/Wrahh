using UnityEngine;
using System.Collections;

public class EnemySplat : MonoBehaviour {


	public static bool isCrushed = false;
	Animator anim;

	AudioSource[] sounds; 
	AudioClip ac;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();

		this.gameObject.AddComponent<AudioSource>();
		sounds = GetComponents<AudioSource>();		//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		ac = Resources.Load("sounds/splat") as AudioClip;
		sounds[0].clip = ac;
		sounds[0].playOnAwake = false;
		sounds[0].rolloffMode = AudioRolloffMode.Linear;
		sounds[0].pitch = 1f;
		sounds[0].volume = 1f;
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		
		if(other.tag == "Player" && Wrahh.canCrushEnemy == true && isCrushed == false)
		{
			Destroy(transform.parent.gameObject.collider2D);
			Destroy(transform.parent.gameObject.rigidbody2D);
			anim.SetBool("Crushing", true);
			isCrushed = true;
			sounds[0].Play();

		}
	}



}
