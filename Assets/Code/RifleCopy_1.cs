using UnityEngine;
using System.Collections;

public class Rifle : Weapon {

	public Rifle() : base()
	{
		// SHOULD ONLY BE USED FOR TESTING PURPOSE
	}

	public Rifle(int ammo, int MAGAZINE_SIZE, float reloadTime) : base(ammo, MAGAZINE_SIZE, reloadTime){
	}

	public Rifle(string name, int durability, int hitDamage, float accidentalFire) : base(name, durability, hitDamage, accidentalFire){}

	public override void hit()
	{
		Debug.Log ("Rifle hit " + ammo);
	}

	public override void shoot ()
	{
		Debug.Log ("Rifle shot");
	}
}
