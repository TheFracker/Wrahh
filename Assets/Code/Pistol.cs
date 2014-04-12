using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	public GameObject bullet = null;
	public float bulletSpeed = 100f;

	public Pistol() : base()
	{
	}

	public Pistol(int ammo, float reloadTime) : base(ammo, reloadTime){}
	
	public Pistol(string name, int durability, int hitDamage, float accidentalFire) : base(name, durability, hitDamage, accidentalFire){}

	public override void shoot()
	{
		GameObject shot = (GameObject)GameObject.Instantiate(bullet, transform.position, transform.rotation);

		shot.rigidbody.AddForce(transform.forward*bulletSpeed);
		Debug.Log ("bullet");
	}

}