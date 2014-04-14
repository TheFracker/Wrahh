using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GUISkin mySkin;
	bool hudOn;

	Wrahh wrahh = new Wrahh();

	void Start () {

	}

	void Update () {
	
	}

	void OnGUI()
	{
		GUI.Box (new Rect(225 , Screen.height-600, 100,20),"Health: " + wrahh.getHealth());
		GUI.Box (new Rect(425 , Screen.height-600, 100,20),"Armor: " + wrahh.getArmor());
		GUI.Box (new Rect(625 , Screen.height-600, 100,20),"Lobster Parts: " + wrahh.getLobsterParts());
		GUI.Box (new Rect(825 , Screen.height-600, 100,20),"Rifles: " + wrahh.getRiflesCollected());
		GUI.Box (new Rect(1025 , Screen.height-600, 100,20),"Guns: " + wrahh.getGunsCollected());
	}
}
