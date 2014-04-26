using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	protected int damage;
	protected float speed;

	void FixedUpdate()
	{
		move ();
	}
	
	public virtual int giveDamage()
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
	
	protected virtual void move ()
	{
		this.rigidbody2D.velocity = Vector2.right * speed;
	}
}
