using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	string name;
	int ammo;
	float reloadTime;
	bool reloading;
	int durability;
	float accidentalFire;

	// Use this for initialization
	void Start () {
	
	}

	public void hit()
	{
		Debug.Log ("Hitting");
	}

	public void shoot()
	{
		Debug.Log ("Shooting");
	}

	public void reload()
	{
		Debug.Log ("Reloading");
	}

	public bool isReloading()
	{
		if (reloading)
			return true;
		return false;
	}
}
