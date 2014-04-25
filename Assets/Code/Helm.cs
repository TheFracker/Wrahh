using UnityEngine;
using System.Collections;

public class Helm : MonoBehaviour
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

	void protectionLevel1()
	{
		wrahh.HelmMaxArmor += 2;
		wrahh.HelmArmor += 2;
		Debug.Log("Im here, Protection level 1");
	}
	void protectionLevel2()
	{
		wrahh.HelmMaxArmor += 3;
		wrahh.HelmArmor += 3;
		Debug.Log("Im here, Protection level 2");
	}
	
	void protectionLevel3()
	{
		wrahh.HelmMaxArmor += 4;
		wrahh.HelmArmor += 4;
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
