using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public GUISkin mySkin;
	bool hudOn;
	float screenHeight = Screen.height;
	float screenWidth = Screen.width;
	public Wrahh player;

	void OnGUI()
	{
		GUI.Box (new Rect(150  , screenHeight-600, 100,25),"Health: " + player.Health);
		GUI.Box (new Rect(250  , screenHeight-600, 200,25), player.CurrentWeapon.getName() + " Damage: " + player.CurrentWeapon.getHitDamage());
		GUI.Box (new Rect(250  , screenHeight-575, 200,25),"Durabillity: " + player.CurrentWeapon.getDura() + " Max: " + player.CurrentWeapon.getMAXDura());
		GUI.Box (new Rect(450  , screenHeight-600, 160,25),"Helm Armor: " + player.HelmArmor + " Max: " + player.HelmMaxArmor);
		GUI.Box (new Rect(450  , screenHeight-575, 160,25),"Shield Armor: " + player.ShieldArmor + " Max: " + player.ShieldMaxArmor);
		GUI.Box (new Rect(900  , screenHeight-600, 100,25),"Lobsters: " + player.LobsterParts);
		GUI.Box (new Rect(1000 , screenHeight-600, 150,25),"Weapon Parts: " + player.WeaponParts);
	}


}
