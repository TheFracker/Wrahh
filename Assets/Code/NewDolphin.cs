using UnityEngine;
using System.Collections;

public class NewDolphin : MonoBehaviour {

	int heatlh;
	float moveSpeed;
	Weapon weapon;

	public float speed = 6.0f;
	public float maxDistance = 20.0f;
	
	private Transform target;
	private Transform enemyTransform;
	
	private bool playerDead = false;
	public bool facingRight;
	
	private Vector3 startPos;
	private Vector3 dir;
	private Vector3 currentDir;

	// Use this for initialization
	void Start () {
		heatlh = 3;
		moveSpeed = 5;
		weapon = gameObject.AddComponent<Weapon> ();
		enemyTransform = this.GetComponent<Transform>();
		facingRight = false;
		startPos = enemyTransform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		//Assign the target to be the whatever object with the tag; "Player"
		target = GameObject.FindWithTag("Player").transform;
		currentDir = enemyTransform.position;
		
		// If I (the dolphin) can see the escapen prisonar (Wrahh) and not too far away, I will start chaing him.
		if ((Vector3.Distance(target.position, enemyTransform.position) < maxDistance && enemyTransform.position.x > target.position.x &! facingRight)
		    || (Vector3.Distance(target.position, enemyTransform.position) < maxDistance && enemyTransform.position.x < target.position.x && facingRight))
			chaseTarget();
		else
			returnToStartPos();
		// If close enough I will shoot at him
	}
	
	void hurt()
	{
		int damageTaken = 0;
		heatlh -= damageTaken;
		if(heatlh <= 0)
			die ();
	}

	void die()
	{

	}

	void useWeapon(Weapon w)
	{
		w.shoot ();
	}

	void AI()
	{

	}

	void guard()
	{
		// Make the guard walk left and right
		// start walking left



	}

	void chaseTarget()
	{
		//The enemy's diretion
		dir = target.position - enemyTransform.position;
		dir.Normalize(); //Normalize the direction vector
		enemyTransform.position += dir * speed * Time.deltaTime; //CHASE THE PLAYER!!!!!
	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 direction = transform.localScale;
		direction.x *= -1;
		transform.localScale = direction;
	}

	void returnToStartPos()
	{
		if(enemyTransform.position.x < startPos.x &! facingRight) //Must face starting position
		{
			flip ();
			enemyTransform.position += enemyTransform.right * speed * Time.deltaTime;
		}
		Debug.Log("The enemy returned");			//Just to make sure it works so far..
	}
}
