﻿using UnityEngine;
using System.Collections;

public class GameCharacters : MonoBehaviour
{
	protected int health;
	protected int armor;
	protected float moveSpeed;
	protected bool facingRight;
	protected float standardGravity = 7.4f; 				// initial gravity
	protected float MAX_MOVE_SPEED; 						
	protected float standardDrag = 5f; 						// initial drag force
	protected bool playerDead = false;
	
	protected Animator anim;

	protected void accesAnimator()
	{
		anim = GetComponent<Animator>(); 
	}

	protected void die(GameObject g)
	{
		Destroy(g);
	}

	protected void flip()
	{
		facingRight = !facingRight;
		Vector3 direction = this.transform.localScale;
		direction.x *= -1;
		this.transform.localScale = direction;
	}

	protected void setStandardPhysics ()
	{
		this.rigidbody2D.gravityScale = standardGravity;
		this.rigidbody2D.drag = standardDrag;
	}

	public bool isFacingRight()
	{
		return facingRight;
	}
}