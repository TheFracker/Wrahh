using UnityEngine;
using System.Collections;



public class Button : MonoBehaviour {
	
	public GameObject Wrahhh;
	private bool triggerActive = false;

	// Use this for initialization
	void Start () {
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
					transform.Find("doorRightTrigger").GetComponent<Doors>().buttonActive = true;
				}
				
				else if(anim.GetBool("push") == true){
					anim.SetBool("push", false);
					transform.Find("doorRightTrigger").GetComponent<Doors>().buttonActive = false;
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
		
	