using UnityEngine;
using System.Collections;

public class Rifle : Weapon {

	// Loads the prefabs in to the correct projectile variables and sets the standard stats of the Rifle
	void Start()
	{
		loadPrefab ();
		name = "Rifle";
		ammo = 15;
		MAGAZINE_SIZE = 15;
		reloadTime = 3.0f;
		durability = 10;
		MAX_DURABILITY = 10;
		hitDamage = 3;
		delay = 0.3f;
		durabilityLossChance = 30;
	}

	// Calls the base function, but loads a different prefab for the hitProjectile
	protected override void loadPrefab()
	{
		base.loadPrefab ();
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectile") as GameObject;
	}

	// Is called when a dolphin starts shooting
	// It is the same as in the Weapon class, only the pos is different
	protected override IEnumerator shot ()
	{
		// If a dolphin is shooting and there is ammo in the clip, bullets will be spawned
		// if on the other hand there is no more ammo left, the rifle will be reloaded
		while(shooting)
		{
			if (ammo > 0) {
				if(gameObject.GetComponent<Dolphin>().isFacingRight())
				{
					pos = this.transform.position + new Vector3(1.4f,0.3f,0);
					Instantiate(bulletRight, pos, Quaternion.identity);
				}
				else
				{
					pos = this.transform.position + new Vector3(-1.4f,0.3f,0);
					Instantiate(bulletLeft, pos, Quaternion.identity);
				}
				ammo--;
				yield return new WaitForSeconds(delay);
				shooting = false;
			} else {
				yield return new WaitForSeconds(reloadTime);
				shooting = false;
				ammo = MAGAZINE_SIZE;
			}
		}
	}

	// Is called when Wrahh starts hitting, and for every hit there is a chance that durability will be lost
	public override void hit ()
	{
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}

	// Loads new prefabs into the hitProjectile variable when the range is upgraded
	protected override void rangeLevel1 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectileMRange") as GameObject;
	}
	
	protected override void rangeLevel2 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectileLRange") as GameObject;
	}
}
