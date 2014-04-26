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
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectile") as GameObject;
	}

	public override void hit ()
	{
		base.hit ();
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}
}
