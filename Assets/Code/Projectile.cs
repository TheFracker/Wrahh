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

	void OnTriggerEnter2D(Collider2D c)
	{
		c.gameObject.GetComponent<Wrahh>().hurt(this);
	}

	public void activate()
	{

	}

	void move ()
	{
		this.rigidbody2D.velocity = Vector2.right * -10;
	}
}
