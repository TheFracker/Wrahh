using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{
	public static void getArmor(string armor)
	{
		if (armor == "shield")
		{
			Debug.Log ("I got a shield!");
		}
		if (armor == "helmet")
			Debug.Log ("I got a helmet!");
	}

	void OnTriggerEnter2D(Collider2D c)
	{

			///////////////////////////////////
			// TO-DO:						 //
			// --							 //
			// Shield should be collected	 //
			// Delete shield from scene		 //
			// Add shield to Wrahh's sprite  //
			///////////////////////////////////
			
			///////////////////////////////////
			// TO-DO:						 //
			// --							 //
			// Helmet should be collected	 //
			// Delete helmet from scene		 //
			// Add helmet to Wrahh's sprite  //
			///////////////////////////////////
	}
}
