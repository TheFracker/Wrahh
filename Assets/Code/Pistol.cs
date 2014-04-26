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
		durability = 7;
		MAX_DURABILITY = 7;
		accidentalFire = 2.0f;
		hitDamage = 2;
		delay = 0.7f;
		durabilityLossChance = 30;
	}

	protected override void loadPrefab()
	{
		base.loadPrefab ();
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectile") as GameObject;
	}

	public override void hit ()
	{
		base.hit ();
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}

	protected override void rangeLevel1 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileMRange") as GameObject;
	}

	protected override void rangeLevel2 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileLRange") as GameObject;
	}

	protected override void rangeLevel3 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileLRange") as GameObject;
	}
}
