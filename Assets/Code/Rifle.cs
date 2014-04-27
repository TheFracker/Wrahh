using UnityEngine;
using System.Collections;

public class Rifle : Weapon {

	void Start()
	{
		loadPrefab ();
		name = "Rifle";
		ammo = 15;
		MAGAZINE_SIZE = 15;
		reloadTime = 3.0f;
		reloading = false;
		durability = 10;
		MAX_DURABILITY = 10;
		accidentalFire = 2.0f;
		hitDamage = 3;
		delay = 0.3f;
		durabilityLossChance = 30;
	}

	protected override void loadPrefab()
	{
		base.loadPrefab ();
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectile") as GameObject;
	}

	protected override IEnumerator shot ()
	{
		while(shooting)
		{
			if (ammo > 0 &! reloading) {
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

	public override void hit ()
	{
		base.hit ();
		Debug.Log(durabilityLossChance);
		if(Random.Range(0,100) <= durabilityLossChance)
			durability--;
	}

	protected override void rangeLevel1 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectileMRange") as GameObject;
	}
	
	protected override void rangeLevel2 ()
	{
		hitProjectile = Resources.Load ("Prefabs/hitRifleProjectileLRange") as GameObject;
	}
	
	protected override void rangeLevel3 ()
	{
		base.rangeLevel3 ();
	}
}
