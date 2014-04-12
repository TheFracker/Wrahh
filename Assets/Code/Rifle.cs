using UnityEngine;
using System.Collections;

public class Rifle : Weapon {

	void Start()
	{
		name = "Rifle";
		ammo = 3;
		MAGAZINE_SIZE = 30;
		reloadTime = 5.0f;
		reloading = false;
		durability = 3;
		accidentalFire = 2.0f;
		hitDamage = 1;
	}

	public override void hit()
	{
		Debug.Log ("Rifle hit " + ammo);
	}
	
	public override void shoot ()
	{
		base.shoot ();
		Debug.Log ("Rifle Shot " + ammo);
	}
}
