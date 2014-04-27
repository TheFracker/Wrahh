using UnityEngine;
using System.Collections;

public class ProjectileRight : Projectile {

	// Inheritates from projectile

	// Damage and speed is set
	void Start () {
		damage = 1;
		speed = 10;
	}

	public int giveDamage()
	{
		return damage;
	}

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
		base.move ();
	}
}
