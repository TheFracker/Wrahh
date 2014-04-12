using UnityEngine;
using System.Collections;

public abstract class Weapon {

	string name;
	int ammo;
	float reloadTime;
	bool reloading;
	int durability;
	float accidentalFire;
	int hitDamage;

	public Weapon()
	{

	}

	public void hit()
	{
		Debug.Log ("Hitting");
	}

	public void shoot()
	{
		Debug.Log ("Shooting");
	}

	public void reload()
	{
		Debug.Log ("Reloading");
	}

	public bool isReloading()
	{
		if (reloading)
			return true;
		return false;
	}

	public int giveHitDamage()
	{
		return hitDamage;
	}
}
