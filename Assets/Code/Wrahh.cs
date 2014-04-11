using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour {

	bool facingRight;
	int health;
	int armor;
	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;
	float moveSpeed = 5.0f;
	float MAX_MOVE_SPEED = 1.0f;

	// Use this for initialization
	void Start () {
		facingRight = true;
		health = 3;  // Three hearts
		armor = 0; // No armor to start with
		grenades = 0; // Nothing to throw yet
		currentWeapon = gameObject.AddComponent<Weapon> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Throw grenade
		if (Input.GetKeyUp(KeyCode.G))
			throwGrenade ();

		// Shooting
		if (Input.GetKeyDown (KeyCode.Space))
			useWeapon (currentWeapon);
	}

	void FixedUpdate()
	{
		float input = Input.GetAxis ("Horizontal");

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
	}

	public void useWeapon(Weapon currentWeapon)
	{
		Debug.Log ("Hitting with this weird club");
		currentWeapon.hit ();
	}

	public void throwGrenade()
	{
		if (grenades > 0) {
			Debug.Log("Throwing grenade " + grenades);
			grenades--;
			return;
		}
		Debug.Log ("Don't have anything to throw");
	}

	public void die()
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
		health -= p.giveDamage ();
	}
}
