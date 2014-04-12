using UnityEngine;
using System.Collections;

public class Lobster : MonoBehaviour
{
	public float speed = 4.0f;
	public float maxDist = 15.0f;

	Transform target;
	Transform enemyTransform;
	
	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();
	}

	void Update ()
	{
		target = GameObject.FindWithTag("Player").transform;			//Look for target

		if (Vector3.Distance(target.position, enemyTransform.position) < maxDist)
		{
			Vector3 dir = target.position - enemyTransform.position;
			dir.Normalize();
			enemyTransform.position += dir * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player")
		{
			Debug.Log("Lobster touched Wrahh!");

			enemyTransform.position += 0 * speed * Time.deltaTime

			//---
			//	Wrahh stops moving
			//	Lobster stops moving
			//	Wrahh should die
			//	Wrahh should respwan
			//---
			Debug.Log("Game Over!");
		}
	}
}
