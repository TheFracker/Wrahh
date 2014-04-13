using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int damage;
	float speed;

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

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.collider.tag == "Player")
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
