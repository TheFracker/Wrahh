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
		durability = 7;
		MAX_DURABILITY = 7;
		accidentalFire = 2.0f;
		hitDamage = 2;
		delay = 0.7f;
		durabilityLossChance = 30;
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

	protected override IEnumerator shot ()
	{
		while(shooting)
		{
			if (ammo > 0 &! reloading) {
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

	protected override void rangeLevel1 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileMRange") as GameObject;
	}

	protected override void rangeLevel2 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileLRange") as GameObject;
	}

	protected override void rangeLevel3 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitGunProjectileLRange") as GameObject;
	}
}
