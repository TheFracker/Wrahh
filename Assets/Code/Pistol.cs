using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	public GameObject bullet = null;
	public float bulletSpeed = 100f;

	public override void shoot()
	{
		GameObject shot = (GameObject)GameObject.Instantiate(bullet, transform.position, transform.rotation);

		shot.rigidbody.AddForce(transform.forward*bulletSpeed);
		Debug.Log ("bullet");
	}

}