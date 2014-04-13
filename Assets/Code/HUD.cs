using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GUISkin mySkin;
	public GUIStyle hudStyle;
	bool hudOn;
	Wrahh wrahh;
	

	void Start () {
		wrahh = gameObject.AddComponent<Wrahh>();
	}

	void Update () {
	
	}

	void OnGUI()
	{
		GUI.Box (new Rect(150 , Screen.height-600, 100,20),"Health: " + wrahh.getHealth());
		GUI.Box (new Rect(250 , Screen.height-600, 100,20),"Armor: " + wrahh.getArmor());
	}
}
