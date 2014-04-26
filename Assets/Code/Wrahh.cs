using UnityEngine;
using System.Collections;

public class Wrahh : GameCharacters
{
	int lobsterParts;
	int weaponParts;
	bool shieldOn;
	bool helmOn;
	int shieldMaxArmor;
	int helmMaxArmor;
	int shieldArmor;
	int helmArmor;
	int currentSlot;
	bool walkSoundPlaying;

	public Shield shield;
	public Helm helm;
	public static GameObject gameoverScreen;

	public static bool canCrushEnemy = false;
	
	Weapon[] weapons = new Weapon[6];
	Weapon currentWeapon;
	int grenades;
	
	private float currentSpeed;										// set to public to see current speed
	private float climbSpeed = 5f;

	private GameObject prefab;
	public GameObject defaultPrefab;

	AudioSource[] sounds; 											// creates an array "sounds" of type "AudioSource"

	//////////////////////////////////
	// START 			    		//
	//////////////////////////////////
	void Start ()
	{

		if(shield == null)
			shield = new Shield();
		if(helm == null)
			helm = new Helm();

		shieldOn = false;
		helmOn = false;
		grenades = 0;
		weaponParts = 100;
		lobsterParts = 10;
		shieldArmor = 0;
		helmArmor = 0;


		for(int i = 0; i < 5; i++)
		{
			weapons[i] = gameObject.AddComponent<Weapon>();
		}

		currentSlot = 0;
		currentWeapon = weapons[currentSlot];
		prefab = defaultPrefab;

		walkSoundPlaying = false;
		for (int i = 0; i<14;i++)
		{
			this.gameObject.AddComponent<AudioSource>();
		}
		sounds = GetComponents<AudioSource>();						//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		for (int i = 0; i<14;i++)
		{
			AudioClip ac = Resources.Load("sounds/walk-"+(1+i)) as AudioClip;
			sounds[i].clip = ac;
		}
		for (int i = 0; i<14;i++)
		{
			sounds[i].playOnAwake = false;
			sounds[i].rolloffMode = AudioRolloffMode.Linear;
			sounds[i].pitch = 1f;
			sounds[i].volume = 0.1f;
		}


	//From parent "GameCharacters.cs":
		moveSpeed = 10000.0f;
		facingRight = true;
		MAX_MOVE_SPEED = 30.0f;
		health = 100;
		setStandardPhysics();
		accesAnimator();
	}
	
	//////////////////////////////////
	// UPDATE 	    				//
	//////////////////////////////////
	void Update ()
	{
		// Throw grenade
		if (Input.GetKeyUp(KeyCode.G))
			throwGrenade ();
		
		// Shooting
		if (Input.GetKeyUp (KeyCode.Space))
			useWeapon (currentWeapon);
		falling ();

		if(Input.GetKeyUp(KeyCode.O))
			changeWeaponUp();
		if(Input.GetKeyUp(KeyCode.L))
			changeWeaponDown();

	}
	
	
	////////////////////////////////////////////
	// FixedUpdate - used for movement		  //
	////////////////////////////////////////////
	void FixedUpdate()
	{
		equipedWeapon();

		float input = 0;																	// creates a local variable "input"

		falling ();
		if (anim.GetBool("IsFalling") == false && anim.GetBool("HitGround") == false){	// checks if the player is not falling or splatted out
			input = Input.GetAxis ("Horizontal"); 										//local variable (a float going from -1 - 1) depending on if you push "A"/"left key" or "D"/"right key" 
			climbingLadder();															// runs the "climbingLadder" function 
			crawlMonkeyBars(); 															// runs the "crawlMonkyBars" function 
		}
		
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

		if(input != 0 && walkSoundPlaying == false)
		{
			playWalkSound();

		}

		anim.SetFloat("Speed", Mathf.Abs(input)); // The "speed" parameter in the Animator gets values from the variable "input" 
		
		// Turn the direction Wrahh is walking
		if (input < 0 && facingRight)
			flip ();
		
		if (input > 0 && !facingRight)
			flip ();
		
		//Allows for Wrahh to move through "OneWayCollider"-Layer objects from the buttom, but not from the top.
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Wrahh"),LayerMask.NameToLayer("OneWayCollider"), rigidbody2D.velocity.y > 0);
	}


	//////////////////////////////////////////////////
	// WALK SOUNDS - controlls sounds of walk	    //
	//////////////////////////////////////////////////
	void playWalkSound()
	{
		walkSoundPlaying = true;
		sounds[Random.Range (0, 14)].Play();
		StartCoroutine(waitForWalkSound());

	}


	IEnumerator waitForWalkSound()
	{
		yield return new WaitForSeconds(0.4f);
		walkSoundPlaying = false;
	}


	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	// MONKEY BAR CRAWL - controlls physics and animtion when the player gets on or off the monkey bars	    //
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	void crawlMonkeyBars()	//controlls physics and animtion when the player gets on or off the monkey bars
	{
		//checks if the boolean from "MonkeyBars.cs" are true
		if (MonkeyBars.onMonkeyBar == true)
		{
			anim.SetBool("Crawling", true); //The "Crawling" parameter in the Animator gets the value true to start crawling animations 
			this.rigidbody2D.gravityScale = 0; //sets gravity to 0, so it simulates if the player was hanging in the arms
			this.rigidbody2D.drag = 25; //Sets the drag up, to make it feel like there is som ressistens and you are not in a zero gravity space 
		}
		
		//checks if the boolean from "MonkeyBars.cs" are false
		else if (MonkeyBars.onMonkeyBar == false)
		{
			anim.SetBool("Crawling", false); //The "Crawling" parameter in the Animator gets the value false to stop crawling animations 
			setStandardPhysics();
		}
	}
	
	//////////////////////////////////////////////
	// CLIMB LADDEER							//
	/////////////////////////////////////////////
	void climbingLadder()
	{
		if (Ladder.canClimb == true)
		{
			this.rigidbody2D.velocity = new Vector2(0,climbSpeed);
			Ladder.canClimb = false;
		}
	}
	
	//////////////////////////////////////
	// FALLING							//
	//////////////////////////////////////
	void falling()
	{
		if (this.rigidbody2D.velocity.y < -2.5)
		{
			anim.SetBool("IsFalling", true);
			canCrushEnemy = true;
		}
		
		if (this.rigidbody2D.velocity.y > -0.5 && anim.GetBool("IsFalling") == true)
		{
			anim.SetBool("HitGround", true);
			StartCoroutine(waitForFallingAnimation());
		}
	}
	
	
	IEnumerator waitForFallingAnimation()
	{
		yield return new WaitForSeconds(1f);
		anim.SetBool("HitGround", false);
		anim.SetBool("IsFalling", false);
		canCrushEnemy = false;
	}

	IEnumerator waitForAttackingAnimation()
	{
		yield return new WaitForSeconds(0.08f);
		anim.SetBool("isAttacking", false);
	}
	
	//////////////////////////////////////
	// WEAPONS							//
	//////////////////////////////////////
	void useWeapon(Weapon currentWeapon)
	{
		anim.SetBool("isAttacking", true);
		StartCoroutine(waitForAttackingAnimation());
		currentWeapon.hit ();
		Debug.Log (currentWeapon.getDura ());
		if(CurrentWeapon.getDura() <= 0)
		{
			Debug.Log ("I AM HERE");
			weapons[currentSlot] = gameObject.AddComponent<Weapon>();
			this.currentWeapon = weapons[currentSlot];
		}
	}
	
	void throwGrenade()
	{
		if (grenades > 0)
		{
			Debug.Log("Throwing grenade " + grenades);
			grenades--;
			return;
		}
		Debug.Log ("Don't have anything to throw");
	}

	//////////////////////////////////////
	//	INVENTORY						//
	//////////////////////////////////////
	bool inventoryFull()
	{
		for(int i = 0; i < 5; i++)
		{
			if(weapons[i].getName() == "weapon")
				return false;
		}
		return true;
	}

	int emptyInventorySlot()
	{
		for(int i = 0; i < 5; i++)
		{
			if(weapons[i].getName() == "weapon")
			{
				return i;
			}
		}
		return 0;
	}

	//////////////////////////////////////
	//	CHANGE WEAPON					//
	//////////////////////////////////////
	void changeWeaponUp()
	{
		if(currentSlot == 4)
			currentSlot = 0;
		else
			currentSlot++;
		currentWeapon = weapons [currentSlot];
	}

	void changeWeaponDown()
	{
		if(currentSlot == 0)
			currentSlot = 4;
		else
			currentSlot--;
		currentWeapon = weapons [currentSlot];
	}


	//////////////////////////////////////
	// PICK UP ITEMS					//
	//////////////////////////////////////
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Item")													
		{
			if (c.gameObject.name == "lobsterParts")
			{
			Debug.Log ("Picking up LobsterParts");
			Destroy(c.gameObject);
			lobsterParts += 5;
			}

			if(!inventoryFull())
			{
				if (c.gameObject.name == "gunPickUp")
				{
					int slot = emptyInventorySlot();
					Debug.Log ("Picking up guns");
					Destroy(c.gameObject);
					weapons[slot] = gameObject.AddComponent<Pistol>();
					currentSlot = slot;
					if(currentWeapon.getName() == "weapon")
						currentWeapon = weapons[currentSlot];
				}
				
				if (c.gameObject.name == "riflePickUp")
				{
					int slot = emptyInventorySlot();
					Debug.Log ("Picking up rifles");
					Destroy(c.gameObject);
					weapons[slot] = gameObject.AddComponent<Rifle>();
					currentSlot = slot;
					if(currentWeapon.getName() == "weapon" || currentWeapon.getName() == "Pistol")
						currentWeapon = weapons[currentSlot];
				}
			}
		}

		if (c.tag == "Buff")												
		{
			if (c.gameObject.name == "healthBuff")
			{
				Debug.Log ("Picking up health buff");
				Destroy(c.gameObject);
				health += 10;
			}
		}

		if (c.tag == "Armor")													// Pick up armor
		{																		
			if(c.gameObject.name == "shield")
			{
				Debug.Log ("Shield obtained!");									// Check if 'shield' was registered
				Destroy(c.gameObject);											// Removed the item from the scene
				shield.Protection = 1;
				shield.upgradeProtection();
				shieldOn = true;
				Debug.Log(shieldOn);
			}
		}
	}

	public void equipedWeapon()
	{
		if (currentWeapon.getName() == "Rifle")
		{
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(true);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
		}
		else if (currentWeapon.getName() == "Pistol")
		{
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(true);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
		}
		else if (currentWeapon.getName() == "weapon")
		{
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
		}
	}
	
	public void hurt(Projectile p)
	{
		Debug.Log("IM HIT");
		int damageTaken = p.giveDamage ();
		int armorHit = Random.Range(0,2);

		if (armorHit == 0 && shieldArmor > 0 && shieldArmor > damageTaken)
		{
			shieldArmor -= damageTaken;
			damageTaken = 0;
		}
		else if(armorHit == 1 && helmArmor > 0 && helmArmor > damageTaken)
		{
			helmArmor -= damageTaken;
			damageTaken = 0;
		}

		if (armorHit == 0 && damageTaken > 0)
		{
			damageTaken -= shieldArmor;
			shieldArmor = 0;
			shield.Protection = 0;
			shield.upgradeProtection();
			if(helmArmor > 0 && helmArmor > damageTaken)
			{
				helmArmor -= damageTaken;
				damageTaken = 0;
			}
			else
			{
				damageTaken -= helmArmor;
				helmArmor = 0;
				helm.Protection = 0;
				helm.upgradeProtection();
			}
		}
		else if (armorHit == 1 && damageTaken > 0)
		{
			damageTaken -= helmArmor;
			helmArmor = 0;
			helm.Protection = 0;
			helm.upgradeProtection();
			if(shieldArmor > 0 && shieldArmor > damageTaken)
			{
				shieldArmor -= damageTaken;
				damageTaken = 0;
			}
			else
			{
				damageTaken -= helmArmor;
				shieldArmor = 0;
				shield.Protection = 0;
				shield.upgradeProtection();
			}
		}

		health -= damageTaken;
		if (health <= 0)
		{
			die(this.gameObject);
			playerDead = true;
		}
	}
	public Weapon CurrentWeapon
	{
		get{ return currentWeapon; }
	}
	public int Health
	{
		get{ return health; }
		set{ health = value; }
	}
	public Weapon[] Weapons
	{
		get{ return weapons; }
		set{ weapons = value; }
	}
	
	public int ShieldMaxArmor
	{
		get{ return shieldMaxArmor; }
		set{ shieldMaxArmor = value; }
	}
	
	public int HelmMaxArmor
	{
		get{ return helmMaxArmor; }
		set{ helmMaxArmor = value; }
	}
	
	public int ShieldArmor
	{
		get{ return shieldArmor; }
		set{ shieldArmor = value; }
	}
	public int HelmArmor
	{
		get{ return helmArmor; }
		set{ helmArmor = value; }
	}
	
	public int LobsterParts
	{
		get{ return lobsterParts; }
		set{ lobsterParts = value; }
	}
	
	public int WeaponParts
	{
		get{ return weaponParts; }
		set{ weaponParts = value; }
	}

	public bool ShieldOn
	{
		get{ return shieldOn; }
		set{ shieldOn = value; }
	}
	
	public bool HelmOn
	{
		get{ return helmOn; }
		set{ helmOn = value; }
	}	
}
