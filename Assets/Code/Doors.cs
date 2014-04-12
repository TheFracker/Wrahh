using UnityEngine;
using System.Collections;



public class Doors : MonoBehaviour {

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
			anim.SetBool("open", true);
		}
	}


}
