using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// This class is inherited from

	protected int damage;		// Used to set the damage of the projectile
	protected float speed;		// Used to set the speed at which the projectile will move

	protected void FixedUpdate()
	{
		// Moves the projectile
		move ();
	}

	// Inflicts damage on the hit character
	public virtual int giveDamage()
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

	// Moves the projectile
	protected virtual void move ()
	{
		this.rigidbody2D.velocity = Vector2.right * speed;
	}
}
