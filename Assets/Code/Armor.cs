using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{
	private int armorDurability;
	public int upgradeLevel;

	void Update()
	{
		if (gameObject.tag == "Shield" && upgradeLevel == 0)
			Debug.Log ("Shield is at default level!");
		else if (gameObject.tag == "Shield" && upgradeLevel == 1)
			Debug.Log("Shield is level 1!");
		else if (gameObject.tag == "Shield" && upgradeLevel == 2)
			Debug.Log ("Shield is level 2!");
		else if (gameObject.tag == "Shield" && upgradeLevel == 3)
			Debug.Log ("Shield is level 3!");
	}
}
