using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	GameObject bullet = null;
	float bulletSpeed = 100f;

	public Pistol() : base()
	{
	}

	public void shoot()
	{
		GameObject shot = GameObject.Instantiate(bullet, transform.position + (transform.forward*2), transform.rotation);

		shot.rigidbody2D.AddForce(transform.forward*bulletSpeed);
	}

}