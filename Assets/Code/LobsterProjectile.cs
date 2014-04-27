using UnityEngine;
using System.Collections;

public class LobsterProjectile : HitProjectile {

	// Since this projectile will not be moving its speed is set to 0
	// And since it will not be moving it will be destroyed after a little while, which is is liveTime
	void Start () {
		speed = 0;
		damage = 20;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
	}

	// This will trigger when the projectile collides with anything, but will only hurt the player
	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			c.gameObject.GetComponent<Wrahh>().hurt(this);
			Destroy (this.gameObject);
		}
	}

	// Inflicts the damage
	public override int giveDamage ()
	{
		return damage;
	}
}
