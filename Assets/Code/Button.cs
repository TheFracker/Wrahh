using UnityEngine;
using System.Collections;



public class Button : MonoBehaviour {
	
	public GameObject Wrahhh;
	private bool triggerActive = false;
	Doors _door;

	// Use this for initialization
	void Start () {
		door = gameObject.GetComponentInChildren<Doors>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject == Wrahhh){

			if(triggerActive == false){
				Debug.Log("trigger entered");
				triggerActive = true;

				Animator anim = GetComponent<Animator>();

				if(anim.GetBool("push") == false){
					anim.SetBool("push", true);
					_door.buttonActive = true;
				}
				
				else if(anim.GetBool("push") == true){
					anim.SetBool("push", false);
					_door.buttonActive = false;
				}
			}

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject == Wrahhh){

			if(triggerActive == true){

				Debug.Log("trigger exited");
				triggerActive = false;
			}
		}
	}
}
		
	