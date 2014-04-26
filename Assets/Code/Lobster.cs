﻿using UnityEngine;
using System.Collections;

	/////////////////////////////////////////////////////////////
	// TO DO:
	// -----
	// CHECK FROM WRAHH's SCRIPT IF HE IS DEAD!
	// Be able to kill Wrahh
	// Be able to get killed
	// Less important: Be able to spawn
	/////////////////////////////////////////////////////////////

public class Lobster : GameCharacters
{
	private float maxDistance = 10.0f;

	private Transform target;
	private Transform enemyTransform;

	private bool playerDead = false;

	private Vector3 startPos;
	private Vector3 dir;
	private Vector3 currentDir;
		
	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();
		startPos = enemyTransform.position;

	//Set from parent
		facingRight = false;
		moveSpeed = 2.0f;
		health = 3;
		armor = 4;
	}

	void FixedUpdate ()
	{
		if(!playerDead) 																	//Only run this if the game is not over - so basically as long as Wrahh is alive :)
		{
			target = GameObject.FindWithTag("Player").transform; 							//Assign the target to be the whatever object with the tag; "Player"
			currentDir = enemyTransform.position;
		
			if (currentDir.x < target.position.x &! facingRight)
				flip();
				
			if (currentDir.x > target.position.x && facingRight)
				flip();
		
			if (Vector3.Distance(target.position, enemyTransform.position) < maxDistance)	//If the distance between the target and the enemy is less than the maximum, chaseTarget will be called!
				chaseTarget();
			else if (Vector3.Distance(enemyTransform.position , startPos) > 0.1f)
				returnToStartPos();															//Otherwise the enemy should return to it's initial position.
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player") 																	//If the collission is with the game obejct tagged; "Player"..
			Debug.Log("Lobster kills the player");												//When playerDead is true, the enemy stops moving because it killed the player.
	}

	void chaseTarget()
	{
		dir = target.position - enemyTransform.position; 									//The enemy's diretion
		dir.Normalize();																	//Normalize the direction vector
		enemyTransform.position += dir * moveSpeed * Time.deltaTime; 						//CHASE THE PLAYER!!!!!
	}

	void returnToStartPos()
	{
		if(enemyTransform.position.x < startPos.x &! facingRight) 							//Must face starting position
		{
			flip();
			enemyTransform.position += enemyTransform.right * moveSpeed * Time.deltaTime;	//Enemy returns to start position
		}
	}	
}
