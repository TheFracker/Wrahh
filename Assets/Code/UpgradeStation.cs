using UnityEngine;
using System.Collections;


//Script Used when accessing the upgrade station, creates an gui menu used to perform equipment upgrades

public class UpgradeStation : MonoBehaviour
{	
	public Wrahh wrahh; 									//Grants Access to Getters/Setters within Wrahhs Script
	public Shield shield;									//Grants Access to Getters/Setters within Shield Script
	public Helm helm;										//Grants Access to Getters/Setters within Helm Script

	public GUISkin mySkin;
	float screenW = Screen.width;
	float screenH = Screen.height;

	int currentWeapon;									//Creates an int to keep track of which weapon is upgraded
	Rect window300X300;									//Creates a gui rect containing buttons					

	//Different variables used to toggle on and off different sections of the upgrade menu
	bool menuShow = false;								
	bool statsShow = false;
	bool armorShow = false, helmShow = false, shieldShow = false;
	bool weaponShow = false, weaponUpgradeShow = false, rifleShow = false;
	
	int weaponRepairPrice = 0; 											//Used for calculating the repair price of weapons
	bool paused = false, playerEnter = true;							//Used for entering a pause mode when entering the Upgrade station

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		window300X300 = new Rect (screenW * 0.5f, screenH * 0.3f, 300 ,300); // Defines the size of the standard Gui Rect
	}

	//On trigger to open up the gui menu and pause the game
	void OnTriggerEnter2D(Collider2D other)
	{
		paused = true; 								//Pauses the game as long as this is true

	if (paused)
		if (other.tag == "Player" && playerEnter){	//Checks if it is the player entering which is only possible when playerEnter = true
			menuShow = true;						//Shows Gui Main Menu			
			Time.timeScale = 0; 					//Sets time to 0, which pauses the game
			playerEnter = false; 					//Player enter is false meaning he cannot reenter the menu until it is true
		}
	
	}
	void OnTriggerExit2D(Collider2D other)
	{
		playerEnter = true; // Sets it to true when exiting the menu, the player can now access the menu again if he wasent finished
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		//Shows different menus depending on which variable is true each having their own ID to avoid conflicts in between.
		if(menuShow)			//if true, Shows the main menu with the title "Upgrade Station", with the size of the window300X300 variable
			window300X300 = GUI.Window(0, window300X300, mainWindowFunc, "Upgrade Station");	//if true, it will refer you to the mainWindowFunc()
		if(armorShow)			//if true, Shows the main menu with the title "Armor Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(1, window300X300, armorMenuFunc, "Armor Upgrades");		//if true, it will refer you to the armorMenuFunc()
		if(shieldShow)			//if true, Shows the main menu with the title "Shield Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(3, window300X300, shieldMenuFunc, "Shield Upgrades");	//if true, it will refer you to the shieldMenuFunc()
		if(helmShow)			//if true, Shows the main menu with the title "Helm Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(4, window300X300, helmMenuFunc, "Helm Upgrades");		//if true, it will refer you to the helmMenuFunc()
		if(weaponShow)			//if true, Shows the main menu with the title "Weapon Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(5, window300X300, weaponMenuFunc, "Weapon Upgrades");	//if true, it will refer you to the weaponMenuFunc()
		if(weaponUpgradeShow) 	// if true, Shows the main menu with the title "Weapon Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(6, window300X300, weaponUpgradeFunc, "Weapon Upgrades"); //if true, it will refer you to the weaponUpgradeFunc()
	}


	void mainWindowFunc(int id)
	{
		//Used to send the player to the different menus
		if(GUILayout.Button("Weapon Upgrades"))	//Gui button created, will be seen multiple times
		{
			menuShow = false; 	//Will close the mainWindowFunc to make sure only one window is open at a time (windows spawn at same location)
			weaponShow = true;	//Will Open the weaponMenuFunc
		}

		if(GUILayout.Button("Armor Upgrades"))
		{
			menuShow = false; 	//Closes the mainWindowFunc
			armorShow = true;	//Opens the armorMenuFunc
		}

		if(GUILayout.Button("I'm Done Upgrading!"))
		{
			paused = false;		//Sets paused to false so that you can reenter paused mode when reentering the menu.
			menuShow = false;	//Closes the mainWindowFunc
			Time.timeScale = 1; //Resets the time to 1, meaning regular play pace.
		}
	}

	void armorMenuFunc(int id)
	{
		//Armor Menu this part is used to navigate according to which part of your armor you whish to upgrade

		if(GUILayout.Button("Helm"))
		{
			armorShow = false; 	//Continues to close current window to replace it with a new one
			helmShow = true; 	//Opens HelmMenuFunc
		}

		if(GUILayout.Button("Shield"))
		{
			armorShow = false;	//Closes ArmorMenuFunc
			shieldShow = true;	//Opens ShieldMenuFunc
		}

		if(GUILayout.Button("<- Return"))
		{ //Button to return to 
			armorShow = false;	//Closes ArmorMenuFunc
			menuShow = true;	//Opens Menu
		}
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;		//Again Allows to reenter paused mode
			armorShow = false;	//Closes ArmorMenuFunc
			Time.timeScale = 1;	//Sets time to standard: 1
		}
	}
	void helmMenuFunc(int id)
	{
		//If helm GuiButton is clicked this menuFunc will be accessed, allowing to upgrade helm protection

		if(GUILayout.Button("Craft Helm \n Costs 5 Lobster Parts"))	//Option to craft and equip a helm
		{
			if (wrahh.LobsterParts >= 5 && wrahh.HelmOn == false)	//Checks if wrahh has the required lobster parts (5) and that helm is not already equiped
			{
				wrahh.LobsterParts -= 5;	//Removes 5 lobster parts from wrahh(the price) 
				helm.Protection = 1;		//Initialices helm Protection at 1
				helm.upgradeProtection();	//Calling function within Helm.cs to set the current protection of the helm
				wrahh.HelmOn = true;		//Sets wrahhs helmOn Variable to true (used when destroying helm) and to check if helm is craftable
				wrahh.transform.FindChild("wrahh_BODY").transform.FindChild("helmet").gameObject.SetActive(true);	//Sets helm child object true with in player "parent"
			}
			else
				Debug.Log("Helm \n Costs 5 Lobster Parts"); //Debugs Helm is equiped if wrahh already is wearing a helm
		}

		if(GUILayout.Button("Repair Helm \n Costs 5 Lobster Parts")) //Option to repair helm and restore its armor to max
		{
			//Checks if (equiped) helmArmor is less than helmMaxArmor and if wrahh has the 5 lobster parts repairing costs 
			if (wrahh.HelmArmor < wrahh.HelmMaxArmor && wrahh.LobsterParts >= 5){
				wrahh.LobsterParts -= 5; //Removes the price of 5 lobster parts from wrahh
				wrahh.HelmArmor = wrahh.HelmMaxArmor; //Sets helmArmor equal to helmMaxArmor
			}
		}

		if(GUILayout.Button("Upgrade Protection \n Costs 5 Lobster Parts"))
		{ 	//Button which upgrades protection of helm according to  
			//This if/Else if statement runs according to the protection level of the Helm equipped
			//Each statement checks if wrahh has 5 lobster parts (price of upgrade) and if the helm is On

			if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 1) //If protectionLevel is 1 this statement is triggered
			{
				helm.Protection = 2;		//helm.Protection is reset to 2 so that next time the next statement will evaluate to true
				wrahh.LobsterParts -= 5; 	//Lobster parts is decreased with 5
				helm.upgradeProtection();	//helm.upgradeProtection() function is run and will upgrade the helms stats
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 2) //If protectionLevel is 2 this statement is triggered
			{
				helm.Protection = 3;		//helm.Protection is reset to 3 so that next time the next statement will evaluate to true
				wrahh.LobsterParts -= 5;	//Lobster parts is decreased with 5
				helm.upgradeProtection();	//helm.upgradeProtection() function is run and will upgrade the helms stats
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 3) //If protectionLevel is 3 this statement is triggered
			{
				helm.Protection = 4;		//helm.Protection is reset to 4 so that next time the next statement will evaluate to true
				wrahh.LobsterParts -= 5;	//Lobster parts is decreased with 5
				helm.upgradeProtection();	//helm.upgradeProtection() function is run and will upgrade the helms stats
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 4) //If protectionLevel is 4 this statement is triggered
			{
				helm.Protection = 5;		//helm.Protection is reset to 5 so that next time the next statement will evaluate to true
				wrahh.LobsterParts -= 5;	//Lobster parts is decreased with 5
				helm.upgradeProtection();	//helm.upgradeProtection() function is run and will upgrade the helms stats
			}
			else if (helm.Protection == 5) 	//If protectionLevel is 5 Max level have been reached and it will print Max Level to the Log
			{
				Debug.Log ("Max Level!");
			}
			else
				Debug.Log ("Cannot Upgrade"); //will be triggered if wrahh cannot upgrade either because he has no helm or can't afford.
		}

		if(GUILayout.Button("<- Return")){ 	//Will return user to previous menu
			helmShow = false;				//Closing helmMenu
			armorShow = true;				//Opening Armor Menu
		}
		if(GUILayout.Button("I'm Done Upgrading!!")) //Exiting menu
		{
			paused = false;					//Alows to pause game again
			helmShow = false;				//Closes HelmShow
			Time.timeScale = 1;				// Sets time to standard (Unpause)
		}
	}
	
	void shieldMenuFunc(int id)
	{
		// If wrahh has found a shield this function enables him to upgrade its armor
		if(GUILayout.Button("Repair Shield \n Costs 5 Lobster Parts")) //Button which Repairs shield armor
		{
																//Checks if shieldArmor is less than ShieldMaxArmor and wrahh can afford upgrade (has more than 5 lobster parts)
			if (wrahh.ShieldArmor < wrahh.ShieldMaxArmor && wrahh.LobsterParts >= 5){ 
				wrahh.LobsterParts -= 5; 						//Wrahh LobsterParts is reduced by 5
				wrahh.ShieldArmor = wrahh.ShieldMaxArmor; 		//Shield Armor is set equal to shield Max Armor
			}
		}

		if(GUILayout.Button("Upgrade Protection \n Costs 5 Lobster Parts"))
		{ 										//Button which upgrades shield protectionLevel
												//If-/else if- statement which upgrades shield according to current protectionLevel
												//Each function statement checks if wrahh has a shield and if he has minimum 5 lobsterparts
			if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 1) //If shield.Protection is 1 this function is beeing run
			{
				wrahh.LobsterParts -= 5;		//reduces wrahhs lobster parts by 5
				shield.Protection = 2; 			//sets Shield.Protection equal to 2 so that next upgrade will evaluate to true
				shield.upgradeProtection(); 	//shield.upgradeProtection() is called to activate the new upgrade level
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 2)	//If shield.Protection is 2 this function is beeing run
			{
				shield.Protection = 3;			//reduces wrahhs lobster parts by 5
				wrahh.LobsterParts -= 5;		//sets Shield.Protection equal to 3 so that next upgrade will evaluate to true
				shield.upgradeProtection();		//shield.upgradeProtection() is called to activate the new upgrade level
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 3) 	//If shield.Protection is 3 this function is beeing run
			{
				shield.Protection = 4;			//reduces wrahhs lobster parts by 5	
				wrahh.LobsterParts -= 5;		//sets Shield.Protection equal to 4 so that next upgrade will evaluate to true
				shield.upgradeProtection();		//shield.upgradeProtection() is called to activate the new upgrade level
			}
			else if (shield.Protection == 4)	//If shield.Protection is 4 this function is beeing run
			{
				Debug.Log ("Max Level!");  		//Logs that the max level is achieved
			}
			else
				Debug.Log ("Cannot Upgrade");
		}
		if(GUILayout.Button("<- Return"))
		{ 								//Returns to previous menu
			shieldShow = false;			//Closes Shield Menu
			armorShow = true;			//Opens Armor Menu
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;				//Allows acces to pause mode again
			shieldShow = false;			//Closes current Menu
			Time.timeScale = 1;			//Resets time to default 1
		}
	}

	void weaponMenuFunc(int id)
	{									//Function used to print out all of wrahhs gathered weapons (max 5) into the gui menu so the player can upgrade them individually
		if(wrahh.Weapons[0].getName() != "Fists")
		{ 								//Accessing location 0 of wrahhs weapon array and check if it is an item (not his fists)
										//If it is a weapon and not his fists, a button will be created containing weapon name and weapon durabillity
		if(GUILayout.Button(wrahh.Weapons[0].getName() + " " + " Durabillity: " + wrahh.Weapons[0].getDura() + "/" + wrahh.Weapons[0].getMAXDura())){
			currentWeapon = 0;			//sets currentWeapon value to 0 if button is pressed, used to modify this exact weapon
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;	//Calculates repair price from weapon MAXdura and current Durabillity
			weaponShow = false; 		//Closes weapon menu function 
			weaponUpgradeShow = true; 	//Opens Weapon Upgrade Menu
			}
		}

		if(wrahh.Weapons[1].getName() != "Fists")
		{								//Accessing location 1 of wrahhs weapon array and check if it is an item (not his fists)
										//If it is a weapon and not his fists, a button will be created containing weapon name and weapon durabillity
		if(GUILayout.Button(wrahh.Weapons[1].getName() + "," + " Durabillity: " + wrahh.Weapons[1].getDura() + "/" + wrahh.Weapons[1].getMAXDura())){
			currentWeapon = 1;			//sets currentWeapon value to 1 if button is pressed, used to modify this exact weapon
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2; //Calculates repair price from weapon MAXdura and current Durabillity
			weaponShow = false;			//Closes weapon menu function
			weaponUpgradeShow = true;	//Opens Weapon Upgrade Menu		
			}
		}

		if(wrahh.Weapons[2].getName() != "Fists")
		{								//Accessing location 2 of wrahhs weapon array and check if it is an item (not his fists)
										//If it is a weapon and not his fists, a button will be created containing weapon name and weapon durabillity
		if (GUILayout.Button(wrahh.Weapons[2].getName()+ "," + " Durabillity: " + wrahh.Weapons[2].getDura() + "/" + wrahh.Weapons[2].getMAXDura())){
			currentWeapon = 2; 			//sets currentWeapon value to 2 if button is pressed, used to modify this exact weapon
				weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2; //Calculates repair price from weapon MAXdura and current Durabillity
			weaponShow = false;			//Closes weapon menu function
			weaponUpgradeShow = true;	//Opens Weapon Upgrade Menu		
			}
		}

		if(wrahh.Weapons[3].getName() != "Fists")
		{								//Accessing location 3 of wrahhs weapon array and check if it is an item (not his fists)
										//If it is a weapon and not his fists, a button will be created containing weapon name and weapon durabillity
		if (GUILayout.Button( wrahh.Weapons[3].getName()+ "," + " Durabillity: " + wrahh.Weapons[3].getDura() + "/" + wrahh.Weapons[3].getMAXDura())){
				currentWeapon = 3;		//sets currentWeapon value to 3 if button is pressed, used to modify this exact weapon
				weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2; //Calculates repair price from weapon MAXdura and current Durabillity
			weaponShow = false;			//Closes weapon menu function
			weaponUpgradeShow = true;	//Opens Weapon Upgrade Menu		
			}
		}

		if(wrahh.Weapons[4].getName() != "Fists")
		{ 								//Accessing location 4 of wrahhs weapon array and check if it is an item (not his fists)
										//If it is a weapon and not his fists, a button will be created containing weapon name and weapon durabillity
		if (GUILayout.Button(wrahh.Weapons[4].getName()+ "," + " Durabillity: " + wrahh.Weapons[4].getDura() + "/" + wrahh.Weapons[4].getMAXDura())){
			currentWeapon = 4;			//sets currentWeapon value to 4 if button is pressed, used to modify this exact weapon
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2; //Calculates repair price from weapon MAXdura and current Durabillity
			weaponShow = false;			//Closes weapon menu function
			weaponUpgradeShow = true;	//Opens Weapon Upgrade Menu		
			}
		}
		if(GUILayout.Button("<- Return"))
		{ 												//Returns to MainMenu
			weaponShow = false; 						//Closes weapon Menu Function
			menuShow = true;							//Opens Main Menu 
		}
		if(GUILayout.Button("I'm Done Upgrading!!")) 	//Exits upgrade Gui
		{
			paused = false;								//Allows to reenter Pause mode
			weaponShow = false;							//Closes current menu window (WeaponMenu)
			Time.timeScale = 1;							//Sets time to standard 1
		}
	}

	void weaponUpgradeFunc(int id)
	{
		//This function allows wrahh to make changes to the weapon he has choosen
		if(GUILayout.Button("Repair Price: " + weaponRepairPrice + " Weapon Parts")) //Allows wrahh to repair weapon of his choice
		{
			//Statement which checks if wrahh has enough lobsterparts to repair his the weapon and if the weapon has lost durabillity
			if (wrahh.WeaponParts >= weaponRepairPrice && wrahh.Weapons[currentWeapon].getDura() < wrahh.Weapons[currentWeapon].getMAXDura()){
				wrahh.WeaponParts -= weaponRepairPrice;						//Reduces wrahhs lobster parts with the price of repair
				wrahh.Weapons[currentWeapon].setDura(wrahh.Weapons[currentWeapon].getMAXDura());//Sets weapon durabillity equal to max durabillity
				weaponUpgradeShow = false; 									//Closes WeaponUpgradeMenu
				weaponShow = true; 											//Reenters Menu which shows all weapons
			}
			else if (wrahh.Weapons[currentWeapon].getDura() == wrahh.Weapons[currentWeapon].getMAXDura())//Tells if wrahh is already at max durabillity
				Debug.Log("Already at max durabillity");
			else 
				Debug.Log("Can't affor Upgrade");
		}

		if(GUILayout.Button("Upgrade Durabillity (7) Weapon Parts"))
		{ 																	//Button which allows for durabillity upgrade of weapon
																			//Statements which checks current durabillityLevel of CurrentWeapon and if he can afford upgrade (7) Weapon Parts
			if (wrahh.Weapons[currentWeapon].DurabillityLevel == 0 && wrahh.WeaponParts >= 7)
			{ 																//If durabillityLevel = 0 this will be run
				wrahh.Weapons[currentWeapon].DurabillityLevel = 1;			//DurabillityLevel is increased and set to 1
				wrahh.WeaponParts -= 7;										//Weapon Parts is reduced with 7							
				wrahh.Weapons[currentWeapon].upgradeLevel();				//Runs upgradeLevel() function to reset durabillity to new
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 1 && wrahh.WeaponParts >= 7)
			{																//If durabillityLevel = 1 this will be run
				wrahh.Weapons[currentWeapon].DurabillityLevel = 2;			//DurabillityLevel is increased and set to 2
				wrahh.WeaponParts -= 7;										//Weapon Parts is reduced with 7
				wrahh.Weapons[currentWeapon].upgradeLevel();				//Runs upgradeLevel() function to reset durabillity to new
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 2 && wrahh.WeaponParts >= 7)
			{																//If durabillityLevel = 2 this will be run
				wrahh.Weapons[currentWeapon].DurabillityLevel = 3;			//DurabillityLevel is increased and set to 2
				wrahh.WeaponParts -= 7;										//Weapon Parts is reduced with 7
				wrahh.Weapons[currentWeapon].upgradeLevel();				//Runs upgradeLevel() function to reset durabillity to new
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 3)	//If durabillityLevel = 3 this will be run
				Debug.Log ("Max Level");
			else 
				Debug.Log ("Can't Upgrade");
		}

		if(GUILayout.Button("Upgrade Range \n Costs 5 Weapon Parts"))
		{ 																	//Button which allows range upgrades of currentWeapon
																			//Checks currentWeapons rangeLevel and if wrahh can afford the upgrade (5) weapon parts)
			if (wrahh.Weapons[currentWeapon].RangeLevel == 0 && wrahh.WeaponParts >= 7)
			{ 																//If RangeLevel Equals 0 this will be run
				wrahh.Weapons[currentWeapon].RangeLevel = 1;				//resets currentWeapons rangeLevel to 1
				wrahh.WeaponParts -= 7;										//Reduces wrahhs Weapon Parts with 7
				wrahh.Weapons[currentWeapon].upgradeLevel();				//Acces upgradeLevel to change the values within the weapon to that of level 1
			}
			else if (wrahh.Weapons[currentWeapon].RangeLevel == 1 && wrahh.WeaponParts >= 7)
			{																//If RangeLevel Equals 1 this will be run
				wrahh.Weapons[currentWeapon].RangeLevel = 2;				//resets currentWeapons rangeLevel to 2
				wrahh.WeaponParts -= 7;										//Reduces wrahhs Weapon Parts with 7
				wrahh.Weapons[currentWeapon].upgradeLevel();				//Acces upgradeLevel to change the values within the weapon to that of level 2
			}
			else if (wrahh.Weapons[currentWeapon].RangeLevel == 2)			//If RangeLevel Equals 2 this will be run
				Debug.Log ("Max Level");
			else 
				Debug.Log ("Can't Upgrade");
		}

		//Button which allows currentWeapon to be Dissasembled for weapon parts equal to weapon durabillity
		if(GUILayout.Button("Disassemble for: " + wrahh.Weapons[currentWeapon].getDura() + " Weapon Parts")){ 
			wrahh.WeaponParts += wrahh.Weapons[currentWeapon].getDura();	//Adds weapons current durabillity to wrahhs weapon parts
			Destroy(wrahh.Weapons[currentWeapon]);							//Destroy the weapon from wrahhs weapon array
			wrahh.Weapons[currentWeapon] = gameObject.AddComponent<Weapon>();//adds standard weapon (Fists)
			weaponShow = true;												//enters weaponShowMenu
			weaponUpgradeShow = false;										//Closes weaponUpgradeMenu	
		}

		if(GUILayout.Button("<- Return")){
			weaponUpgradeShow = false;		//Closes WeaponUpgradeMenu
			weaponShow = true;				//Opens WeaponShowMenu
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;					//Allows player to enter pause mode again
			weaponUpgradeShow = false;   	//Closes WeaponUpgradeMenu Window
			Time.timeScale = 1;				//Resets timeScale to standard 1		
		}
	}
}