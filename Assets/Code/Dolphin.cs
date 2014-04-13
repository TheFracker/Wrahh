using UnityEngine;
using System.Collections;

public class Dolphin : MonoBehaviour
{
		public float walkSpeed = 2.0f;
		public float walkRight = 0.0f;
		public float walkLeft = 1.0f;
		float walkingDirection = -1.0f;
		Vector3 walkAmount;
		int health = 2;
		Weapon weapon;
		bool dead;
		public Transform sightStart, sightEnd;
		public bool spotted = false;
		public static bool facingRight = true;
	
		// Use this for initialization
		void Start ()
		{
				weapon = gameObject.AddComponent<Pistol>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			transform.Translate (walkAmount);
			raycast ();
			actions ();

		}

		public void die ()
		{

				dead = true;
				Debug.Log ("Dead!");
		}

		public void useWeapon (Weapon weapon)
		{
				weapon.shoot ();
		}

		public void raycast ()
		{

				Debug.DrawLine (sightStart.position, sightEnd.position, Color.red);
				spotted = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Wrahh"));
		}

		public void patrol ()
		{ 
			//walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;	

		if (walkingDirection > 0.0f && transform.position.x >= walkLeft && facingRight == true) {
						walkingDirection = -1.0f;
						transform.eulerAngles = new Vector2 (0, 0);
						facingRight = false;
				} else {
						walkingDirection = 1.0f;
						transform.eulerAngles = new Vector2 (0, 180);
						facingRight=true;
		}

						
	
		}

		public void actions ()
		{
				if (spotted) {
						useWeapon (weapon);
				}
		}

		public void hurt ()
		{
				health--;
		}

		void FixedUpdate ()
		{
				if (health <= 0 && !dead) {
						die ();
				}

		}

}
