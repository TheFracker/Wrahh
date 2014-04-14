using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public Wrahh wrahh = new Wrahh();
	private int durabilityLevel;
	private int protectionLevel;

	void Start()
	{
		if (protectionLevel == 1){
			Debug.Log("I'm Here");
		}
		else if (protectionLevel == 2){
			wrahh.Armor = 5;
			Debug.Log(wrahh.Armor);
			Debug.Log("Shield is level 1!dawdadawdadawdawdawdw");
		}
		else if (protectionLevel == 3){
			Debug.Log ("Shield is level 2!");
		}
		else if (protectionLevel == 4){
			Debug.Log ("Shield is level 3!");
		}
	}

	void Update()
	{

	}

	public void something()
	{

		
		/*if (durabilityLevel == 0){
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

}
