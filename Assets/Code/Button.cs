using UnityEngine;
using System.Collections;



public class Button : MonoBehaviour {
	
	public GameObject Wrahhh;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == Wrahhh){
			Animator anim = GetComponent<Animator>();
			Debug.Log("im in trigger");
			//if(anim.GetBool("push") == false){
				//anim.SetBool("push", true);
				//Debug.Log("it is now activated");
			//}

			//else(anim.GetBool("push") == true){
			  // anim.SetBool("push", false);
				//Debug.Log("it is now deactivated");
			}

		}
	}
	
	
}