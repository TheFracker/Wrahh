using UnityEngine;
using System.Collections;



public class Button : MonoBehaviour {
	
	public GameObject Wrahhh; //Crates a GameObject variable in the insspector for drag and drop (meant for player character)
	private bool triggerActive = false; //bool to check wether the trigger area have been entered 
	public static bool buttonActive = false; //bool used to check if the button is active and to send on to "Doors.cs"

	//A OnTriggerEnter, which runs when/if "other" enters the trigger area (remember to set colider to "is trigger")
	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject == Wrahhh){
		
			if(triggerActive == false){
				Debug.Log("trigger entered");
				triggerActive = true;

				Animator anim = GetComponent<Animator>();

				if(anim.GetBool("push") == false){
					anim.SetBool("push", true);
					buttonActive = true;
				}
				
				else if(anim.GetBool("push") == true){
					anim.SetBool("push", false);
					buttonActive = false;
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
		
	