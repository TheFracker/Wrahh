using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour
{
	bool facingRight;

	int health;
	int armor;

	int lobsterParts;
	int gunsCollected;
	int riflesCollected;

	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;

	private float moveSpeed = 10000.0f; // initial force
	private float currentSpeed; // set to public to see current speed
	private float MAX_MOVE_SPEED = 3.0f; // initial max speed
	private float standardGravity = 7.42f; // initial gravity
	private float standardDrag = 5f; // initial drag force

	Animator anim; // Variable of the typ "Animator" to acces the Animator later

	public GameObject defaultPrefab, shieldPrefab;
	private GameObject prefab;
	
	void Start ()
	{
		facingRight = true;
		health = 3;  										// Three hearts
		armor = 0; 											// No armor to start with
		grenades = 0; 										// Nothing to throw yet
		currentWeapon = gameObject.AddComponent<Rifle>();
		anim = GetComponent<Animator>();
		prefab = defaultPrefab;
	}

	void Update ()
	{
		// Throw grenade
		if (Input.GetKeyUp(KeyCode.G))
			throwGrenade ();

		// Shooting
		if (Input.GetKeyUp (KeyCode.Space))
			useWeapon (currentWeapon);
	}

	void FixedUpdate(){

		float input = Input.GetAxis ("Horizontal"); //local variable (a float going from 0-1)

		anim.SetFloat("Speed", Mathf.Abs(input)); // The "speed" parameter in the Animator gets values from the variable "input" 
		anim.SetFloat("FallingSpeed", Mathf.Abs(rigidbody2D.velocity.y)); // The "speed" parameter in the Animator gets values from the variable "input"
		
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED){
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);
		}

		currentSpeed = rigidbody2D.velocity.x; //sets the "currentSpeed" to the movement speed in the x-axis

		// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();

		if (input > 0 && !facingRight)
			flip ();

		crawlMonkeyBars(); // runs the "crawlMonkyBars" function 

		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}

	//controlls physics and animtion when the player gets on or off the monkey bars
	void crawlMonkeyBars(){

		//checks if the boolean from "MonkeyBars.cs" are true
		if (MonkeyBars.onMonkeyBar == true){
			anim.SetBool("Crawling", true); //The "Crawling" parameter in the Animator gets the value true to start crawling animations 
			this.rigidbody2D.gravityScale = 0; //sets gravity to 0, so it simulates if the player was hanging in the arms
			this.rigidbody2D.drag = 25; //Sets the drag up, to make it feel like there is som ressistens and you are not in a zero gravity space 
		}

		//checks if the boolean from "MonkeyBars.cs" are false
		else if (MonkeyBars.onMonkeyBar == false){
				anim.SetBool("Crawling", false); //The "Crawling" parameter in the Animator gets the value false to stop crawling animations 
				this.rigidbody2D.gravityScale = standardGravity; //sets gravity to initial
			this.rigidbody2D.drag = standardDrag; //sets drag to initial
		}
	}

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

	void die()
	{
		Debug.Log ("Dying");
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Weapon")												// Pick up weapon
		{
			Debug.Log ("Picking up this thing");
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
