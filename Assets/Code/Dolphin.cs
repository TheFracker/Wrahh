using UnityEngine;
using System.Collections;

public class Dolphin : GameCharacters
{
	Weapon weapon;													// The weapon that the dolphin carry with it

	GameObject rifleDrop;											// This is the Dolphins rifle drop item
	GameObject gunDrop;												// This is the Dolphins gun drop item
	GameObject healthDrop;
	private Transform enemyTransform;								// Stores position of this object

																	// Can be used for setting up the positions that a dolphin should walk between. Two positions is already set in code, but with these, they can be manually set on the fly
	public Transform positionedTarget_right = null;					// This is for the right position
	public Transform positionedTarget_left = null;					// This is for the left position
	private Transform target;										// This is used for finding the target, in this case that would be Wrahh
																	// It also possible to make the dolphin take a break when it reaches its end position
	public bool stopRight = true;									// Will make it stop at the right position
	public bool stopLeft = true;									// Will make it stop at the left position
	private bool gotHere;											// Is used to check if the dolphin got to the end of the position
	private float timeToWalk;										// Is used to check if it is time to start walking again after a break

	public float maxDistance = 20.0f;								// This is the distance that the target has to be inside for the dolphin to start chasing is
	public float shootDistance = 10.0f;								// This is the distance that the target has to be inside fot the dolphin to start shooting at it
	public float heightDistance = 0.4f;								// This is used to check if the target and the dolphin are on the same platform.

	private Vector3 dir;											// Is used to find the direction in which to walk

	public bool usesPistol;											// Check to indicate what type of weapon the dolphin carry
 	public bool usesRifle;											// Check to indicate what type of weapon the dolphin carry

	public static AudioSource[] gunFire = new AudioSource[2];

	void Start ()
	{
		gunFire[0] = gameObject.AddComponent<AudioSource>() as AudioSource;
		gunFire[1] = gameObject.AddComponent<AudioSource>() as AudioSource;
		gunFire[0].clip = Resources.Load("sounds/gunshot1") as AudioClip;
		gunFire[1].clip = Resources.Load("sounds/gunshot1") as AudioClip;

		enemyTransform = this.GetComponent<Transform>();	

		// Adds rifle and gun and health drop prefabs
		rifleDrop = (GameObject)Resources.Load("Prefabs/riflePickUp");
		gunDrop = (GameObject)Resources.Load("Prefabs/gunPickUp");
		healthDrop = (GameObject)Resources.Load("Prefabs/Items/healthBuff");

		// Checks what weapon the dolphin carries and attach the correct script and shows the correct weapon
		if(usesRifle){
			weapon = gameObject.AddComponent<Rifle> ();
			this.transform.FindChild("_dolphin").transform.FindChild("weapon_rifle").gameObject.SetActive(true);
		}
		if(usesPistol){
			weapon = gameObject.AddComponent<Pistol> ();
			this.transform.FindChild("_dolphin").transform.FindChild("weapon_gun").gameObject.SetActive(true);
		}

		// Is set to false, so that the dolphin can stop the first time it comes to a position, and time to walk is set to 0 as it is set when it stops for a break
		gotHere = false;
		timeToWalk = 0;

		//From parent "GameCharacters.cs":
		moveSpeed = 7;
		facingRight = false;
		health = 20;
		setStandardPhysics();
	}

	void FixedUpdate()
	{
		if (EnemySplat.isCrushed == false)							// If the dolphin has not been crushed by Wrahh it will guard an area, chase Wrahh and shoot at him
		{
			target = GameObject.FindWithTag("Player").transform;	//Assign the target to be the whatever object with the tag; "Player" - in this case Wrahh

			// Is true if the dolphin and Wrahh are on the same platform
			bool withinSameHeight = this.transform.position.y + heightDistance >= target.position.y && this.transform.position.y - heightDistance <= target.position.y;
			bool tooClose = withinSameHeight && Mathf.Abs(this.transform.position.x - target.transform.position.x) < shootDistance - 0.4f;
			// If the dolphin and Wrahh are within a certain distance of eachother, the dolphin will chase Wrahh. If the dolphin and Wrahh are closer to each other, the dolphin will
			// shoot to kill. If Wrahh is too far away for the dolphin to see, the dolphin will just guard an area.
			if (tooClose && ((Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x > target.position.x && withinSameHeight &! facingRight)
			    || (Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x < target.position.x && withinSameHeight && facingRight)))
			{
				moveAway();
				useWeapon(weapon);
			}
			else if ((Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x > target.position.x && withinSameHeight &! facingRight)
			    || (Vector3.Distance(target.position, this.transform.position) <= shootDistance && this.transform.position.x < target.position.x && withinSameHeight && facingRight))
				useWeapon(weapon);
			else if((Vector3.Distance(target.position, this.transform.position) <= maxDistance && this.transform.position.x > target.position.x && withinSameHeight &! facingRight)
			        || (Vector3.Distance(target.position, this.transform.position) <= maxDistance && this.transform.position.x < target.position.x && withinSameHeight && facingRight))
				chaseTarget();
			else
				guard ();
		}

	}

	// This makes the dolphin able to take damage from projectiles hitting it. And will also check if the health of the dolphin is below 0 so that it will die
	public void hurt(Projectile p)
	{
		base.blinkRed();
		health -= p.giveDamage();
		if(health <= 0)
			die(this.gameObject);
	}
	
	protected override void die (GameObject g)
	{
		//If dolphin uses rifle on death it will drop an riflePickUp object
		if (usesRifle)
		{ 
			GameObject itemDrop = (GameObject)GameObject.Instantiate(rifleDrop, Vector2.zero, Quaternion.identity); // Instanciate a new gameobject based on the prefab of the rifle pick up
			itemDrop.transform.Translate(enemyTransform.position.x + 1, enemyTransform.position.y + 1.5f, enemyTransform.position.z); // Place the dropped item at the dolphin last known position with a little offset
			base.die (this.gameObject);
		}

		//If dolphin uses rifle on death it will drop an riflePickUp object
		if(usesPistol)
		{
			GameObject itemDrop1 = (GameObject)GameObject.Instantiate(gunDrop, Vector2.zero, Quaternion.identity); // Instanciate a new gameobject based on the prefab of the gun pick up
			itemDrop1.transform.Translate(enemyTransform.position.x + 1, enemyTransform.position.y + 1.5f, enemyTransform.position.z); // Place the dropped item at the dolphin last known position with a little offset
			base.die (this.gameObject);
		}
		if(Random.Range(0,100) < 50)
		{
			GameObject itemDrop2 = (GameObject)GameObject.Instantiate(healthDrop, Vector2.zero, Quaternion.identity); // Instanciate a new gameobject based on the prefab of the health buff
			itemDrop2.transform.Translate(enemyTransform.position.x + 0.5f, enemyTransform.position.y + 1f, enemyTransform.position.z); // Place the dropped item at the dolphin last known position with a little offset
			base.die (this.gameObject);
		}
	}

	// Uses the weapon that the dolphin is carrying 
	void useWeapon(Weapon w)
	{
		w.shoot ();
	}
	
	void guard()
	{
		// Guard right
		// If no postion has been set manually, it will walk to set distance, and walk back again in the other direction when it is reached
		if(positionedTarget_right == null){
			Vector2 point = new Vector2 (5, 2);
			if(this.transform.position.x < point.x && facingRight)
				this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > point.x)
			{
				// Checks if it should stop at the right position, and if it should it will set it to start walking after after a random amount of time between 2 (inclusive)
				// 5 (inclusive) seconds
				// If it is not set to stop, it will just turn around and start walking in the other direction
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
				if(!stopRight)
					flip ();
			}
		}
		// If a position has been manually set, it will walk to that position and walk back again in the other direction when it is reached
		else if (positionedTarget_right != null)
		{
			if(this.transform.position.x < positionedTarget_right.position.x && facingRight)
				this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x > positionedTarget_right.position.x)
			{
				// Checks if it should stop at the right position, and if it should it will set it to start walking after after a random amount of time between 2 (inclusive)
				// 5 (inclusive) seconds
				// If it is not set to stop, it will just turn around and start walking in the other direction
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
				if(!stopRight)
					flip ();
			}
		}
		
		// Guard left
		if(positionedTarget_left == null)
		{
			Vector2 point = new Vector2(-5,2);
			if(this.transform.position.x > point.x &! facingRight)
				this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < point.x)
			{
				// Checks if it should stop at the left position, and if it should it will set it to start walking after after a random amount of time between 2 (inclusive)
				// 5 (inclusive) seconds
				// If it is not set to stop, it will just turn around and start walking in the other direction
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
				if(!stopLeft)
					flip ();
			}
		}
		else if (positionedTarget_left != null)
		{
			if(this.transform.position.x > positionedTarget_left.position.x &! facingRight)
				this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
			if(this.transform.position.x < positionedTarget_left.position.x)
			{
				// Checks if it should stop at the left position, and if it should it will set it to start walking after after a random amount of time between 2 (inclusive)
				// 5 (inclusive) seconds
				// If it is not set to stop, it will just turn around and start walking in the other direction
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
				if(!stopLeft)
					flip ();
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

	void moveAway()
	{
		//The enemy's diretion
		dir = target.position - this.transform.position;
		dir.Normalize(); //Normalize the direction vector
		this.transform.position += dir * -moveSpeed * 0.4f * Time.deltaTime; //GET AWAY FROM THE PLAYER!!!!!
	}
}
