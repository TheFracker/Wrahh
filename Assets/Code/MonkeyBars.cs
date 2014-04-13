using UnityEngine;
using System.Collections;

public class MonkeyBars : MonoBehaviour {
	
	private bool triggerActive = false; //bool to check wether the trigger area have been entered 
	public static bool onMonkeyBar; //bool used to check if the button is active and to send on to "Wrahh.cs"
	
	// Update is called once per frame
	void Update () {
	
		// If "W" is pressed and the trigger is active/the player is in the trigger zone
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && triggerActive == true){
			onMonkeyBar = true; //bolean set to true and used in (Wrahh.cs)
		}


		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			onMonkeyBar = false;
		}


	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player" && triggerActive == false){
			triggerActive = true;
			Debug.Log("Trigger is active");
		}

	}

	void OnTriggerExit2D(Collider2D other){
		
		if(other.tag == "Player" && triggerActive == true){
			triggerActive = false;
			Debug.Log("Trigger is not active");
			onMonkeyBar = false;
		}
	}


}
