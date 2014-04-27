using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public GUISkin mySkin;
	GUIStyle HealthDisplay;

	bool hudOn;
	float screenHeight = Screen.height;
	float screenWidth = Screen.width;
	public Wrahh player;

	void OnGUI()
	{
		GUI.skin = mySkin;
		HealthDisplay = mySkin.customStyles[0];

		GUI.Box (new Rect(0, screenHeight - 600, 20,20 ),"", HealthDisplay);
		GUI.Box (new Rect(30, screenHeight - 600, 100,25),"" + player.Health);
		GUI.Box (new Rect(screenWidth + 250, screenHeight-600, 200,25), player.CurrentWeapon.getName() + " Damage: " + player.CurrentWeapon.getHitDamage());
		GUI.Box (new Rect(screenWidth + 250, screenHeight-575, 200,25),"Durabillity: " + player.CurrentWeapon.getDura() + " Max: " + player.CurrentWeapon.getMAXDura());
		GUI.Box (new Rect(screenWidth + 450, screenHeight-600, 160,25),"Helm Armor: " + player.HelmArmor + " Max: " + player.HelmMaxArmor);
		GUI.Box (new Rect(screenWidth + 450, screenHeight-575, 160,25),"Shield Armor: " + player.ShieldArmor + " Max: " + player.ShieldMaxArmor);
		GUI.Box (new Rect(screenWidth + 900, screenHeight-600, 100,25),"Lobsters: " + player.LobsterParts);
		GUI.Box (new Rect(screenWidth + 1000, screenHeight-600, 150,25),"Weapon Parts: " + player.WeaponParts);
	}


}
