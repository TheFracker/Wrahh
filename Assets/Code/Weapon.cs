using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected string name; 				// Name of the weapon
	protected int ammo = 9; 			// Amount of ammoation
	protected int MAGAZINE_SIZE = 9; 	// Size of magazine
	protected float reloadTime = 2; 	// The time it takes to reload
	protected bool reloading; 			// If reloading, this will be true
	protected int durability; 			// How many hits it can take
	protected int MAX_DURABILITY;		// The maximum amount of hits the weapon can take
	protected int durabilityLossChance; // The chance to lose durability when hitting
	protected float accidentalFire; 	// Critacal chance
	protected int hitDamage; 			// Damage it gives when using it as a club
	protected GameObject bulletRight; 	// The bullet in the champer when shooting right
	protected GameObject bulletLeft;	// The bullet in the champer when shooting left
	protected GameObject hitProjectile; // Projectile created when hitting with the weapon
	protected Vector3 pos; 				// The position where the projectile should spawn
	protected bool shooting;			// If the Dolphin is pressing the trigger or not
	protected float delay;// The time between each shot
	protected int durabillityLevel = 0;
	protected int rangeLevel = 0; 


	void Start()
	{
		loadPrefab ();
		durabilityLossChance = 70;
		name = "weapon";
		shooting = false;
		durability=1;
	}

	public void upgradeLevel()
	{
		if(durabillityLevel == 1){
			durabillityLevel1();
		}
		else if(durabillityLevel == 2){
			durabillityLevel2();
		}
		else if(durabillityLevel == 3){
			durabillityLevel3();
		}

		if(rangeLevel== 1){
			rangeLevel1();
		}
		else if(rangeLevel == 2){
			rangeLevel2();
		}
		else if(durabillityLevel == 3){
			rangeLevel3();
		}
	}

	public virtual void hit()
	{
		if(gameObject.GetComponent<Wrahh>().isFacingRight())
			pos = this.transform.position + new Vector3(1.0f,0.5f,0);
		else
			pos = this.transform.position + new Vector3(-1.0f,0.5f,0);

		Instantiate(hitProjectile, pos, Quaternion.identity);
	}

	protected virtual void loadPrefab()
	{
		bulletRight = Resources.Load ("Prefabs/BulletRight") as GameObject;
		bulletLeft =  Resources.Load ("Prefabs/BulletLeft") as GameObject;
		hitProjectile =  Resources.Load ("Prefabs/hitProjectile") as GameObject;
	}

	public virtual void shoot()
	{
		if(!shooting)
		{
			shooting = true;
			StartCoroutine ("shot");
		}
	}

	protected virtual void durabillityLevel1()
	{
		MAX_DURABILITY += 3;
		durability +=3;
		Debug.Log ("Durabillity Level 1");
	}

	protected virtual void durabillityLevel2()
	{
		MAX_DURABILITY += 4;
		durability +=4;
		Debug.Log ("Durabillity Level 2");
	}

	protected virtual void durabillityLevel3()
	{
		MAX_DURABILITY += 5;
		durability +=5;
		Debug.Log ("Durabillity Level 3");
	}

	protected virtual void rangeLevel1()
	{
	}

	protected virtual void rangeLevel2()
	{
	}

	protected virtual void rangeLevel3()
	{
	}

	public int DurabillityLevel
	{
	get{ return durabillityLevel; }
	set{ durabillityLevel = value; }
	}

	public int RangeLevel
	{
		get{ return rangeLevel; }
		set{ rangeLevel = value; }
	}
	public bool isReloading()
	{
		return reloading;
	}

	public int getHitDamage()
	{
		return hitDamage;
	}

	public string getName()
	{
		return name;
	}
	public int getDura()
	{
		return durability;
	}

	public void setDura(int value)
	{
		durability = value;
	}

	public int getMAXDura()
	{
		return MAX_DURABILITY;
	}

	public void addToMAXDura(int value)
	{
		MAX_DURABILITY += value;
	}
	
	protected virtual IEnumerator shot()
	{
		while(shooting)
		{
			if (ammo > 0 &! reloading) {
				if(gameObject.GetComponent<Dolphin>().isFacingRight())
				{
					pos = this.transform.position + new Vector3(1.5f,0.5f,0);
					Instantiate(bulletRight, pos, Quaternion.identity);
				}
				else
				{
					pos = this.transform.position + new Vector3(-1.5f,0.5f,0);
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
}
