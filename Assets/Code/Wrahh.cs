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
	private float moveSpeed = 10000.0f;
	private float currentSpeed;
	private float MAX_MOVE_SPEED = 3.0f;
	private float standardGravity = 7.42f;
	private float standardDrag = 5f;

	Animator anim;

	void Start ()
	{
		facingRight = true;
		health = 3;  										// Three hearts
		armor = 3; 											// No armor to start with
		grenades = 0; 										// Nothing to throw yet
		currentWeapon = gameObject.AddComponent<Rifle>();
		anim = GetComponent<Animator>();
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



		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

		currentSpeed = rigidbody2D.velocity.x;

		// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();

		if (input > 0 && !facingRight)
			flip ();

		crawlMonkeyBars();

		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}

	void crawlMonkeyBars(){
		if (MonkeyBars.onMonkeyBar == true){
			anim.SetBool("Crawling", true);
			this.rigidbody2D.gravityScale = 0;
			this.rigidbody2D.drag = 25;
		}

		else if (MonkeyBars.onMonkeyBar == false){
				anim.SetBool("Crawling", false);
				this.rigidbody2D.gravityScale = standardGravity;
				this.rigidbody2D.drag = standardDrag;
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
		// Pick up weapon
		if (c.tag == "Weapon") {
			// picks up weapon
			Debug.Log ("Picking up this thing");
			Destroy(c.gameObject);
		}
		// Pick up armor
		if (c.tag == "Armor") {
			// picks up armor
			Debug.Log ("This can protect me");
			Destroy(c.gameObject);
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
