using UnityEngine;
using System.Collections;



public class Doors : MonoBehaviour {

	public GameObject Wrahhh;

	//public Button _checkButton;

	// Use this for initialization
	void Start () {

		//button = GameObject.Find("redButton");
		//GameObject button = GameObject.Find("redButton");

		//Button _checkButton = button.GetComponent<Button>();
		//Button _checkButton = GameObject.Find("redButton").GetComponent<Button>();
		//Debug.Log(_checkButton.buttonActive);
		Debug.Log(Button.buttonActive);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == Wrahhh && Button.buttonActive == true){
			Animator anim = GetComponent<Animator>();
			anim.SetBool("open", true);
		}
	}


}
