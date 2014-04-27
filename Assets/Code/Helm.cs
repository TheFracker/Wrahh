using UnityEngine;
using System.Collections;

public class Helm : MonoBehaviour
{
	public Wrahh wrahh;
	private int protection = 0;
	
	void Start()
	{
		if(wrahh == null)
			wrahh = new Wrahh();
	}
	
	// Is called whenever the helmet is upgraded in the upgrade station
	// protection level 0 is set when he loses his helmet
	public void upgradeProtection()
	{
		if(protection == 0){
			protectionLevel0();
		}
		else if (protection == 1){
			protectionLevelEquip();
		}
		else if (protection == 2){
			protectionLevel1();
		}
		else if (protection == 3){
			protectionLevel2();
		}
		else if (protection == 4){
			protectionLevel3();
		}
		
	}

	// The different stages of protection that the helmet can offer.
	void protectionLevel0()
	{
		wrahh.HelmOn = false;
		wrahh.HelmMaxArmor = 0;
		wrahh.transform.FindChild("wrahh_BODY").transform.FindChild("helmet").gameObject.SetActive(false);
		Debug.Log("Im here, Protection level 0");
	}
	void protectionLevelEquip()
	{
		wrahh.HelmMaxArmor += 5;
		wrahh.HelmArmor += 5;
		wrahh.transform.FindChild("wrahh_BODY").transform.FindChild("helmet").gameObject.SetActive(true);
		Debug.Log("Helm Equiped");
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

	// Getter and setter
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
