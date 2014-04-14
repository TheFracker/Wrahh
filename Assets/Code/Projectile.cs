using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected int damage;
	protected float speed;

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

	protected virtual void OnCTriggerStay2D(Collider c)
	{
		if(c.tag == "Player")
		{
			c.gameObject.GetComponent<Wrahh>().hurt(this);
			Destroy (this.gameObject);
		}

	}

	void move ()
	{
		this.rigidbody2D.velocity = Vector2.right * -10;
	}
}
