﻿using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{
	private int durabilityLevel;
	private int protectionLevel;
	bool shieldOn;

	void Start(){
		basicStats();
	}
	public void basicStats(){

		durabilityLevel = 0;
		protectionLevel = 0;
		shieldOn = true;

	}
	void Update()
	{
		if (gameObject.tag == "Shield" && protectionLevel == 0)
			Debug.Log ("Shield is at default level!");
		else if (gameObject.tag == "Shield" && protectionLevel == 1)
			Debug.Log("Shield is level 1!");
		else if (gameObject.tag == "Shield" && protectionLevel == 2)
			Debug.Log ("Shield is level 2!");
		else if (gameObject.tag == "Shield" && protectionLevel == 3)
			Debug.Log ("Shield is level 3!");
	}
	public int DurabillityLevel
	{
		get
		{
			return durabilityLevel;
		}
		set
		{
			durabilityLevel = value;
		}
	}

	public int ProtectionLevel
	{
		get
		{
			return protectionLevel;
		}
		set
		{
			protectionLevel = value;
		}
	}

	public bool ShieldOn
	{
		get
		{
			return shieldOn;
		}
		set
		{
			shieldOn = value;
		}
	}

}
