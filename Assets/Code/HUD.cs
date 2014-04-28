using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public Wrahh player; 							//Creates a connection to getters/setters within wrah
	public GUISkin mySkin;							
	GUIStyle HealthDisplay, StatsDisplay;

	float screenHeight = Screen.height;				//Variables used to place the HUD in the right location according to width and hight of screen	
	float screenWidth = Screen.width;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject); 		//Used to make sure the HUD will persist and remember stats of wrahh on level shift
	}

	//Created so that when wrahh dies and the game is restarted a new HUD containing the "new" Wrahhs stats will appear
	void OnLevelWasLoaded(int level) {
		if (level == 1){
			//player variable will contain the Wrahh currently located in the scene
			player = GameObject.FindWithTag("Player").gameObject.GetComponent<Wrahh>();
		}
	}

	void FixedUpdate()
	{
		//If wrahh dies the HUD will be destroyed (If retry is pressed a new will be created on level load)
		if (player.Health < 0)
			Destroy(this.gameObject);
	}

	void Start()
	{		
		//The player variable containing an instance of wrahh has the current wrahh in the scene
			player = GameObject.FindWithTag("Player").gameObject.GetComponent<Wrahh>();
	}

	void OnGUI()
	{
		//This part creates and positions the different parts of the GUI so that the player has an overview of his current stats
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
