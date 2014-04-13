using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour
{
	bool facingRight;
	int health;
	int armor;
	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;
	public float moveSpeed = 11.0f;

	float MAX_MOVE_SPEED = 1.0f;
	Animator anim;

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

	void FixedUpdate()
	{
		float input = Input.GetAxis ("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(input));

		// Moving right
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

		// Moving left
		if (Mathf.Abs (input * rigidbody2D.velocity.x) > MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

		// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();

		if (input > 0 && !facingRight)
			flip ();
		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
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


}
