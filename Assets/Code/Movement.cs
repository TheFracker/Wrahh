using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float speed = 5.0f;
	private Vector2 moveDirection = Vector2.zero;
	private CharacterController controller;
	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController>();

		if(!controller){
			controller = gameObject.AddComponent<CharacterController>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveDirection = new Vector2(Input.GetAxis("Horizontal"),0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
	}
}
