using UnityEngine;
using System.Collections;

public class Wrahh : MonoBehaviour {

	int health;
	int armor;
	Weapon[] weapons;
	Weapon currentWeapon;
	int grenades;

	// Use this for initialization
	void Start () {
		health = 3; // Three hearts
		armor = 0; // No armor to start with
		grenades = 0; // Haven't found any grenades yet
	}
	
	// Update is called once per frame
	void Update () {
		// movement in here
	}

	public void useWeapon(Weapon currentWeapon)
	{
		Debug.Log ("Hitting with this weird club");
	}

	public void throwGrenade()
	{
		if (grenades > 0) {
			Debug.Log("Throwing grenade");
			grenades--;
		}
	}

	public void die()
	{
		Debug.Log ("Dying");
	}

	public void OnCollisionEnter(Collider2D c)
	{
		// Pick up weapon
		if (c.tag == "Weapon")
			// picks up weapon
			Debug.Log ("Picking up this thing");
		// Pick up armor
		if (c.tag == "Armor")
			// picks up armor
			Debug.Log ("This can protect me");
	}

}
