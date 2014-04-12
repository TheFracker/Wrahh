using UnityEngine;
using System.Collections;



public class Doors : MonoBehaviour {

	public GameObject Wrahhh;
	public bool buttonActive;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(buttonActive);
		if(other.gameObject == Wrahhh && buttonActive == true){
			Animator anim = GetComponent<Animator>();
			anim.SetBool("open", true);
		}
	}


}
