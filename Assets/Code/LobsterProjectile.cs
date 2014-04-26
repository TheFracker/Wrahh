using UnityEngine;
using System.Collections;

public class LobsterProjectile : HitProjectile {

	// Use this for initialization
	void Start () {
		speed = 0;
		damage = 20;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToDie < Time.time)
			Destroy(this.gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			c.gameObject.GetComponent<Wrahh>().hurt(this);
			Destroy (this.gameObject);
		}
	}

	public override int giveDamage ()
	{
		return damage;
	}
}
