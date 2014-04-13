using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	// Use this for initialization
	void Start () 
	{
		loadPrefab ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// The position of the dolphin + its weapon (needs to get tweeked a bit)
		pos = this.transform.position + new Vector3(-1.5f,0.5f,0);
	}

	protected override void loadPrefab()
	{
		// This bullet variable is inherited from Weapon
		bullet = Resources.Load ("Prefabs/Bullet") as GameObject;
	}
	
	public override void shoot()
	{
		// Shoots creates the bullets, the bullets themself give them their speed
		// There needs to be two kinds of bullets, one for shooting left and one for right
		if (Dolphin.facingRight){
			Instantiate(bullet, pos, Quaternion.identity);
		}
		else{
			Instantiate(bullet, pos, Quaternion.identity);
		}
	}
}
