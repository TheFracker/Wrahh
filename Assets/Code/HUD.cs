using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public Wrahh player;
	public GUISkin mySkin;
	GUIStyle HealthDisplay, StatsDisplay;

	float screenHeight = Screen.height;
	float screenWidth = Screen.width;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1){
			player = GameObject.FindWithTag("Player").gameObject.GetComponent<Wrahh>();
		}
	}

	void FixedUpdate()
	{
		if (player.Health < 0)
			Destroy(this.gameObject);
	}

	void Start()
	{
			player = GameObject.FindWithTag("Player").gameObject.GetComponent<Wrahh>();
	}

	void OnGUI()
	{
		GUI.skin = mySkin;
		HealthDisplay = mySkin.customStyles[0];
		StatsDisplay = mySkin.customStyles[1];

		GUI.Box (new Rect(screenWidth * 0.035f, screenHeight * 0.02f, 60,  50),"", HealthDisplay);
		GUI.Box (new Rect(screenWidth * 0.02f,screenHeight * 0.015f, 100, 40),"" + player.Health);
		GUI.Box (new Rect(screenWidth * 0.1f, screenHeight 	* 0.02f, 150, 40), player.CurrentWeapon.getName() + " Damage: " + player.CurrentWeapon.getHitDamage(), StatsDisplay);
		GUI.Box (new Rect(screenWidth * 0.25f, screenHeight	* 0.02f, 150, 40),"Durabillity: " + player.CurrentWeapon.getDura() + " / " + player.CurrentWeapon.getMAXDura(), StatsDisplay);
		GUI.Box (new Rect(screenWidth * 0.40f, screenHeight	* 0.02f, 150, 40),"Helm Armor: " + player.HelmArmor + " / " + player.HelmMaxArmor, StatsDisplay);
		GUI.Box (new Rect(screenWidth * 0.55f, screenHeight	* 0.02f, 150, 40),"Shield Armor: " + player.ShieldArmor + " / " + player.ShieldMaxArmor, StatsDisplay);
		GUI.Box (new Rect(screenWidth * 0.7f, screenHeight	* 0.02f, 150, 40),"Lobster Parts: " + player.LobsterParts, StatsDisplay);
		GUI.Box (new Rect(screenWidth * 0.85f, screenHeight	* 0.02f, 150, 40),"Weapon Parts: " + player.WeaponParts, StatsDisplay);
	}
}
