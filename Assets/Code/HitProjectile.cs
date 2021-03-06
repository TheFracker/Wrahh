﻿using UnityEngine;
using System.Collections;

public class HitProjectile : Projectile
{
	protected float liveTime;
	protected float timeToDie;
	protected float criticalChance;
	
	void Awake ()
	{
		speed = 0;
		damage = 5;
		liveTime = 0.2f;
		timeToDie = Time.time + liveTime;
		criticalChance = 5;
	}

	// Update is used to see if it is time for the projectile to be removed again
	protected void Update () 
	{
		if(timeToDie < Time.time)
			Destroy(this.gameObject);
	}

	// This will trigger when the projectile collides with anything, but will only hurt the enemies
	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Enemy" && c.name == "Dolphin")
		{
			c.gameObject.GetComponent<Dolphin>().hurt(this);
			Destroy (this.gameObject);
		}
		if(c.tag == "Enemy" && c.name == "ThaLobster")
		{
			c.gameObject.GetComponent<Lobster>().hurt(this);
			Destroy (this.gameObject);
		}
	}

	// Inflicts damage to the gamecharacter being hit
	public override int giveDamage ()
	{
		if (Random.Range (0, 100) < criticalChance)
				damage *= 3;
		return damage; 
	}
}