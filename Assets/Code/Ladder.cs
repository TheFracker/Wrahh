using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	bool canClimb = false;
	float yPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(canClimb);
	}
	void OnTriggerStay2D (Collider2D other)
	{
		canClimb = true;
		if (Input.GetAxis("Vertical") > 0 && canClimb == true){
			other.rigidbody2D.velocity = new Vector2(0,3f);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		canClimb = false;
	}
}
