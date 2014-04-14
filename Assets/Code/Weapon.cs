using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected string name; 				// Name of the weapon
	protected int ammo = 9; 			// Amount of ammoation
	protected int MAGAZINE_SIZE = 9; 	// Size of magazine
	protected float reloadTime = 2; 	// The time it takes to reload
	protected bool reloading; 			// If reloading, this will be true
	protected int durability; 			// How many hits it can take
	protected float accidentalFire; 	// Critacal chance
	protected int hitDamage; 			// Damage it gives when using it as a club
	protected GameObject bullet; 		// The bullet in the champer
	protected GameObject hitProjectile; // The projectile that will be created when hitting
	protected Vector3 pos; 				// The position of the barrels mouth

	void Start()
	{
		loadPrefab ();
	}

	public virtual void hit()
	{
		Debug.Log ("Hitting");
		durability--;
	}
	
	void Update()
	{

	}

	protected virtual void loadPrefab()
	{
		bullet = Resources.Load ("Prefabs/Bullet") as GameObject;
	}

	public void pew()
	{
		shoot ();
	}

	public virtual void shoot()
	{
		if (ammo > 0) {
			//Instantiate(bullet, this.transform.position, Quaternion.identity);
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
