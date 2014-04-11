using UnityEngine;
using System.Collections;

public class Dolphin : MonoBehaviour {

	int health = 2;
	Weapon weapon;
	bool dead;
	public Transform sightStart, sightEnd;
	public bool spottet = false;

	public void die()
	{

		dead = true;
		Debug.Log ("Dead!");
	}

	public void useWeapon()
	{
		Debug.Log("Fire");
	}

	public void raycast()
	{

		Debug.DrawLine(sightStart.position,sightEnd.position, Color.red);
		spottet = Physics2D.Linecast(sightStart.position,sightEnd.position);
	}

	public void behaviours()
	{
	}

	public void hurt()
	{
		health--;
	}

	void FixedUpdate()
	{
		if (health <= 0 && !dead)
		{
			die();
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		behaviours();
		raycast();
	
	}
}
