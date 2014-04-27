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
	protected Animator anim;								// The animator accesor
	protected float duration = 1.0f;
	protected bool isHurt;

	void Start()
	{
		isHurt = false;
	}

	protected void accesAnimator()
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
}