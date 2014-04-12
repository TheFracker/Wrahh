using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	public Rigidbody2D bullet;
	float speed = 20f;
	Dolphin dolphin;
	bool facing;
	void Awake()
	{
		dolphin = GetComponent<Dolphin>();
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{	
		facing = dolphin.facingRight;
		shoot ();
	}
	public override void shoot()
	{
		if (facing == true){
		Debug.Log ("bullet");
		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(speed, 0);
		}
		else{
			Debug.Log ("bulletFalse");
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-speed, 0);
		}
	}
}
