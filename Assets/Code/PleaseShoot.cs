using UnityEngine;
using System.Collections;

public class PleaseShoot : MonoBehaviour {

	PewPew pewpew;

	// Use this for initialization
	void Start () {
		pewpew = gameObject.AddComponent<PewPew> ();
	}
	
	// Update is called once per frame
	void Update () {
		pewpew.pew ();
	}
}
