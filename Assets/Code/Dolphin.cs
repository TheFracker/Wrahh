﻿using UnityEngine;
using System.Collections;

public class Dolphin : GameCharacters
{
	Weapon weapon;

	public Transform positionedTarget_right = null;
	public Transform positionedTarget_left = null;
	private Transform target;

	public float maxDistance = 20.0f;
	public float shootDistance = 10.0f;
	public float closeDistance = 0.7f;
	
	private bool playerDead = false;

	private Vector3 dir;
	
	void Start ()
	{
		weapon = gameObject.AddComponent<Pistol> ();

	//From parent:
		health = 3;
		moveSpeed = 5;
		facingRight = false;
	}

	void FixedUpdate()
	{
		//Assign the target to be the whatever object with the tag; "Player"
		target = GameObject.FindWithTag("Player").transform;
		
		// If I (the dolphin) can see the escapen prisonar (Wrahh) and I'm not too far away, I will start chasing him.
		if ((Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x > target.position.x &! facingRight)
		    || (Vector3.Distance(target.position, this.transform.position) < shootDistance && this.transform.position.x < target.position.x && facingRight))
			useWeapon(weapon);
		else if((Vector3.Distance(target.position, this.transform.position) <= maxDistance && this.transform.position.x > target.position.x &! facingRight)
		        || (Vector3.Distance(target.position, this.transform.position) < maxDistance && this.transform.position.x < target.position.x && facingRight))
			chaseTarget();
		else if (Vector3.Distance(target.position, this.transform.position) <= closeDistance)
			chaseTarget();
		else
			guard ();
		// If close enough I will shoot at him
	}
	
	public void hurt()
	{
		int damageTaken = 0;
		health -= damageTaken;
		if(health <= 0)
			die ();
	}

	void useWeapon(Weapon w)
	{
		w.shoot ();
	}

	void guard()
	{
		// Guard right
		if(positionedTarget_right == null){
			Vector2 point = new Vector2 (2, 2);
			if(this.transform.position.x < point.x && facingRight)
				this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > point.x)
				flip();
		}
		else if (this.transform.position.x < positionedTarget_right.position.x && facingRight)
		{
			this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > positionedTarget_right.position.x)
				flip ();
		}
		
		// Guard left
		if(positionedTarget_left == null)
		{
			Vector2 point = new Vector2(-2,2);
			if(this.transform.position.x > point.x &! facingRight)
				this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < point.x)
				flip();
		}
		else if (this.transform.position.x > positionedTarget_left.position.x &! facingRight)
		{
			this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < positionedTarget_left.position.x)
				flip();
		}
	}

	void chaseTarget()
	{
		//The enemy's diretion
		dir = target.position - this.transform.position;
		dir.Normalize(); //Normalize the direction vector
		this.transform.position += dir * moveSpeed * 2 * Time.deltaTime; //CHASE THE PLAYER!!!!!
	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
	}

	public bool isFacingRight()
	{
		return facingRight;
	}
}
