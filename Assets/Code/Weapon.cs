using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected string name; // Name of the weapon
	protected int ammo = 9; // Amount of ammoation
	protected int MAGAZINE_SIZE = 9; // Size of magazine
	protected float reloadTime = 2; // The time it takes to reload
	protected bool reloading; // If reloading, this will be true
	protected int durability; // How many hits it can take
	protected float accidentalFire; // Critacal chance
	protected int hitDamage; // Damage it gives when using it as a club
	public Rigidbody2D bullet;
	float speed = 20.0f;

	public virtual void hit()
	{
		Debug.Log ("Hitting");
		durability--;
	}

	void Update()
	{
		//shoot ();
	}

	public virtual void shoot()
	{
		if (ammo > 0) {
			Debug.Log ("Shooting");
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
			Debug.Log (bullet.ToString());
			ammo--;
		} else {
			StartCoroutine (reload ());
		}

	}

	public bool isReloading()
	{
		return reloading;
	}

	public int giveHitDamage()
	{
		return hitDamage;
	}

	public string getName()
	{
		return name;
	}

	IEnumerator reload()
	{
		reloading = true;
		yield return new WaitForSeconds (reloadTime);
		reloading = false;
	}
}
