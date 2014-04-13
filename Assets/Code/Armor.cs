using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{

	public Sprite shield_sprite;
	public Sprite helmet_sprite;

	void FixedUpdate ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player" && this.gameObject.name == "shield")
		{
			///////////////////////////////////
			// TO-DO:						 //
			// --							 //
			// Shield should be collected	 //
			// Delete shield from scene		 //
			// Add shield to Wrahh's sprite  //
			///////////////////////////////////
			Debug.Log("Shield!");

		}
		if (c.tag == "Player" && this.gameObject.name == "helmet")
		{
			///////////////////////////////////
			// TO-DO:						 //
			// --							 //
			// Helmet should be collected	 //
			// Delete helmet from scene		 //
			// Add helmet to Wrahh's sprite  //
			///////////////////////////////////
			Debug.Log("Helmet!");
		}
	}
}
