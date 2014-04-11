using UnityEngine;
using System.Collections;

public class Dolphin : MonoBehaviour
{

		int health = 2;
		Weapon weapon;
		bool dead;
		public Transform sightStart, sightEnd;
		public bool spotted = false;
		public bool facingRight = true;
	
		// Use this for initialization
		void Start ()
		{
				InvokeRepeating ("patrol", 0f, Random.Range (2f, 4f));
		}
	
		// Update is called once per frame
		void Update ()
		{
				raycast ();
				actions ();
		}

		public void die ()
		{

				dead = true;
				Debug.Log ("Dead!");
		}

		public void useWeapon ()
		{
				Debug.Log ("Fire");
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
						useWeapon ();
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
