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
		GUI.Box (new Rect(200 , Screen.height-600, 100,25),"Health: " + wrahh.Health);
		GUI.Box (new Rect(300 , Screen.height-600, 100,25),"Damage: ");
		GUI.Box (new Rect(400 , Screen.height-600, 160,25),"Helm Armor: " + wrahh.HelmArmor + " max: " + wrahh.HelmMaxArmor);
		GUI.Box (new Rect(400 , Screen.height-575, 160,25),"Shield Armor: " + wrahh.ShieldArmor + " max: " + wrahh.ShieldMaxArmor);
		GUI.Box (new Rect(900 , Screen.height-600, 100,25),"Lobsters: " + wrahh.LobsterParts);
		GUI.Box (new Rect(1000 , Screen.height-600, 100,25),"Rifles: " + wrahh.RiflesCollected);
		GUI.Box (new Rect(1100 , Screen.height-600, 100,25),"Guns: " + wrahh.GunsCollected);
	}
}
