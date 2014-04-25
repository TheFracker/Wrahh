using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public Wrahh wrahh = new Wrahh();
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

	void protectionLevel1()
	{
		wrahh.ShieldMaxArmor +=2;
		wrahh.ShieldArmor += 2;
		Debug.Log("Im here, Protection level 1");
	}
	void protectionLevel2()
	{
		wrahh.ShieldMaxArmor +=3;
		wrahh.ShieldArmor += 3;
		Debug.Log("Im here, Protection level 2");
	}

	void protectionLevel3()
	{
		wrahh.ShieldMaxArmor +=4;
		wrahh.ShieldArmor += 4;
		Debug.Log("Im here, Protection level 3");
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
