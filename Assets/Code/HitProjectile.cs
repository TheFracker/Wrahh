using UnityEngine;
using System.Collections;

public class HitProjectile : Projectile
{
	protected float liveTime;
	protected float timeToDie;
	protected float criticalChange;
	
	void Start ()
	{
		speed = 0;
		damage = 1;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
		criticalChange = 5;
	}
	
	void Update () 
	{
		if(timeToDie < Time.time)
			Destroy(this.gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Enemy" && c.name == "Dolphin_Gun" || c.name == "Dolphin_Rifle")
		{
			c.gameObject.GetComponent<Dolphin>().hurt(this);
			Destroy (this.gameObject);
		}
	}

	public override int giveDamage ()
	{
		if(Random.Range(0,100) < criticalChange)
			damage *= 3;
		return damage; 
	}
}