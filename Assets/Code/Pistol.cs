using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	// Loads the prefabs in to the correct projectile variables and sets the standard stats of the Pistol
	void Start () 
	{
		loadPrefab ();
		name = "Pistol";
		ammo = 3;
		MAGAZINE_SIZE = 3;
		reloadTime = 1.0f;
		durability = 7;
		MAX_DURABILITY = 7;
		hitDamage = 8;
		delay = 0.7f;
		durabilityLossChance = 30;
		xPos = 1f;
	}

	// Calls the base function, but loads a different prefab for the hitProjectile
	protected override void loadPrefab()
	{
		base.loadPrefab ();
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectile") as GameObject;
	}

	// Is called when Wrahh starts hitting, and for every hit there is a chance that durability will be lost
	public override void hit ()
	{
		base.hit ();
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}

	// Is called when a dolphin starts shooting
	// It is the same as in the Weapon class, only the pos is different
	protected override IEnumerator shot ()
	{
		while(shooting)
		{
			if (ammo > 0) {
				if(gameObject.GetComponent<Dolphin>().isFacingRight())
				{
					pos = this.transform.position + new Vector3(1.12f,0.37f,0);
					Instantiate(bulletRight, pos, Quaternion.identity);
				}
				else
				{
					pos = this.transform.position + new Vector3(-1.12f,0.37f,0);
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

	// Loads new prefabs into the hitProjectile variable when the range is upgraded
	protected override void rangeLevel1 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileMRange") as GameObject;
		xPos = 1.05f;
	}

	protected override void rangeLevel2 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileLRange") as GameObject;
		xPos = 1.2f;
	}
}
