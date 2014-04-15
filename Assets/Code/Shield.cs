using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public Wrahh wrahh = new Wrahh();
	private int durabillity = 0;
	private int protection = 0;

	void Start()
	{
		wrahh.Armor = 3;
	}

	void FixedUpdate()
	{
		if (protection == 1){
			protectionLevel1();
		}
		else if (protection == 2){
			protectionLevel2();
		}
	}

	void protectionLevel1()
	{

		Debug.Log("Im here, Protection level 1");
	}

	void protectionLevel2()
	{
		wrahh.Armor = 5;
		Debug.Log(wrahh.Armor);
		Debug.Log("Im here, Protection level 2");
	}
	public void something()
	{
		/*
		if (protectionLevel == 1){
			wrahh.Armor = 3;
			Debug.Log("I'm Here");
		}
		else if (protectionLevel == 2){
			wrahh.Armor = 6;
			Debug.Log(wrahh.Armor);
		}
		else if (protectionLevel == 3){
			Debug.Log ("Shield is level 2!");
		}
		else if (protectionLevel == 4){
			Debug.Log ("Shield is level 3!");
		}
		
		if (durabilityLevel == 0){
			Debug.Log ("Shield is at default level!");
		}
		else if (durabilityLevel == 1){
		}
		else if (durabilityLevel == 2){
			Debug.Log ("Shield is level 2!");
		}
		else if (durabilityLevel == 3){
			Debug.Log ("Shield is level 3!");
		}*/
	}

	public int Durabillity
	{
		get
		{
			return durabillity;
		}
		set
		{
			durabillity = value;
		}
	}

	public int Protection
	{
		get
		{
			return protection;
		}
		set
		{
			protection = value;
		}
	}

}
