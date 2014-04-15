using UnityEngine;
using System.Collections;

public class Wrahh : GameCharacters
{
	int lobsterParts;
	int gunsCollected;
	int riflesCollected;
	bool shieldOn;


	Shield shield;

	public static bool canCrushEnemy = false;



	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;
	
	private float currentSpeed;								// set to public to see current speed
	private float standardDrag = 5f; 						// initial drag force
	private float climbSpeed = 5f;

	Animator anim; 										 	// Variable of the typ "Animator" to acces the Animator later

	public GameObject defaultPrefab, shieldPrefab, helmetPrefab, shieldAndHelmetPrefab;
	private GameObject prefab;

	//////////////////////////////////
	// START 			    		//
	//////////////////////////////////
	void Start ()
	{
		shieldOn = false;
		grenades = 0;
		riflesCollected = 5;
		gunsCollected = 5;
		lobsterParts = 10;
		health = 3;

		shield = gameObject.AddComponent<Shield>();
		currentWeapon = gameObject.AddComponent<Rifle>();
		anim = GetComponent<Animator>();
		prefab = defaultPrefab;
	

		//From parent:
		moveSpeed = 10000.0f;
		facingRight = true;
		MAX_MOVE_SPEED = 30.0f;
		setStandardPhysics(this.gameObject);

		//this.rigidbody2D.gravityScale = standardGravity;

	}
	

	//////////////////////////////////
	// UPDATE 	    				//
	//////////////////////////////////
	void Update ()
	{
	// Throw grenade
		if (Input.GetKeyUp(KeyCode.G))
			throwGrenade ();

	// Shooting
		if (Input.GetKeyUp (KeyCode.Space))
			useWeapon (currentWeapon);
		Debug.Log(armor + "aljlfajlhsafklhsafklsafklj");
	}


	////////////////////////////////////////////
	// FixedUpdate - used for movement		  //
	////////////////////////////////////////////
	void FixedUpdate()
	{
		float input = 0;																	// creates a local variable "input"
		falling ();

			if (anim.GetBool("IsFalling") == false && anim.GetBool("HitGround") == false){	// checks if the player is not falling or splatted out
				input = Input.GetAxis ("Horizontal"); 										//local variable (a float going from -1 - 1) depending on if you push "A"/"left key" or "D"/"right key" 
				climbingLadder();															// runs the "climbingLadder" function 
				crawlMonkeyBars(); 															// runs the "crawlMonkyBars" function 
			}
		
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);
		
		anim.SetFloat("Speed", Mathf.Abs(input)); // The "speed" parameter in the Animator gets values from the variable "input" 

	// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();

		if (input > 0 && !facingRight)
			flip ();
		
		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	// MONKEY BAR CRAWL - controlls physics and animtion when the player gets on or off the monkey bars	    //
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	void crawlMonkeyBars()	//controlls physics and animtion when the player gets on or off the monkey bars
	{
		//checks if the boolean from "MonkeyBars.cs" are true
		if (MonkeyBars.onMonkeyBar == true)
		{
			anim.SetBool("Crawling", true); //The "Crawling" parameter in the Animator gets the value true to start crawling animations 
			this.rigidbody2D.gravityScale = 0; //sets gravity to 0, so it simulates if the player was hanging in the arms
			this.rigidbody2D.drag = 25; //Sets the drag up, to make it feel like there is som ressistens and you are not in a zero gravity space 
		}

		//checks if the boolean from "MonkeyBars.cs" are false
		else if (MonkeyBars.onMonkeyBar == false)
		{
			anim.SetBool("Crawling", false); //The "Crawling" parameter in the Animator gets the value false to stop crawling animations 
			this.rigidbody2D.gravityScale = standardGravity; //sets gravity to initial
			this.rigidbody2D.drag = standardDrag; //sets drag to initial
		}
	}

	//////////////////////////////////////////////
	// CLIMB LADDEER							//
	/////////////////////////////////////////////
	void climbingLadder()
	{
		if (Ladder.canClimb == true)
		{

			this.rigidbody2D.velocity = new Vector2(0,climbSpeed);
		}
	}

	//////////////////////////////////////
	// FALLING							//
	//////////////////////////////////////
	void falling()
	{
		if (this.rigidbody2D.velocity.y < -2.5)
		{
			anim.SetBool("IsFalling", true);
			canCrushEnemy = true;
		}

		if (this.rigidbody2D.velocity.y > -0.5 && anim.GetBool("IsFalling") == true)
		{
			anim.SetBool("HitGround", true);
			StartCoroutine(waitForFallingAnimation());
		}
	}
	

	IEnumerator waitForFallingAnimation(){
		yield return new WaitForSeconds(1f);
		anim.SetBool("HitGround", false);
		anim.SetBool("IsFalling", false);
		canCrushEnemy = false;
	}


	//////////////////////////////////////
	// WEAPONS							//
	//////////////////////////////////////
	void useWeapon(Weapon currentWeapon)
	{
		Debug.Log ("Hitting with this weird club");
		currentWeapon.hit ();
	}

	void throwGrenade()
	{
		if (grenades > 0)
		{
			Debug.Log("Throwing grenade " + grenades);
			grenades--;
			return;
		}
		Debug.Log ("Don't have anything to throw");
	}

	//////////////////////////////////////
	// PICK UP ITEMS					//
	//////////////////////////////////////
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Weapon")													// Pick up weapon
		{
			Debug.Log ("Picking up this weapon");
			Destroy(c.gameObject);
		}

		if (c.tag == "Armor")													// Pick up armor
		{																		
			if(c.gameObject.name == "shield")
			{
				Debug.Log ("Shield obtained!");									// Check if 'shield' was registered
				prefab = shieldPrefab;											// Store new prefab in variable
				Destroy(gameObject);											// Delete old game object
				Instantiate (prefab, transform.position, Quaternion.identity);	// Change to new prefab
				shield.Protection = 1;
				shield.Durabillity = 1;
				Destroy(c.gameObject);// Removed the item from the scene
				shieldOn = true;
				Debug.Log(shieldOn);
			}
		}
	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
	}
	
	public void hurt(Projectile p)
	{
		int damageTaken = p.giveDamage ();
		if (armor > 0 && armor > damageTaken)
		{
			armor -= damageTaken;
			damageTaken = 0;
		}
		else if (armor > 0)
		{
			damageTaken -= armor;
			armor = 0;
		}
		health -= damageTaken;
		if (health <= 0)
			die (this.gameObject);
	}

	public int Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
		}
	}

	public int Armor
	{
		get
		{
			return armor;
		}
		set
		{
			armor = value;
		}
	}

	public int LobsterParts
	{
		get
		{
			return lobsterParts;
		}
		set
		{
			lobsterParts = value;
		}
	}

	public int RiflesCollected
	{
		get
		{
			return riflesCollected;
		}
		set
		{
			riflesCollected = value;
		}
	}

	public int GunsCollected
	{
		get
		{
			return gunsCollected;
		}
		set
		{
			gunsCollected = value;
		}
	}
	public bool ShieldOn
	{
		get
		{
			return shieldOn;
		}
		set
		{
			shieldOn = value;
		}
	}

}
