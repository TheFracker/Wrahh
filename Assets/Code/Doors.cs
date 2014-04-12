using UnityEngine;
using System.Collections;



public class Doors : MonoBehaviour {

	public GameObject Wrahhh;
	// Use this for initialization
	void Start () {

		Debug.Log("I see this");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == Wrahhh){
			Debug.Log("im in collision");
			Animator anim = GetComponent<Animator>();
			anim.SetBool("open", true);
			Debug.Log("im on the other side");
		}
	}


}
