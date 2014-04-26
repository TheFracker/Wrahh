using UnityEngine;
using System.Collections;

public class UpgradeStation : MonoBehaviour {

	public Wrahh wrahh = new Wrahh();
	public Shield shield = new Shield();
	public Helm helm = new Helm();

	public GUISkin mySkin;

	int currentWeapon;

	Rect window300X300, statsWindow;
	bool menuShow = false;
	bool statsShow = false;
	bool armorShow = false, helmShow = false, shieldShow = false;
	bool weaponShow = false, weaponUpgradeShow = false, rifleShow = false;
		
	int weaponRepairPrice = 0;

	bool paused = false, playerEnter = true;

	void Start()
	{
		window300X300 = new Rect (Screen.width / 2-250, Screen.height / 2 - 100, 300,300);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		paused = true;

	if (paused)
		if (other.tag == "Player"&&playerEnter){
			menuShow = true;
			statsShow = true;
			Time.timeScale = 0;
			playerEnter = false;
		}
	
	}
	void OnTriggerExit2D(Collider2D other)
	{
		playerEnter = true;
	}

	void OnGUI()
	{
		if(menuShow){
			window300X300 = GUI.Window(0, window300X300, mainWindowFunc, "Upgrade Station");
		}
		if(armorShow){
			window300X300 = GUI.Window(1, window300X300, armorMenuFunc, "Armor Upgrades");
		}
		if(shieldShow){
			window300X300 = GUI.Window(3, window300X300, shieldMenuFunc, "Shield Upgrades");
		}
		if(helmShow){
			window300X300 = GUI.Window(4, window300X300, helmMenuFunc, "Helm Upgrades");
		}
		if(weaponShow){
			window300X300 = GUI.Window(5, window300X300, weaponMenuFunc, "Weapon Upgrades");

		}
		if(weaponUpgradeShow){
			window300X300 = GUI.Window(6, window300X300, weaponUpgradeFunc, "Weapon Upgrades");
		}


	}
	
	void mainWindowFunc(int id)
	{

		if(GUILayout.Button("Weapon Upgrades Avaliable"))
		{
			menuShow = false;
			weaponShow = true;
		}

		if(GUILayout.Button("Armor Upgrades"))
		{
			menuShow = false;
			armorShow = true;
		}

		if(GUILayout.Button("I'm Done Upgrading!"))
		{
			paused = false;
			menuShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void armorMenuFunc(int id)
	{
		if(GUILayout.Button("Helm")){
			armorShow = false;
			helmShow = true;
		}

		if(GUILayout.Button("Shield")){
			armorShow = false;
			shieldShow = true;
		}

		if(GUILayout.Button("Return")){
			armorShow = false;
			menuShow = true;
		}
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			armorShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}
	void helmMenuFunc(int id)
	{
		if(GUILayout.Button("Craft Helm"))
		{
			if (wrahh.LobsterParts >= 5 && wrahh.HelmOn == false)
			{
				wrahh.LobsterParts -= 5;
				helm.Protection = 1;
				helm.upgradeProtection();
				wrahh.HelmOn = true;
				wrahh.transform.FindChild("wrahh_BODY").transform.FindChild("helmet").gameObject.SetActive(true);
			}
			else
			{
				Debug.Log("Helm Already Equiped");
			}
		}

		if(GUILayout.Button("Repair Helm"))
		{
			if (wrahh.HelmArmor < wrahh.HelmMaxArmor && wrahh.LobsterParts >= 5){
				wrahh.LobsterParts -= 5;
				wrahh.HelmArmor = wrahh.HelmMaxArmor;
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
			Debug.Log("Range is now: ");
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