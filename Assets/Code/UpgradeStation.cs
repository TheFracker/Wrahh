using UnityEngine;
using System.Collections;

public class UpgradeStation : MonoBehaviour {

	public GUISkin mySkin;
	Rect window300X300;
	bool menuShow = false;
	bool armorShow = false, helmShow = false, shieldShow = false;
	bool weaponShow = false, pistolShow = false, rifleShow = false;

	int lobsterParts = 10;
	int guns = 10;
	int Rifles = 10;

	int helmDurabillityLevel = 0; 
	int shieldDurabillityLevel = 0;

	int helmProtectionLevel = 0; 
	int shieldProtectionLevel = 0;

	int pistolDamageLevel = 0; 
	int rifleDamageLevel = 0;

	int pistolDurabillityLevel = 0; 
	int rifleDurabillityLevel = 0;

	float rifleAccidentalTriggerLevel = 0f; 
	float pistolAccidentalTriggerLevel = 0f;
		
	int repairPrice;

	bool paused = false, playerEnter = true;

	void Start()
	{
		window300X300 = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 300,300);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		paused = true;

	if (paused)
		if (other.tag == "Player"&&playerEnter){
			menuShow = true;
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
			window300X300 = GUI.Window(2, window300X300, weaponMenuFunc, "Weapon Upgrades");
		}
		if(pistolShow){
			window300X300 = GUI.Window(2, window300X300, pistolMenuFunc, "Pistol Upgrades");
		}
		if(rifleShow){
			window300X300 = GUI.Window(2, window300X300, rifleMenuFunc, "Rifle Upgrades");
		}


	}
	
	void mainWindowFunc(int id)
	{
		if(GUILayout.Button("Repair"))
		{
				
		}
		if(GUILayout.Button("Weapon Upgrades"))
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
			Time.timeScale = 1;
		}
	}
	void helmMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade Protection")){
			helmProtection += 5;
			Debug.Log("Helm protection is now: " + helmProtection);
		}
		
		if(GUILayout.Button("Upgrade Durabillity")){
			helmDurabillity += 5;
			Debug.Log("Helm Durabillity is now: " + helmDurabillity);
		}
		
		if(GUILayout.Button("Return")){
			helmShow = false;
			armorShow = true;
		}
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			helmShow = false;
			Time.timeScale = 1;
		}
	}
	
	void shieldMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade Protection")){
			shieldProtection += 5;
			Debug.Log("Shield protection is now: " + shieldProtection);
		}
		
		if(GUILayout.Button("Upgrade Durabillity")){
			shieldDurabillity += 5;
			Debug.Log("Shield Durabillity is now: " + shieldDurabillity);
		}
		
		if(GUILayout.Button("Return")){
			shieldShow = false;
			armorShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			shieldShow = false;
			Time.timeScale = 1;
		}
	}

	void weaponMenuFunc(int id)
	{
		if(GUILayout.Button("Pistol")){
			weaponShow = false;
			pistolShow = true;
		}
		
		if(GUILayout.Button("Rifle")){
			weaponShow = false;
			rifleShow = true;
		}

		if(GUILayout.Button("Return")){
			weaponShow = false;
			menuShow = true;
		}
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			weaponShow = false;
			Time.timeScale = 1;
		}
	}

	void pistolMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade damage")){
			Debug.Log("Pistol damage is now: " + pistolDamage);
		}
		
		if(GUILayout.Button("Upgrade range")){
			Debug.Log("Pistol range is now: ");
		}

		if(GUILayout.Button("Upgrade accidental trigger")){
			Debug.Log("Pistols chance to accidental trigger: " + pistolAccidentalTrigger);
		}
		
		if(GUILayout.Button("Return")){
			pistolShow = false;
			weaponShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			pistolShow = false;
			Time.timeScale = 1;
		}
	}

	void rifleMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade damage")){
			Debug.Log("Rifle damage is now: " + rifleDamage);
		}
		
		if(GUILayout.Button("Upgrade range")){
			Debug.Log("Rifle range is now: ");
		}

		if(GUILayout.Button("Upgrade Accidental Trigger")){
			Debug.Log("Rifle chance to trigger is now: " + rifleAccidentalTrigger);
		}
		
		if(GUILayout.Button("Return")){
			rifleShow = false;
			weaponShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			rifleShow = false;
			Time.timeScale = 1;
		}
	}

}