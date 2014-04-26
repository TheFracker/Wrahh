﻿using UnityEngine;
using System.Collections;

public class HitProjectile : Projectile {

	protected float liveTime;
	protected float timeToDie;
	protected float criticalChange;

	// Use this for initialization
	void Start () {
		speed = 0;
		damage = 1;
		liveTime = 100.0f;
		timeToDie = Time.time + liveTime;
		criticalChange = 5;
	}

	// Update is called once per frame
	void Update () 
	{
		if(timeToDie < Time.time)
		{
			Destroy(this.gameObject);
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Enemy")
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
