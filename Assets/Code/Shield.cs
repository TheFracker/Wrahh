using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public Wrahh wrahh = new Wrahh();
	private int durabillity = 0;
	private int protection = 0;

	void Start()
	{
	}


	public void upgradeProtection()
	{
		if(protection == 1){
			protectionLevel1();
		}
		else if (protection == 2){
			protectionLevel2();
		}
		else if (protection == 3){
			protectionLevel3();
		}

	}

	public void upgradeDurabillity()
	{
		if(durabillity == 1){
			durabillityLevel1();
		}
		else if (durabillity == 2){
			durabillityLevel2();
		}
		else if (durabillity == 3){
			durabillityLevel3();
		}
	}

	void protectionLevel1()
	{
		wrahh.Armor += 2;
		Debug.Log("Im here, Protection level 1");
	}
	void protectionLevel2()
	{
		wrahh.Armor += 3;
		Debug.Log("Im here, Protection level 2");
	}

	void protectionLevel3()
	{
		wrahh.Armor += 5;
		Debug.Log("Im here, Protection level 2");
	}

	void durabillityLevel1()
	{
		wrahh.ShieldDura += 5;
		Debug.Log("Im here, Durabillity level 1");
	}
	void durabillityLevel2()
	{
		wrahh.ShieldDura += 5;
		Debug.Log("Im here, Durabillity level 2");
	}
	
	void durabillityLevel3()
	{
		wrahh.ShieldDura += 5;
		Debug.Log("Im here, Durabillity level 2");
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
