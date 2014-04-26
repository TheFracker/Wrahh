using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GUISkin mySkin;
	bool hudOn;

	public Wrahh wrahh = new Wrahh();

	void Start () {

	}

	void FixedUpdate () {
	}

	void OnGUI()
	{
		GUI.Box (new Rect(150 , Screen.height-600, 100,25),"Health: " + wrahh.Health);
		GUI.Box (new Rect(250 , Screen.height-600, 200,25),wrahh.CurrentWeapon.getName() + " Damage: " + wrahh.CurrentWeapon.getHitDamage());
		GUI.Box (new Rect(250 , Screen.height-575, 200,25),"Durabillity: " + wrahh.CurrentWeapon.getDura() + " max: " + wrahh.CurrentWeapon.getMAXDura());
		GUI.Box (new Rect(450 , Screen.height-600, 160,25),"Helm Armor: " + wrahh.HelmArmor + " max: " + wrahh.HelmMaxArmor);
		GUI.Box (new Rect(450 , Screen.height-575, 160,25),"Shield Armor: " + wrahh.ShieldArmor + " max: " + wrahh.ShieldMaxArmor);
		GUI.Box (new Rect(900 , Screen.height-600, 100,25),"Lobsters: " + wrahh.LobsterParts);
		GUI.Box (new Rect(1000 , Screen.height-600, 150,25),"Weapon Parts: " + wrahh.WeaponParts);
	}
}
