using UnityEngine;
using System.Collections;

public class Lobster : MonoBehaviour
{
	public float speed = 6.0f;
	public float maxDistance = 20.0f;

	Transform target;
	Transform enemyTransform;

	private bool playerDead = false;
	private bool facingRight;

	Vector2 startPos;
	Vector3 dir;
	
	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();
		facingRight = false;
		startPos = enemyTransform.position;
	}

	void FixedUpdate ()
	{
		//Only run this if the game is not over - so basically as long as Wrahh is alive :)
		if(!playerDead)
		{
		
		//Assign the target to be the whatever object with the tag; "Player"
			target = GameObject.FindWithTag("Player").transform;
		
		//Flip according to player's direction
			if (enemyTransform.position.x < target.position.x &! facingRight)
				flip ();
			if (enemyTransform.position.x > target.position.x && facingRight)
				flip ();

		//If the distance between the target and the enemy is less than the maximum, chaseTarget will be called!
			if (Vector3.Distance(target.position, enemyTransform.position) < maxDistance)
				chaseTarget();
			else //Otherwise the enemy should return to it's initial position.
			{
				returnToStartPos();
				Debug.Log("The lobster returned");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player") //If the collission is with the game obejct tagged; "Player"..
		{
			Debug.Log("Lobster touched Wrahh!"); //Just to make sure it works so far..
			playerDead = true; //When playerDeadr is true, the enemy stops moving because it killed the player.
			Debug.Log("The player was killed!"); //Just to make sure it works so far..
		}
	}

	void chaseTarget()
	{
	//The enemy's diretion
		dir = target.position - enemyTransform.position;
		dir.Normalize(); //We normalize the direction vector
		enemyTransform.position += dir * speed * Time.deltaTime; //CHASE THE PLAYER!!!!!
	}

	void flip()
	{
		Debug.Log ("FLIPPED!"); 					//Just to make sure it works so far..
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
	}

	void returnToStartPos()
	{
		enemyTransform.position = startPos;
	}
}
