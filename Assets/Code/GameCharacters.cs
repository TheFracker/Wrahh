/// <summary>
/// This sripts should be inherinted by all game characters in the game. It contains functions and attributes for animations, damage, speed, health,
///filp, physics etc. 
/// </summary>

using UnityEngine;
using System.Collections;

public class GameCharacters : MonoBehaviour
{
	protected int health;									// The health of a character
	protected int armor;									// The armor a character might have
	protected float moveSpeed;								// The speed at which a character will move
	protected bool facingRight;								// Used to check what direction the charater is facing
	protected float standardGravity = 7.4f; 				// initial gravity
	protected float MAX_MOVE_SPEED; 						// The maximum movement speed that a character should be able to achive
	protected float standardDrag = 5f; 						// initial drag force
	protected Animator anim;								// Creates an Animator variable
	protected float duration = 1.0f;
	protected bool isHurt;
	
	//Sets bool to false (no one hurts anyone in the beginning
	void Start()
	{
		isHurt = false; // Used to check if a character is hurt - used for blinking red when hurt
	}

	// This IEnumerator waits a split second before turning the red color off and back to normal (white)
	protected IEnumerator waitForBlink()
	{
		while(isHurt)
		{
			yield return new WaitForSeconds(0.1f); 				// The split second that was waited
			foreach (Transform child in transform) 				// Since all the characters are stores in empty gameobjects, we have to
			{													// access their children which contains the SpriteRenderer og the graphics.
				child.renderer.material.color = Color.white; 	// Make the color on the children's renderers white (back to default)
			}
			isHurt = false; 									//Now stop this boolean because color-wise we are not being hurt anymore.
		}
	}

	// Actual method for turning the characters red
	protected void blinkRed()
	{
		foreach (Transform child in transform)					// Same as above - access all children of the gameobject
		{
			child.renderer.material.color = Color.red;			// But this time, turn them red!
		}			
		isHurt = true; 											// And color-wise we are getting hurt
		StartCoroutine("waitForBlink"); 						// So start the IEnumerator so we can go back to normal
	}

	// For accessing the animator - duh!
	protected void accessAnimator()
	{
		anim = GetComponent<Animator>();
	}

	// Destroyes the character that died
	protected virtual void die(GameObject g)
	{
		Destroy(g);
	}

	// Is used to change the direction of a character
	protected void flip()
	{
		facingRight = !facingRight;
		Vector3 direction = this.transform.localScale;
		direction.x *= -1;
		this.transform.localScale = direction;
	}

	// Set the standard physics of a character
	protected void setStandardPhysics ()
	{
		this.rigidbody2D.gravityScale = standardGravity;
		this.rigidbody2D.drag = standardDrag;
	}

	// Called to get information about what direction the character is facing
	public bool isFacingRight()
	{
		return facingRight;
	}

	protected virtual IEnumerator waitForAttackingAnimation()
	{
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("isAttacking", false);
	}
}