using UnityEngine;
using System.Collections;

	/////////////////////////////////////////////////////////////
	// TO DO:
	// -----
	// CHECK FROM WRAHH's SCRIPT IF HE IS DEAD!
	// Be able to kill Wrahh
	// Be able to get killed
	// Less important: Be able to spawn
	/////////////////////////////////////////////////////////////

public class Lobster : MonoBehaviour
{
	private float speed = 10.0f;
	private float maxDistance = 20.0f;

	private Transform target;
	private Transform enemyTransform;

	private bool playerDead = false;
	private bool facingRight;
	
	private Vector3 startPos;
	private Vector3 dir;
	private Vector3 currentDir;

	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();
		facingRight = false;
		startPos = enemyTransform.position;
	}

	void FixedUpdate ()
	{
		if(!playerDead) 																	//Only run this if the game is not over - so basically as long as Wrahh is alive :)
		{
			target = GameObject.FindWithTag("Player").transform; 							//Assign the target to be the whatever object with the tag; "Player"
			currentDir = enemyTransform.position;
		
			if (enemyTransform.position.x < target.position.x &! facingRight)				//Flip according to player's direction
				flip ();
			if (enemyTransform.position.x > target.position.x && facingRight)
				flip ();
		
			if (Vector3.Distance(target.position, enemyTransform.position) < maxDistance)	//If the distance between the target and the enemy is less than the maximum, chaseTarget will be called!
				chaseTarget();
			else 
				returnToStartPos();															//Otherwise the enemy should return to it's initial position.
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player") 																//If the collission is with the game obejct tagged; "Player"..
		{
			playerDead = true; 																//When playerDeadr is true, the enemy stops moving because it killed the player.
			Wrahh.die();
		}
	}

	void chaseTarget()
	{
		dir = target.position - enemyTransform.position; 									//The enemy's diretion
		dir.Normalize();																	//Normalize the direction vector
		enemyTransform.position += dir * speed * Time.deltaTime; 							//CHASE THE PLAYER!!!!!
	}

	void flip() 																			//The lobster should face the player
	{
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
		Debug.Log ("FLIPPED!"); 															//Just to make sure it works so far..
	}

	void returnToStartPos()
	{
		if(enemyTransform.position.x < startPos.x &! facingRight) 							//Must face starting position
		{
			flip ();
			enemyTransform.position += enemyTransform.right * speed * Time.deltaTime;
		}
		Debug.Log("The enemy returned");
	}
}
