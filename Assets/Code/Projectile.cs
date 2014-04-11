using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int damage;
	float speed;

	// Use this for initialization
	void Start () {
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int giveDamage()
	{
		return damage;
	}
}
