using UnityEngine;
using System.Collections;

public class GUI_controller : MonoBehaviour
{
	public GameObject gameoverScreen;
	Quaternion GUIrot = Quaternion.identity;
	Vector3 GUIpos = new Vector3(7.5f, 4.5f, -1.3f);

	void Update()
	{
		if (Lobster.playerDead)
		{
			Instantiate(gameoverScreen, GUIpos, GUIrot);
			Lobster.playerDead = false;
		}
			
	}
}
