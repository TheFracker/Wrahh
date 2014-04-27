using UnityEngine;
using System.Collections;

public class Lobster : GameCharacters
{
	private float maxDistance = 10.0f;										// This is the scope for the lobster's field of sight

	private Transform target;
	private Transform enemyTransform;
	private GameObject lobsterPart;											// This is the lobster's drop item
	private GameObject wrahh;

	private Vector3 startPos;
	private Vector3 dir;
	private Vector3 currentDir;

	protected Vector3 pos; 													// The position where the projectile should spawn
	protected GameObject hitProjectile; 									// Projectile created when hitting with the weapon
	private bool attacking;
		
	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();					// Gets the Tranform of this gameobjects (lobster)
		lobsterPart = (GameObject)Resources.Load("Prefabs/lobsterParts");	// Collecting the lobsterPart prefab from the assets
		wrahh = GameObject.FindWithTag("Player");
		startPos = enemyTransform.position;									// Stores the lobster's initial position in an attribute/variable

	//Set from parent (super) class :: GameCharacters
		facingRight = false;
		moveSpeed = 2.0f;
		health = 3;
		armor = 4;

		attacking = false;
		hitProjectile = Resources.Load ("Prefabs/LobsterProjectile") as GameObject;
	}

	void FixedUpdate ()
	{
		// If the lobster has not been crushed by Wrahh and Wrahh is still alive the lobster will chase Wrahh
		if(wrahh != null && EnemySplat.isCrushed == false)
		{
			target = GameObject.FindWithTag("Player").transform; 							// Assign the target to be the whatever object with the tag; "Player"
			currentDir = enemyTransform.position;
		
			if (currentDir.x < target.position.x &! facingRight)							// Flipping the lobster according to its' target's position
				flip();
			if (currentDir.x > target.position.x && facingRight)
				flip();
		
			if (Vector3.Distance(target.position, enemyTransform.position) < maxDistance)	// If the distance between the target and the enemy is less than the maximum, chaseTarget will be called!
				chaseTarget();
			else if (Vector3.Distance(enemyTransform.position , startPos) > 0.1f)
				returnToStartPos();															// Otherwise the enemy should return to it's initial position.
		}
	}

	void OnCollisionEnter2D (Collision2D c)													// Check for any collission
	{
		if (c.collider.tag == "Player")														// If the collission is with the game obejct tagged; "Player"..
		{
			if(!attacking)																	// Check whether the enemy is attacking on collission
			{
				attacking = true;															// The enemy is in "attacking" state
				StartCoroutine("attack");												// Wait for an attack to finish before moving on
			}
		}
		if (c.collider.tag == "HitProjectile")
		{
			GameObject itemDrop = (GameObject)GameObject.Instantiate(lobsterPart, Vector2.zero, Quaternion.identity); // Instanciate a new gameobject based on the prefab of the lobster parts
			itemDrop.transform.Translate(enemyTransform.position.x + 1, enemyTransform.position.y + 1.5f, enemyTransform.position.z); // Place the dropped item at the lobster's last known position with a little offset
			die(this.gameObject);															// Remove the dead lobster from the game
		}
	}

	// This method enables the lobster to chase its enemy (the player)
	void chaseTarget()
	{
		dir = target.position - enemyTransform.position; 									// The enemy's diretion will be in the direction of the player (target)
		dir.Normalize();																	// Normalize the direction vector (because math...)
		enemyTransform.position += dir * moveSpeed * Time.deltaTime; 						// Move the enemy in the target's direction
	}

	// This method makes the enemy return to its original position
	void returnToStartPos()
	{
		if(enemyTransform.position.x < startPos.x &! facingRight) 							// Must face starting position so it is not moonwalking
		{
			flip();																			// If it is moonwalking, flip it!
			enemyTransform.position += enemyTransform.right * moveSpeed * Time.deltaTime;	// Move the enemy to its start position..
		}
	}
	
	// When the lobster is close enough to the player to start attacking, this coroutine will be started. It essentially gives a delay to attacks, so that
	// Wrahh has a larger chance of surviving its attack.
	IEnumerator attack()
	{
		while(attacking)
		{
			yield return new WaitForSeconds(0.5f);
			if(isFacingRight())
				pos = this.transform.position + new Vector3(1.0f,0.5f,0);
			else
				pos = this.transform.position + new Vector3(-1.0f,0.5f,0);
			Instantiate(hitProjectile, pos, Quaternion.identity);
			attacking = false;
		}
	}
}
