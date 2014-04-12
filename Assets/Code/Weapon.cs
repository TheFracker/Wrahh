using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected string name; // Name of the weapon
	protected int ammo; // Amount of ammoation
	protected int MAGAZINE_SIZE; // Size of magazine
	protected float reloadTime; // The time it takes to reload
	protected bool reloading; // If reloading, this will be true
	protected int durability; // How many hits it can take
	protected float accidentalFire; // Critacal chance
	protected int hitDamage; // Damage it gives when using it as a club

	void Update()
	{
		Debug.Log (isReloading ());
	}

	public virtual void hit()
	{
		Debug.Log ("Hitting");
		durability--;
	}

	public virtual void shoot()
	{
		if (ammo > 0) {
			Debug.Log ("Shooting");
			ammo--;
		} else
			StartCoroutine (reload ());
	}

	public bool isReloading()
	{
		return reloading;
	}

	public int giveHitDamage()
	{
		return hitDamage;
	}

	public string getName()
	{
		return name;
	}

	IEnumerator reload()
	{
		reloading = true;
		yield return new WaitForSeconds (reloadTime);
		reloading = false;
	}
}
