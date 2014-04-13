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
			InvokeRepeating ("patrol", 0, 5);
		}
	
		// Update is called once per frame
		void Update ()
		{
			transform.Translate (walkAmount);
			raycast ();
			actions ();
			//patrol ();
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
			walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
			Vector3 direction = this.transform.localScale;

			if (walkingDirection < 0.0f && facingRight == true) {
				walkingDirection *= -1;
				direction.x *= -1;
				transform.localScale = direction;
				facingRight = false;
				Debug.Log ("Walking Right");
			} else {
				walkingDirection *= -1;
				direction.x *= -1;
				transform.localScale = direction;
				facingRight = true;
				Debug.Log ("Walking Left");
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
