using UnityEngine;
using System.Collections;

public class Wrahh : GameCharacters
{
	int lobsterParts;											// Lobster parts are used to buy helmets and can also be used to upgrade and repair helmets and shields
	int weaponParts;											// Weapon parts are used to upgrade and repair weapons
	bool shieldOn;												// Used to check if Wrahh has a shield
	bool helmOn;												// Used to check if Wrahh has a helmet
	int shieldMaxArmor;											// The maximum armor that the shield can provide
	int helmMaxArmor;											// The maximum armor that the helmet can provide
	int shieldArmor;											// The current armor of the shield
	int helmArmor;												// The current armor of the helmet
	int currentSlot;											// The current weapon slot used 1-5
	bool walkSoundPlaying;										// Used to check if a sound of Wrahh walking is played

	public Shield shield;										// The shield gameobject
	public Helm helm;											// The helmet gameobject

	public static bool canCrushEnemy = false;					// Used to check if Wrahh is able to crush an enemy, set to true when he is falling
	
	Weapon[] weapons = new Weapon[5];							// An array of weapons used to store the weapons that he picks up
	Weapon currentWeapon;										// The weapon that Wrahh is currently using
	int grenades;												// The number of grenades that Wrahh has on his person
	
	private float currentSpeed;									// Is the current speed of Wrahh
	private float climbSpeed = 5f;								// Is the speed at which Wrahh can climb a ladder

	AudioSource[] sounds; 										// creates an array "sounds" of type "AudioSource"
	int numberOfSounds;											// The number of sounds in total
	int numberOfWalkingSounds;									// The number of walking sounds
	int numberOfFallingSounds;									// The number of falling sounds
	int numberOfHitGroundSounds;								// The number of sounds of Wrahh hitting the ground
	int numberOfPunchSounds;

	public Material wrahhMat;

	//////////////////////////////////
	// START 			    		//
	//////////////////////////////////
	void Start ()
	{
		// This is used so that there will not be a constant call, with Wrahh calling eg shield, and shield calling Wrahh, as they need information about each other
		if(shield == null)
			shield = new Shield();
		if(helm == null)
			helm = new Helm();

		// At the begging of the game, Wrahh is stripped of everything, and he therefore does not have a shield, no helmet, no grenades, and no armor
		shieldOn = false;
		helmOn = false;
		grenades = 0;
		weaponParts = 100;	// Set to 0?
		lobsterParts = 10;	// Set to 0?
		shieldArmor = 0;
		helmArmor = 0;

		// Loads the standard weapon (his bare hands) in to all slots in the weapon array
		for(int i = 0; i < 5; i++)
		{
			weapons[i] = gameObject.AddComponent<Weapon>();
		}

		// Sets the start slot to the first slot in the array, and set his current weapon
		currentSlot = 0;
		currentWeapon = weapons[currentSlot];

		// Lots of sounds being loaded
		walkSoundPlaying = false;
		numberOfSounds = 17;
		numberOfWalkingSounds = 14;
		numberOfFallingSounds = 1+numberOfWalkingSounds;
		numberOfHitGroundSounds = 1+numberOfFallingSounds;
		numberOfPunchSounds = 1+numberOfHitGroundSounds;
		for (int i = 0; i<numberOfSounds;i++)
		{
			this.gameObject.AddComponent<AudioSource>();
		}
		sounds = GetComponents<AudioSource>();						//all audio source components on the object it put in the "sounds" array in the order they are listed on the object
		for (int i = 0; i<numberOfSounds;i++)
		{
			while(i<numberOfWalkingSounds)
			{
				sounds[i].clip = Resources.Load("sounds/walk-"+(1+i)) as AudioClip;
				sounds[i].playOnAwake = false;
				sounds[i].rolloffMode = AudioRolloffMode.Linear;
				sounds[i].pitch = 1f;
				sounds[i].volume = 0.1f;
				i++;
			}

			while(i<numberOfFallingSounds)
			{
				sounds[i].clip = Resources.Load("sounds/screamFall") as AudioClip;
				sounds[i].playOnAwake = false;
				sounds[i].rolloffMode = AudioRolloffMode.Linear;
				sounds[i].pitch = 1.0f;
				sounds[i].volume = 1.0f;
				sounds[i].loop = true;
				i++;
			}

			while(i<numberOfHitGroundSounds)
			{
				sounds[i].clip = Resources.Load("sounds/hitGround") as AudioClip;
				sounds[i].playOnAwake = false;
				sounds[i].rolloffMode = AudioRolloffMode.Linear;
				sounds[i].pitch = 1.0f;
				sounds[i].volume = 1.0f;
				sounds[i].loop = false;
				i++;
			}
			while(i<numberOfPunchSounds)
			{
				sounds[i].clip = Resources.Load("sounds/punch") as AudioClip;
				sounds[i].playOnAwake = false;
				sounds[i].rolloffMode = AudioRolloffMode.Linear;
				sounds[i].pitch = 1.0f;
				sounds[i].volume = 0.5f;
				sounds[i].loop = false;
				i++;
			}
		}

	//From parent "GameCharacters.cs":
		moveSpeed = 10000.0f; // The moveSpeed is very high, because he is heavy, and he is moved by adding force
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
		
		// Hitting
		if (Input.GetKeyUp (KeyCode.Space) && anim.GetBool("isAttacking") == false)
		{
			useWeapon (currentWeapon);
			sounds[16].Play();
		}

		// Change weapon
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

		float input = 0;																// creates a local variable "input"

		falling ();
		if (anim.GetBool("IsFalling") == false && anim.GetBool("HitGround") == false){	// checks if the player is not falling or splatted out
			input = Input.GetAxis ("Horizontal"); 										// local variable (a float going from -1 - 1) depending on if you push "A"/"left key" or "D"/"right key" 
			climbingLadder();															// runs the "climbingLadder" function 
			crawlMonkeyBars(); 															// runs the "crawlMonkyBars" function 
		}

		// Moves Wrahh
		if (input * rigidbody2D.velocity.x < MAX_MOVE_SPEED)
			rigidbody2D.AddForce (Vector2.right * input * moveSpeed);

		// Plays sound when Wrahh moves
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
		if (this.rigidbody2D.velocity.y < -2.5 && anim.GetBool("IsFalling")==false)
		{
			anim.SetBool("IsFalling", true);
			canCrushEnemy = true;
			sounds[14].Play();
		}
		
		if (this.rigidbody2D.velocity.y > -0.5 && anim.GetBool("IsFalling") == true && anim.GetBool("HitGround") == false)
		{
			sounds[14].Stop();
			sounds[15].Play();
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
		yield return new WaitForSeconds(0.5f);
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
			Destroy(weapons[currentSlot]);
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

	// Checks if the inventory is full, (standard weapon (bare hands) is not considered a weapon in this case), and if it is, will return true,
	// so he is not able to pick up a new weapon
	bool inventoryFull()
	{
		for(int i = 0; i < 5; i++)
		{
			if(weapons[i].getName() == "Fists")
				return false;
		}
		return true;
	}

	// Finds the first available inventory slot
	int emptyInventorySlot()
	{
		for(int i = 0; i < 5; i++)
		{
			if(weapons[i].getName() == "Fists")
			{
				return i;
			}
		}
		// Should never reach this point, just put here because other wise the compiler will complain.
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
		// Pick up lobster parts used for buying helmets, and upgrading and reparing shields and helmets
		if (c.tag == "Item")													
		{
			if (c.gameObject.name == "lobsterParts")
			{
				Destroy(c.gameObject);
				lobsterParts += 5;
			}

			// If the inventory is not full, Wrahh is able to pick up a weapon
			// When a weapon is picked up, it will be added to the first empty slot in the weapon array, and if it is
			// stronger than the currently equipped, Wrahh will change to that.
			// Bare hands < Pistol < Rifle
			if(!inventoryFull())
			{
				if (c.gameObject.name == "gunPickUp")
				{
					int slot = emptyInventorySlot();
					Debug.Log ("Picking up guns");
					Destroy(c.gameObject);
					Destroy(weapons[slot]);
					weapons[slot] = gameObject.AddComponent<Pistol>();
					currentSlot = slot;
					if(currentWeapon.getName() == "Fists")
						currentWeapon = weapons[currentSlot];
				}
				
				if (c.gameObject.name == "riflePickUp")
				{
					int slot = emptyInventorySlot();
					Debug.Log ("Picking up rifles");
					Destroy(c.gameObject);
					Destroy(weapons[slot]);
					weapons[slot] = gameObject.AddComponent<Rifle>();
					currentSlot = slot;
					if(currentWeapon.getName() == "Fists" || currentWeapon.getName() == "Pistol")
						currentWeapon = weapons[currentSlot];
				}
			}
		}

		// Picks up a med pack
		if (c.tag == "Buff")												
		{
			if (c.gameObject.name == "healthBuff")
			{
				Debug.Log ("Picking up health buff");
				Destroy(c.gameObject);
				health += 10;
			}
		}

		// Picks up armor
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

	//////////////////////////////////////
	// EQUIPED WEAPON					//
	//////////////////////////////////////

	// Checks what weapon Wrahh currently has equipped and sets that weapon (which is a child of his back arm) to true. If a weapons range is upgraded, eg the rifle, more rifles will
	// added, showing them as extended
	public void equipedWeapon()
	{

		if (currentWeapon.getName() == "Rifle")
		{
			if (currentWeapon.RangeLevel == 0){
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(true);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun2").gameObject.SetActive(false);
			}
			else if (currentWeapon.RangeLevel == 1){
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun2").gameObject.SetActive(false);
			}
			else if (currentWeapon.RangeLevel == 2){
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle2").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun2").gameObject.SetActive(false);
			}
		}
		else if (currentWeapon.getName() == "Pistol")
		{
			if (currentWeapon.RangeLevel == 0){
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle2").gameObject.SetActive(false);
			}
			else if (currentWeapon.RangeLevel == 1){
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle2").gameObject.SetActive(false);
			}
			else if (currentWeapon.RangeLevel == 2){
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun2").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(true);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(false);
				this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle2").gameObject.SetActive(false);
			}
		}
		else if (currentWeapon.getName() == "Fists")
		{
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle1").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_rifle2").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun1").gameObject.SetActive(false);
			this.transform.FindChild("wrahh_arm_BACK").transform.FindChild("weapon_gun2").gameObject.SetActive(false);

		}
	}

	IEnumerator waitForBlink()
	{
		while(isHurt)
		{
			yield return new WaitForSeconds(0.1f);
			foreach (Transform child in transform)
			{
				child.renderer.material.color = Color.white;
			}
			isHurt = false;
		}
	}
	//////////////////////////////////////
	// WRAHH TAKES DAMAGE				//
	//////////////////////////////////////
	public void hurt(Projectile p)
	{
		foreach (Transform child in transform)
		{
			child.renderer.material.color = Color.red;
		}
		isHurt = true;
		StartCoroutine("waitForBlink");

		// The damage that Wrahh is about to take is kept in the damageTaken variable.
		// The damage is first takes away his armor, and then start to take Wrahh's health 

		int damageTaken = p.giveDamage ();
		int armorHit = Random.Range(0,2);	// Randomly assings what part of Wrahh's armor takes the first hit

		// If the damage is less than the currenlty worn armor, only armor will be lost.
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

		// If the damage is higher than the armor that takes the first hit, that piece of armor will be destroyed, and the rest of the damage will be applied to the
		// other armor part, if that is still equiped.
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

		// Any damage left after the armor has been hit, if any armor left, will take away his health.
		// And if the health takes away the last of Wrahh's health, he will die, and the game will end.

		health -= damageTaken;
		if (health <= 0)
		{
			// Calls die from GameCharacters class
			die(this.gameObject);
		}
	}

	// Lots of getters and setters. These are used to by the hud to show different stats to the user.
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
