using UnityEngine;
using System.Collections;

public class Lobster : MonoBehaviour
{
	public float speed = 4.0f;
	public float maxDistance = 15.0f;

	Transform target;
	Transform enemyTransform;

	void Start()
	{
		enemyTransform = this.GetComponent<Transform>();
	}

	void Update ()
	{
		target = GameObject.FindWithTag("Player").transform;			//Look for target
		enemyTransform.Translate(speed*Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player")
		{
			Debug.Log("Lobster sight triggered!");
		}
	}
}
