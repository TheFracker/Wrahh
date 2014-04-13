using UnityEngine;
using System.Collections;

public class MonkeyBars : MonoBehaviour {


	private bool triggerActive = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player" && triggerActive == false){
			triggerActive = true;
			Debug.Log("Trigger is active");
			//set this in wrahh:::: anim.SetBool("crawling", true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		
		if(other.tag == "Player" && triggerActive == true){
			triggerActive = false;
			Debug.Log("Trigger is not active");
		}
	}


}
