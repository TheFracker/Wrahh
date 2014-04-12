using UnityEngine;
using System.Collections;

public class Dolphin : MonoBehaviour
{
		public float walkSpeed = 2.0f;
		public float walkLeft = 0.0f;
		public float walkRight = 2.0f;
		float walkingDirection = 1.0f;
		Vector3 walkAmount;
		
		int health = 2;
		Weapon weapon;
		bool dead;
		public Transform sightStart, sightEnd;
		public bool spotted = false;
		public bool facingRight = true;
	
		// Use this for initialization
		void Start ()
		{
				weapon = new Pistol();
				InvokeRepeating ("patrol", 0f, Random.Range (2f, 4f));
		}
	
		// Update is called once per frame
		void Update ()
		{
				walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
		
				if (walkingDirection > 0.0f && transform.position.x >= walkRight) {
						walkingDirection = -1.0f;
				} else if (walkingDirection < 0.0f && transform.position.x <= walkLeft) {
						walkingDirection = 1.0f;
				}
		
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
				facingRight = !facingRight;

				if (facingRight == true) {
						transform.eulerAngles = new Vector2 (0, 0);
				} else {
						transform.eulerAngles = new Vector2 (0, 180);
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
