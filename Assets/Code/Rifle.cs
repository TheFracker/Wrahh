using UnityEngine;
using System.Collections;

public class Rifle : Weapon {

	void Start()
	{
		loadPrefab ();
		name = "Rifle";
		ammo = 15;
		MAGAZINE_SIZE = 15;
		reloadTime = 3.0f;
		reloading = false;
		durability = 3;
		accidentalFire = 2.0f;
		hitDamage = 1;
		delay = 0.3f;
	}

	protected override void loadPrefab()
	{
		base.loadPrefab ();
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectile") as GameObject;
	}

	public override void hit ()
	{
		base.hit ();
	}

	public override void shoot()
	{
		base.shoot ();

//		// Gives the position of where the bullet should be spawned
//		pos = this.transform.position + new Vector3(-1.5f,0.5f,0);
//		// Shoots creates the bullets, the bullets themself give them their speed
//		// There needs to be two kinds of bullets, one for shooting left and one for right
//		if (gameObject.GetComponent<Dolphin>().isFacingRight()){
//			Instantiate(bullet, pos, Quaternion.identity);
//		}
//		else{
//			Instantiate(bullet, pos, Quaternion.identity);
//		}
	}
}
