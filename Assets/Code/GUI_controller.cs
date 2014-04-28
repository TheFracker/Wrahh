using UnityEngine;
using System.Collections;

public class GUI_controller : MonoBehaviour
{
	public GameObject gameoverScreen;
	private GameObject player;
	private bool guiIsOn;

	Quaternion GUIrot = Quaternion.identity;
	Vector3 GUIpos = new Vector3(7.5f, 4.5f, -1.3f);

	void Start()
	{
		player = GameObject.Find("Player");
		guiIsOn = false;
	}

	void Update()
	{
		if (player == null && !guiIsOn)
		{
			Instantiate(gameoverScreen, GUIpos, GUIrot);
			guiIsOn = true;
		}
	}
}
