using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour
{
	bool facingRight;

	int health = 0; // Three Lives
	int armor = 0; // No armor to begin with

	int lobsterParts = 5;
	int gunsCollected = 10;
	int riflesCollected = 5;

	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;

	private float moveSpeed = 10000.0f; 					// initial move force
	private float currentSpeed; 							// set to public to see current speed
	private float MAX_MOVE_SPEED = 3.0f; 					// initial max speed
	private float standardGravity = 7.42f; 					// initial gravity
	private float standardDrag = 5f; 						// initial drag force
	private float climbSpeed = 5f;

	Animator anim; 										 	// Variable of the typ "Animator" to acces the Animator later

	public GameObject defaultPrefab, shieldPrefab, helmetPrefab;
	private GameObject prefab;

	//////////////////////////////////
	// START 			    		//
	//////////////////////////////////
	void Start ()
	{
		health = 3;
		facingRight = true;
		grenades = 0; 										// Nothing to throw yet
		currentWeapon = gameObject.AddComponent<Rifle>();
		anim = GetComponent<Animator>();
		prefab = defaultPrefab;
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
	}

<<<<<<< HEAD
	////////////////////////////////////////////
	// FixedUpdate - used for movement		  //
	////////////////////////////////////////////
	void FixedUpdate(){

		float input = 0;																// creates a local variable "input"



		falling ();
		Debug.Log(this.rigidbody2D.velocity.y);

		if (anim.GetBool("IsFalling") == false && anim.GetBool("HitGround") == false){	// checks if the player is not falling or splatted out
			input = Input.GetAxis ("Horizontal"); 										//local variable (a float going from -1 - 1) depending on if you push "A"/"left key" or "D"/"right key" 
			climbingLadder();															// runs the "climbingLadder" function 
			crawlMonkeyBars(); 															// runs the "crawlMonkyBars" function 
		}

		anim.SetFloat("Speed", Mathf.Abs(input)); 										// The "speed" parameter in the Animator gets values from the variable "input" 


=======
	void FixedUpdate()
	{
		float input = 0;
		falling ();
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f

		if (anim.GetBool("IsFalling") == false)
			input = Input.GetAxis ("Horizontal"); //local variable (a float going from 0-1)
		
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

<<<<<<< HEAD
		currentSpeed = rigidbody2D.velocity.x; 											//sets the "currentSpeed" to the movement speed in the x-axis
=======
		anim.SetFloat("Speed", Mathf.Abs(input)); // The "speed" parameter in the Animator gets values from the variable "input" 
		currentSpeed = rigidbody2D.velocity.x; //sets the "currentSpeed" to the movement speed in the x-axis
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f

	// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();

		if (input > 0 && !facingRight)
			flip ();

<<<<<<< HEAD
		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	// MONKEY BAR CRAWL - controlls physics and animtion when the player gets on or off the monkey bars	    //
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	void crawlMonkeyBars(){

=======
		climbingLadder();
		crawlMonkeyBars(); // runs the "crawlMonkyBars" function 

	//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}

	void crawlMonkeyBars()	//controlls physics and animtion when the player gets on or off the monkey bars
	{
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
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

<<<<<<< HEAD

	//////////////////////////////////////////////
	// CLIMB LADDEER							//
	/////////////////////////////////////////////
	void climbingLadder(){

		if (Ladder.canClimb == true){
=======
	void climbingLadder()
	{
		if (Ladder.canClimb == true)
		{
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
			Debug.Log("im should move now");
			this.rigidbody2D.velocity = new Vector2(0,climbSpeed);
		}
	}

<<<<<<< HEAD

	//////////////////////////////////////
	// FALLING							//
	//////////////////////////////////////
	void falling(){
		if (this.rigidbody2D.velocity.y < -2.5f){
=======
	void falling()
	{
		if (this.rigidbody2D.velocity.y < -2.5)
		{
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
			anim.SetBool("IsFalling", true);
		}
<<<<<<< HEAD

		if (this.rigidbody2D.velocity.y > -0.5f && anim.GetBool("IsFalling") == true){
			Debug.Log("I get here");
=======
		if (this.rigidbody2D.velocity.y > -0.5 && anim.GetBool("IsFalling") == true)
		{
			anim.SetBool("IsFalling", false);
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
			anim.SetBool("HitGround", true);
			StartCoroutine(waitForFallingAnimation());
		}
<<<<<<< HEAD
	}

	IEnumerator waitForFallingAnimation(){
		yield return new WaitForSeconds(1f);
		anim.SetBool("HitGround", false);
		anim.SetBool("IsFalling", false);
=======
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
	}


	//////////////////////////////////////
	// WEAPONS							//
	//////////////////////////////////////
	void useWeapon(Weapon currentWeapon)
	{
		Debug.Log ("Hitting with this weird club");
		currentWeapon.shoot ();
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

<<<<<<< HEAD
	//////////////////////////////////
	// DIE							//
	//////////////////////////////////
	void die()
=======
	public static void die()
>>>>>>> 92f4f63800def4c9088604635d4dc1636d7d929f
	{
		Debug.Log ("Dying");
		///////////////////////////////////////
		// TO-DO:
		// ---
		// Make respawn point and function
		// Decrease points
		///////////////////////////////////////
	}

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
				armor += 3;														// Add additional armor to the player
				Destroy(c.gameObject);											// Removed the item from the scene
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
			die ();
	}

	public int getHealth()
	{
		return health;
	}

	public int getArmor()
	{
		return armor;
	}

	public int getLobsterParts()
	{
		return lobsterParts;
	}

	public int getRiflesCollected()
	{
		return riflesCollected;
	}

	public int getGunsCollected()
	{
		return gunsCollected;
	}

}
