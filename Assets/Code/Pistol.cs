using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	// Use this for initialization
	void Start () 
	{
		loadPrefab ();
		name = "Pistol";
		ammo = 3;
		MAGAZINE_SIZE = 3;
		reloadTime = 1.0f;
		reloading = false;
		durability = 3;
		accidentalFire = 2.0f;
		hitDamage = 1;
		delay = 0.7f;
	}

	protected override void loadPrefab()
	{
		base.loadPrefab ();
		// This bullet variable is inherited from Weapon
		//bullet = Resources.Load ("Prefabs/Bullet") as GameObject;
	}
	
	public override void shoot()
	{
		base.shoot ();
//		// Gives the position of where the bullet should be spawned
//		pos = this.transform.position + new Vector3(-2.5f,0.5f,0);
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
