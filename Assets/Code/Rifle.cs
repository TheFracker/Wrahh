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
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}
}
