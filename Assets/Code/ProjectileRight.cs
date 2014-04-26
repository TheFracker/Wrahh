using UnityEngine;
using System.Collections;

public class ProjectileRight : Projectile {

	// Use this for initialization
	void Start () {
		damage = 1;
		speed = 10;
	}
	
	// Update is called once per frame
	void Update () {
		move ();
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
