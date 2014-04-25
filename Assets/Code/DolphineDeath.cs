using UnityEngine;
using System.Collections;

public class DolphineDeath : MonoBehaviour {


	public static bool isCrushed = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		
		if(other.collider.tag == "Player" && Wrahh.canCrushEnemy == true)
		{
			anim.SetBool("Crushing", true);
			isCrushed = true;
		}
	}



}
