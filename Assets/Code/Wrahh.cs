using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour {

	int health;
	int armor;
	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;
	float moveSpeed = 5.0f;
	float MAX_MOVE_SPEED = 1.0f;

	// Use this for initialization
	void Start () {
		health = 3;  // Three hearts
		armor = 0; // No armor to start with
		grenades = 0; // Nothing to throw yet
	}
	
	// Update is called once per frame
	void Update () {
		// Throw grenade
		if (Input.GetKeyUp(KeyCode.G))
			throwGrenade ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		// Moving right
		if (h * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * h * moveSpeed);

		// Moving left
		if (Mathf.Abs(h * rigidbody2D.velocity.x) > MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * h * moveSpeed);
	}

	public void useWeapon(Weapon currentWeapon)
	{
		Debug.Log ("Hitting with this weird club");
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

	void OnCollisionEnter2D(Collision2D c)
	{
		// Pick up weapon
		if (c.collider.tag == "Weapon")
			// picks up weapon
			Debug.Log ("Picking up this thing");
		// Pick up armor
		if (c.collider.tag == "Armor")
			// picks up armor
			Debug.Log ("This can protect me");
	}

}
