using UnityEngine;
using System.Collections;

public class GUI_controller : MonoBehaviour
{
	void FixedUpdate()
	{
		if (GameCharacters.playerDead)
			Debug.Log("HE IS DEEEAAADDDDD!!!");

	}
}
