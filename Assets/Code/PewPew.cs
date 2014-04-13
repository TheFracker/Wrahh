using UnityEngine;
using System.Collections;

public class PewPew : MonoBehaviour {

	public GameObject bullet;
	float speed = 20.0f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!true)
		{
			pew ();
		}
	}

	public void pew()
	{
		Debug.Log ("PEWPEW");
		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(speed, 0);
	}
}
