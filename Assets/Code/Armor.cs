using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{

	public Sprite shieldSprite;
	public Sprite helmetSprite;
	public int armorState = 0;
	private SpriteRenderer sprRenderer;

	void Start()
	{
		sprRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void FixedUpdate ()
	{
		if (armorState == 0)
			Debug.Log ("Not wearing armor!");
		else if (armorState == 1)
			Debug.Log ("I have a shield!");
		else if (armorState == 2)
			Debug.Log ("I have a helmet!");
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player" && this.gameObject.name == "shield")
		{
			armorState = 1;
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
