using UnityEngine;
using System.Collections;

public class Pistol : Weapon {


	public Rigidbody2D bullet;
	float speed = 20.0f;
	Transform trans;
	bool t;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{	
		//shoot ();
		t = Dolphin.facingRight;
		fireLeft ();
	}
	
	public override void shoot()
	{
		Debug.Log (12345678);
		if (!t){
			Debug.Log ("bullet");
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
		}
		else{
			fireLeft();
			//Debug.Log ("bulletFalse");
			//Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody2D;
			//bulletInstance.velocity = new Vector2(-speed, 0);
			//Debug.Log (122);
		}
	}
	
	void fireRight()
	{
		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(speed, 0);
	}

	void fireLeft()
	{
		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(-speed, 0);
	}
}
