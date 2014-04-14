using UnityEngine;
using System.Collections;

public class UpgradeStation : MonoBehaviour {

	public GUISkin mySkin;
	Rect window300X300, statsWindow;
	bool menuShow = false;
	bool statsShow = false;
	bool armorShow = false, helmShow = false, shieldShow = false;
	bool weaponShow = false, pistolShow = false, rifleShow = false;

	Wrahh wrahh = new Wrahh();
	Armor armor = new Armor();

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
		if(pistolShow){
			window300X300 = GUI.Window(6, window300X300, pistolMenuFunc, "Pistol Upgrades");
		}
		if(rifleShow){
			window300X300 = GUI.Window(7, window300X300, rifleMenuFunc, "Rifle Upgrades");
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
		if(GUILayout.Button("Upgrade Protection")){

			Debug.Log("Helm protection is now: ");
		}
		
		if(GUILayout.Button("Upgrade Durabillity")){
			Debug.Log("Helm Durabillity is now: ");
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
		if(GUILayout.Button("Upgrade Protection")){

			if (armor.ProtectionLevel == 0 && armor.ShieldOn && wrahh.LobsterParts >= 5)
			{
				wrahh.LobsterParts -= 5;

			}
			else
				Debug.Log ("Cannot Upgrade");*/
			Debug.Log (armor.ProtectionLevel);
		}
		
		if(GUILayout.Button("Upgrade Durabillity")){
			Debug.Log("Shield Durabillity is now: ");
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
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void pistolMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade damage")){
			Debug.Log("Pistol damage is now: ");
		}
		
		if(GUILayout.Button("Upgrade range")){
			Debug.Log("Pistol range is now: ");
		}

		if(GUILayout.Button("Upgrade accidental trigger")){
			Debug.Log("Pistols chance to accidental trigger: ");
		}
		
		if(GUILayout.Button("Return")){
			pistolShow = false;
			weaponShow = true;
		}
		
		if(GUILayout.Button("I'm Done Upgrading!!"))
		{
			paused = false;
			pistolShow = false;
			statsShow = false;
			Time.timeScale = 1;
		}
	}

	void rifleMenuFunc(int id)
	{
		if(GUILayout.Button("Upgrade damage")){
			Debug.Log("Rifle damage is now: ");
		}
		
		if(GUILayout.Button("Upgrade range")){
			Debug.Log("Rifle range is now: ");
		}

		if(GUILayout.Button("Upgrade Accidental Trigger")){
			Debug.Log("Rifle chance to trigger is now: ");
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