using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

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
