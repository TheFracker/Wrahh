using UnityEngine;
using System.Collections;

public class MonkeyBars : MonoBehaviour {


	private bool triggerActive = false;
	public static bool onMonkeyBar;
	private Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.W) && triggerActive == true){
			onMonkeyBar = true;
		}

		if(Input.GetKeyDown(KeyCode.S)){
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
