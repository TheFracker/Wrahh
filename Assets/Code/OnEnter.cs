using UnityEngine;
using System.Collections;

public class OnEnter : MonoBehaviour {

	void Awake()
	{
		GameObject.FindWithTag ("Player").transform.position = this.gameObject.transform.position;
	}
}
