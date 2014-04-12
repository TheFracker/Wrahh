using UnityEngine;
using System.Collections;

public class Lobster : MonoBehaviour
{
	public float speed = 6.0f;
	public float maxDistance = 20.0f;

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
		//Only run this if the game is not over - so basically as long as Wrahh is alive :)
		if(!playerDead)
		{
		//Assign the target to be the whatever object with the tag; "Player"
			target = GameObject.FindWithTag("Player").transform;
			currentDir = enemyTransform.position;
		
		//Flip according to player's direction
			if (enemyTransform.position.x < target.position.x &! facingRight)
				flip ();
			if (enemyTransform.position.x > target.position.x && facingRight)
				flip ();

		//If the distance between the target and the enemy is less than the maximum, chaseTarget will be called!
			if (Vector3.Distance(target.position, enemyTransform.position) < maxDistance)
				chaseTarget();
			else //Otherwise the enemy should return to it's initial position.
				returnToStartPos();
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
		dir.Normalize(); //Normalize the direction vector
		enemyTransform.position += dir * speed * Time.deltaTime; //CHASE THE PLAYER!!!!!
	}

	void flip() //The lobster should face the player
	{
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
		Debug.Log ("FLIPPED!"); 					//Just to make sure it works so far..
	}

	void returnToStartPos()
	{
		if(enemyTransform.position.x < startPos.x &! facingRight) //Must face starting position
		{
			flip ();
			enemyTransform.position += enemyTransform.right * speed * Time.deltaTime;
		}
		Debug.Log("The enemy returned");			//Just to make sure it works so far..
	}
}
