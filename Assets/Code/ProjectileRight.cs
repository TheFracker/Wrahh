using UnityEngine;
using System.Collections;

public class ProjectileRight : Projectile {

	// Inheritates from projectile

	// Damage and speed is set
	void Start () {
		damage = 1;
		speed = 10;
	}

	// Gives damage to Wrahh when hit
	public int giveDamage()
	{
		return damage;
	}

	// Is called when colliding with something but nothing will happen unless the player is hit
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			Destroy (this.gameObject);
			c.gameObject.GetComponent<Wrahh>().hurt(this);
		}
	}
}
