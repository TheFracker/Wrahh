using UnityEngine;
using System.Collections;


//Script Used when accessing the upgrade station, creates an gui menu used to perform equipment upgrades

public class UpgradeStation : MonoBehaviour
{	
	public Wrahh wrahh; 									//Grants Access to Getters/Setters within Wrahhs Script
	public Shield shield;								//Grants Access to Getters/Setters within Shield Script
	public Helm helm;										//Grants Access to Getters/Setters within Helm Script

	public GUISkin mySkin;

	int currentWeapon;									//Creates an int to keep track of which weapon is upgraded

	Rect window300X300;									//Creates a gui rect containing buttons					

	//Different variables used to toggle on and off different sections of the upgrade menu
	bool menuShow = false;								
	bool statsShow = false;
	bool armorShow = false, helmShow = false, shieldShow = false;
	bool weaponShow = false, weaponUpgradeShow = false, rifleShow = false;
	
	int weaponRepairPrice = 0; 											//Used for calculating the repair price of weapons

	bool paused = false, playerEnter = true;							//Used for entering a pause mode when entering the Upgrade station

	void Start()
	{
		window300X300 = new Rect (Screen.width / 2-250, Screen.height / 2 - 100, 300,300); // Defines the size og the standard Gui Rect
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
		//Shows different menus depending on which variable is true each having their own ID to avoid conflicts in between.

		if(menuShow){	//if true, Shows the main menu with the title "Upgrade Station", with the size of the window300X300 variable
			window300X300 = GUI.Window(0, window300X300, mainWindowFunc, "Upgrade Station"); 
			//if true, it will refer you to the mainWindowFunc()
		}
		if(armorShow){	//if true, Shows the main menu with the title "Armor Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(1, window300X300, armorMenuFunc, "Armor Upgrades");
			//if true, it will refer you to the armorMenuFunc()
		}
		if(shieldShow){	//if true, Shows the main menu with the title "Shield Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(3, window300X300, shieldMenuFunc, "Shield Upgrades");
			//if true, it will refer you to the shieldMenuFunc()
		}
		if(helmShow){	//if true, Shows the main menu with the title "Helm Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(4, window300X300, helmMenuFunc, "Helm Upgrades");
			//if true, it will refer you to the helmMenuFunc()
		}
		if(weaponShow){	//if true, Shows the main menu with the title "Weapon Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(5, window300X300, weaponMenuFunc, "Weapon Upgrades");
			//if true, it will refer you to the weaponMenuFunc()

		}
		if(weaponUpgradeShow){	// if true, Shows the main menu with the title "Weapon Upgrades", with the size of the window300X300 variable
			window300X300 = GUI.Window(6, window300X300, weaponUpgradeFunc, "Weapon Upgrades");
			//if true, it will refer you to the weaponUpgradeFunc()
		}


	}


	void mainWindowFunc(int id)
	{
		//Used to send the player to the different menus

		if(GUILayout.Button("Weapon Upgrades Avaliable"))		//Gui button created, will be seen multiple times
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

		if(GUILayout.Button("Helm")){
			armorShow = false; 	//Continues to close current window to replace it with a new one
			helmShow = true; 	//Opens HelmMenuFunc
		}

		if(GUILayout.Button("Shield")){
			armorShow = false;	//Closes ArmorMenuFunc
			shieldShow = true;	//Opens ShieldMenuFunc
		}

		if(GUILayout.Button("Return")){ //Button to return to 
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

		if(GUILayout.Button("Craft Helm (5) Lobster Parts"))	//Option to craft and equip a helm
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
			{
				Debug.Log("Helm Already Equiped"); //Debugs Helm is equiped if wrahh already is wearing a helm
			}
		}

		if(GUILayout.Button("Repair Helm (5) Lobster Parts")) //Option to repair helm and restore its armor to max
		{
			//Checks if (equiped) helmArmor is less than helmMaxArmor and if wrahh has the 5 lobster parts repairing costs 
			if (wrahh.HelmArmor < wrahh.HelmMaxArmor && wrahh.LobsterParts >= 5){
				wrahh.LobsterParts -= 5; //Removes the price of 5 lobster parts from wrahh
				wrahh.HelmArmor = wrahh.HelmMaxArmor; //Sets helmArmor equal to helmMaxArmor
			}
		}

		if(GUILayout.Button("Upgrade Protection")){

			if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 1)
			{
				helm.Protection = 2;
				wrahh.LobsterParts -= 5;
				helm.upgradeProtection();
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 2)
			{
				helm.Protection = 3;
				wrahh.LobsterParts -= 5;
				helm.upgradeProtection();
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 3)
			{
				helm.Protection = 4;
				wrahh.LobsterParts -= 5;
				helm.upgradeProtection();
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.HelmOn && helm.Protection == 4)
			{
				helm.Protection = 5;
				wrahh.LobsterParts -= 5;
				helm.upgradeProtection();
			}
			else if (helm.Protection == 5)
			{
				Debug.Log ("Max Level!");
			}
			else
				Debug.Log ("Cannot Upgrade");
		}

		if(GUILayout.Button("Return")){
			helmShow = false;
			armorShow = true;
		}
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			helmShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}
	
	void shieldMenuFunc(int id)
	{
		if(GUILayout.Button("Repair Shield"))
		{
			if (wrahh.ShieldArmor < wrahh.ShieldMaxArmor && wrahh.LobsterParts >= 5){
				wrahh.LobsterParts -= 5;
				wrahh.ShieldArmor = wrahh.ShieldMaxArmor;
			}
		}

		if(GUILayout.Button("Upgrade Protection")){

			if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 1)
			{
				wrahh.LobsterParts -= 5;
				shield.Protection = 2;
				shield.upgradeProtection();
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 2)
			{
				shield.Protection = 3;
				wrahh.LobsterParts -= 5;
				shield.upgradeProtection();
			}
			else if (wrahh.LobsterParts >= 5 && wrahh.ShieldOn && shield.Protection == 3)
			{
				shield.Protection = 4;
				wrahh.LobsterParts -= 5;
				shield.upgradeProtection();
			}
			else if (shield.Protection == 4)
			{
				Debug.Log ("Max Level!");
			}
			else
				Debug.Log ("Cannot Upgrade");
		}
		
		if(GUILayout.Button("Return")){
			shieldShow = false;
			armorShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			shieldShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void weaponMenuFunc(int id)
	{
		if(wrahh.Weapons[0].getName() != "weapon")
		if(GUILayout.Button(wrahh.Weapons[0].getName() + "," + " Durabillity: " + wrahh.Weapons[0].getDura() + " Max " + wrahh.Weapons[0].getMAXDura())){
			currentWeapon = 0;
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;
			weaponShow = false;
			weaponUpgradeShow = true;
		}

		if(wrahh.Weapons[1].getName() != "weapon")
		if(GUILayout.Button(wrahh.Weapons[1].getName() + "," + " Durabillity: " + wrahh.Weapons[1].getDura() + " Max " + wrahh.Weapons[1].getMAXDura())){
			currentWeapon = 1;
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;
			weaponShow = false;
			weaponUpgradeShow = true;
		}

		if(wrahh.Weapons[2].getName() != "weapon")
		if (GUILayout.Button(wrahh.Weapons[2].getName()+ "," + " Durabillity: " + wrahh.Weapons[2].getDura() + " Max " + wrahh.Weapons[2].getMAXDura())){
			currentWeapon = 2;
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;
			weaponShow = false;
			weaponUpgradeShow = true;
		}

		if(wrahh.Weapons[3].getName() != "weapon")
		if (GUILayout.Button( wrahh.Weapons[3].getName()+ "," + " Durabillity: " + wrahh.Weapons[3].getDura() + " Max " + wrahh.Weapons[3].getMAXDura())){
			currentWeapon = 3;
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;
			weaponShow = false;
			weaponUpgradeShow = true;
		}

		if(wrahh.Weapons[4].getName() != "weapon")
		if (GUILayout.Button(wrahh.Weapons[4].getName()+ "," + " Durabillity: " + wrahh.Weapons[4].getDura() + " Max " + wrahh.Weapons[4].getMAXDura())){
			currentWeapon = 4;
			weaponRepairPrice = (wrahh.Weapons[currentWeapon].getMAXDura() - wrahh.Weapons[currentWeapon].getDura())/2;
			weaponShow = false;
			weaponUpgradeShow = true;
		}
	
		if(GUILayout.Button("Return")){
			weaponShow = false;
			menuShow = true;
		}

		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			weaponShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void weaponUpgradeFunc(int id)
	{
		if(GUILayout.Button("Repair Price: " + weaponRepairPrice + " Weapon Parts"))
		{
			if (wrahh.WeaponParts >= weaponRepairPrice && wrahh.Weapons[currentWeapon].getDura() < wrahh.Weapons[currentWeapon].getMAXDura()){
				wrahh.WeaponParts -= weaponRepairPrice;
				wrahh.Weapons[currentWeapon].setDura(wrahh.Weapons[currentWeapon].getMAXDura());
				weaponShow = true;
				weaponUpgradeShow = false;
			}
			else if (wrahh.Weapons[currentWeapon].getDura() == wrahh.Weapons[currentWeapon].getMAXDura())
			{
				Debug.Log("Already at max durabillity");
			}
			else 
			{
				Debug.Log("Can't affor Upgrade");
			}
		}

		if(GUILayout.Button("Upgrade Durabillity: 7 Weapon Parts")){

			if (wrahh.Weapons[currentWeapon].DurabillityLevel == 0 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].DurabillityLevel = 1;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 1 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].DurabillityLevel = 2;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 2 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].DurabillityLevel = 3;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}
			else if (wrahh.Weapons[currentWeapon].DurabillityLevel == 3)
			{
				Debug.Log ("Max Level");
			}
			else 
			{
				Debug.Log ("Can't Upgrade");
			}
		}

		if(GUILayout.Button("Upgrade Range: 5 Weapon Parts")){
			if (wrahh.Weapons[currentWeapon].RangeLevel == 0 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].RangeLevel = 1;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}
			else if (wrahh.Weapons[currentWeapon].RangeLevel == 1 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].RangeLevel = 2;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}
			/*else if (wrahh.Weapons[currentWeapon].RangeLevel == 2 && wrahh.WeaponParts >= 7){
				wrahh.Weapons[currentWeapon].RangeLevel = 3;
				wrahh.WeaponParts -= 7;
				wrahh.Weapons[currentWeapon].upgradeLevel();
			}*/
			else if (wrahh.Weapons[currentWeapon].RangeLevel == 2)
			{
				Debug.Log ("Max Level");
			}
			else 
			{
				Debug.Log ("Can't Upgrade");
			}
		}

		
		if(GUILayout.Button("Disassemble, Gain: " + wrahh.Weapons[currentWeapon].getDura() + " Weapon Parts")){
			wrahh.WeaponParts += wrahh.Weapons[currentWeapon].getDura();
			wrahh.Weapons[currentWeapon] = gameObject.AddComponent<Weapon>();
			weaponShow = true;
			weaponUpgradeShow = false;
		}

		if(GUILayout.Button("Return")){
			weaponUpgradeShow = false;
			weaponShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			weaponUpgradeShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void rifleMenuFunc(int id)
	{
		if(GUILayout.Button("Repair"))
		{
		}
		
		if(GUILayout.Button("Upgrade Durabillity")){
			Debug.Log("Pistol Durabillity is now: ");
		}
		
		if(GUILayout.Button("Upgrade range")){
			Debug.Log("Rifle range is now: ");
		}
		

		if(GUILayout.Button("Return")){
			rifleShow = false;
			weaponShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			rifleShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}
}