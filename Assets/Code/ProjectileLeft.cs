using UnityEngine;
using System.Collections;

public class ProjectileLeft : Projectile {

	// Inheritates from projectile

	// Damage and speed is set
	void Awake () {
		damage = 5;
		speed = 10;
	}

	// Inflicts damage on the hit character
	public int giveDamage()
	{
		return damage;
	}

	// This will trigger when the projectile collides with anything, but will only hurt the player
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			Destroy (this.gameObject);
			c.gameObject.GetComponent<Wrahh>().hurt(this);
		}
	}
	
	protected override void move ()
	{
		// Moves the projectile left
		this.rigidbody2D.velocity = Vector2.right * -speed;
	}
}
