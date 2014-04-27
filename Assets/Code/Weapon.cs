using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected string name; 				// Name of the weapon
	protected int ammo = 9; 			// Amount of ammoation
	protected int MAGAZINE_SIZE = 9; 	// Size of magazine
	protected float reloadTime = 2; 	// The time it takes to reload
	protected int durability; 			// How many hits it can take
	protected int MAX_DURABILITY;		// The maximum amount of hits the weapon can take
	protected int durabilityLossChance; // The chance to lose durability when hitting
	protected int hitDamage; 			// Damage it gives when using it as a club
	protected GameObject bulletRight; 	// The bullet in the champer when shooting right
	protected GameObject bulletLeft;	// The bullet in the champer when shooting left
	protected GameObject hitProjectile; // Projectile created when hitting with the weapon
	protected Vector3 pos; 				// The position where the projectile should spawn
	protected bool shooting;			// If the Dolphin is pressing the trigger or not
	protected float delay;				// The time between each shot
	protected int durabillityLevel = 0;	// The level the durability has been upgraded to
	protected int rangeLevel = 0;		// The level the range has been upgraded to

	// Start by loading prefabs into the different projectile variables
	void Start()
	{
		loadPrefab ();
		name = "Fists";
		shooting = false;
		durability=1;
		hitDamage = 1;
	}

	// Is called whenever the weapon is upgraded in the upgrade station
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
	}

	// Is called when Wrahh uses a weapon
	public virtual void hit()
	{
		if(gameObject.GetComponent<Wrahh>().isFacingRight())
			pos = this.transform.position + new Vector3(1.0f,0.5f,0);
		else
			pos = this.transform.position + new Vector3(-1.0f,0.5f,0);

		Instantiate(hitProjectile, pos, Quaternion.identity);
	}

	// Function that loads the prefabs into the variables
	protected virtual void loadPrefab()
	{
		bulletRight = Resources.Load ("Prefabs/BulletRight") as GameObject;
		bulletLeft =  Resources.Load ("Prefabs/BulletLeft") as GameObject;
		hitProjectile =  Resources.Load ("Prefabs/hitProjectile") as GameObject;
	}

	//////////////////////////////////////////////
	//	SHOOT									//
	//////////////////////////////////////////////

	// Is called when a dolphin uses a weapon
	public virtual void shoot()
	{
		// If a dolphin is not shooting a coroutine is startet, and shooting is set to true.
		if(!shooting)
		{
			shooting = true;
			StartCoroutine ("shot");
		}
	}

	protected virtual IEnumerator shot()
	{
		// If shootng is true and ammo is more than 0, then the dolphin will shoot, if not, the weapon will be reloaded.
		if(shooting)
		{
			if (ammo > 0) {
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

	//////////////////////////////////////////////
	//	UPGRADE STATES							//
	//////////////////////////////////////////////


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


	// Getters and setters
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
}
