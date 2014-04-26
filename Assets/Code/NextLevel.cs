using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Application.LoadLevel (Application.loadedLevel + 1);
			Button.buttonActive = false;
		}

	}
}
