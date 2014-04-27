using UnityEngine;
using System.Collections;

public class HUDpos : MonoBehaviour {

	void Awake()
	{
		GameObject.Find ("HUD").transform.position = this.transform.position;
	}
}
