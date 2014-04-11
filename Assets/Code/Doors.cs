using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


}


	void OnCollisionEnter(Collision collision){
		animation.Play("lvl_object_openDoor");
	}


}
