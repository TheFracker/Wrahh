using UnityEngine;
using System.Collections;

public class UpgradeStationPos : MonoBehaviour {

	void Awake()
	{
		GameObject.Find ("upgradeStation").transform.position = this.transform.position;
	}
}
