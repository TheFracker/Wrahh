using UnityEngine;
using System.Collections;

public class jail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Player")
		{
			Animator anim;
			anim.GetComponent<Animator>();
			anim.SetBool("DoorHit", true);
		}
			
	}

}
