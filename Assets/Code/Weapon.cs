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
	protected GameObject bulletRight; 	// The bullet in the champer when shooting right;
	protected GameObject bulletLeft;	// The bullet in the champer when shooting left;
	protected GameObject hitProjectile; // Projectile created when hitting with the weapon;
	protected Vector3 pos; 				// The position of the barrels mouth
	protected bool shooting;			// If the Dolphin is pressing the trigger or not
	protected float delay;				// The time between each shot

	void Start()
	{
		loadPrefab ();
		shooting = false;
	}

	public virtual void hit()
	{
		Debug.Log ("Hitting");
		durability--;
	}
	
	void Update()
	{
		pos = this.transform.position + new Vector3(-1.5f,0.5f,0);
	}

	protected virtual void loadPrefab()
	{
		bulletRight = Resources.Load ("Prefabs/BulletRight") as GameObject;
		bulletLeft =  Resources.Load ("Prefabs/BulletLeft") as GameObject;
		hitProjectile =  Resources.Load ("Prefabs/hitProjectile") as GameObject;
	}

	public virtual void shoot()
	{
		if(!shooting)
		{
			shooting = true;
			StartCoroutine ("shot");
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

	IEnumerator shot()
	{
		while(shooting)
		{
			if (ammo > 0 &! reloading) {
				if(gameObject.GetComponent<Dolphin>().isFacingRight())
					Instantiate(bulletRight, pos, Quaternion.identity);
				else
					Instantiate(bulletLeft, pos, Quaternion.identity);
				ammo--;
				yield return new WaitForSeconds(delay);
				shooting = false;
				Debug.Log (ammo);
			} else {
				yield return new WaitForSeconds(reloadTime);
				shooting = false;
				ammo = MAGAZINE_SIZE;
			}
		}
	}

	public void done()
	{
		ammo = 1;
		Debug.Log ("HERE I AM");
	}
}
