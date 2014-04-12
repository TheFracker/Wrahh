using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	string name; // Name of the weapon
	int ammo; // Amount of ammoation
	float reloadTime; // The time it takes to reload
	bool reloading; // If reloading, this will be true
	int durability; // How many hits it can take
	float accidentalFire; // Critacal chance
	int hitDamage; // Damage it gives when using it as a club

	public Weapon()
	{

	}

	public Weapon(int ammo, float reloadTime)
	{
		this.ammo = ammo;
		this.reloadTime = reloadTime;
		reloading = false;
	}

	public Weapon(string name, int durability, int hitDamage, float accidentalFire)
	{
		this.name = name;
		this.durability = durability;
		this.hitDamage = hitDamage;
		this.accidentalFire = accidentalFire;
	}

	public virtual void hit()
	{
		Debug.Log ("Hitting");
	}

	public virtual void shoot()
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
