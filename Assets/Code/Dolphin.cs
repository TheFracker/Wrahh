using UnityEngine;
using System.Collections;

public class Dolphin : GameCharacters
{
	Weapon weapon;

	public Transform positionedTarget_right = null;
	public Transform positionedTarget_left = null;
	private Transform target;
	public bool stopRight = true;
	public bool stopLeft = true;
	bool gotHere;
	float timeToWalk;

	public float maxDistance = 20.0f;
	public float shootDistance = 10.0f;
	public float closeDistance = 0.7f;
	public float heightDistance = 0.4f;
	
	private bool playerDead = false;

	private Vector3 dir;

	Animator anim;
	
	void Start ()
	{
		weapon = gameObject.AddComponent<Rifle> ();

		gotHere = false;
		timeToWalk = 0;
		//From parent "GameCharacters.cs":
		moveSpeed = 10;
		facingRight = false;
		health = 3;
		setStandardPhysics();


	}

	void FixedUpdate()
	{
		if (DolphineDeath.isCrushed == false)
		{
			//Assign the target to be the whatever object with the tag; "Player"
			target = GameObject.FindWithTag("Player").transform;

			bool withinSameHeight = this.transform.position.y + heightDistance >= target.position.y && this.transform.position.y - heightDistance <= target.position.y;

			// If I (the dolphin) can see the escapen prisonar (Wrahh) and I'm not too far away, I will start chasing him.
			if ((Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x > target.position.x && withinSameHeight &! facingRight)
			    || (Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x < target.position.x && withinSameHeight && facingRight))
				useWeapon(weapon);
			else if((Vector3.Distance(target.position, this.transform.position) <= maxDistance && this.transform.position.x > target.position.x && withinSameHeight &! facingRight)
			        || (Vector3.Distance(target.position, this.transform.position) <= maxDistance && this.transform.position.x < target.position.x && withinSameHeight && facingRight))
				chaseTarget();
			//else if (Vector3.Distance(target.position, this.transform.position) <= closeDistance)
			//	chaseTarget();
			else
				guard ();
		// If close enough I will shoot at him
		}

		if (DolphineDeath.isCrushed == true)
		{
			this.collider2D.enabled = false;
		}
	}

	public void hurt(Projectile p)
	{
		int damageTaken = p.giveDamage();
		health -= damageTaken;
		if(health <= 0)
			die ();
	}

	void useWeapon(Weapon w)
	{
		w.shoot ();
	}

	int o = 0;
	void guard()
	{
		// Guard right
		if(positionedTarget_right == null){
			Vector2 point = new Vector2 (5, 2);
			if(this.transform.position.x < point.x && facingRight)
				this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > point.x)
			{
				if(stopRight && !gotHere)
				{
					gotHere = true;
					timeToWalk = Time.time + Random.Range(2,6);
				}
				else if(timeToWalk < Time.time)
				{
					flip();
					gotHere = false;
				}
			}
		}
		else if (positionedTarget_right != null)
		{
			if(this.transform.position.x < positionedTarget_right.position.x && facingRight)
				this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > positionedTarget_right.position.x)
			{
				if(stopRight && !gotHere)
				{
					gotHere = true;
					timeToWalk = Time.time + Random.Range(2,6);
				}
				else if(timeToWalk < Time.time)
				{
					flip();
					gotHere = false;
				}
			}
		}
		
		// Guard left
		if(positionedTarget_left == null)
		{
			Debug.Log (1);
			Vector2 point = new Vector2(-5,2);
			if(this.transform.position.x > point.x &! facingRight)
				this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < point.x)
			{
				if(stopLeft && !gotHere)
				{
					gotHere = true;
					timeToWalk = Time.time + Random.Range(2,6);
				}
				else if(timeToWalk < Time.time)
				{
					flip();
					gotHere = false;
				}
			}
		}
		else if (positionedTarget_left != null)
		{
			if(this.transform.position.x > positionedTarget_left.position.x &! facingRight)
				this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < positionedTarget_left.position.x)
			{
				if(stopLeft && !gotHere)
				{
					gotHere = true;
					timeToWalk = Time.time + Random.Range(2,6);
				}
				else if(timeToWalk < Time.time)
				{
					flip();
					gotHere = false;
				}
			}
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

}
