using UnityEngine;
using System.Collections;

public class Lobster : MonoBehaviour
{
	public int speed = 4;
	
	void Start ()
	{
	
	}

	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player")
		{
			Debug.Log("Lobster sight triggered!");
		}
	}
}
