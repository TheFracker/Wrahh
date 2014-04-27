using UnityEngine;
using System.Collections;

public class OnEnter : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		this.transform.position = GameObject.Find ("doorLeft").transform.position + new Vector3 (1, -0.4f, 0);
		Vector3 pos = this.gameObject.transform.position;
		GameObject.FindWithTag ("Player").transform.position = pos;
	}
}
