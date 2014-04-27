using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{

	public Wrahh wrahh; //Grants access to wrahhs getters/Setters
	private int protection = 0; //Variable which is changed when upgrading equipped shield

	void Start()
	{
		if(wrahh == null) //Function which prevents stack Overflow
			wrahh = new Wrahh(); 
	}


	public void upgradeProtection() //Functions which defines protection level of equipped shield
	{
		//If statement which runs function according to protectionLevel of shield
		if (protection == 0)
		{
			protectionLevel0();
		}
		if(protection == 1){
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

	void protectionLevel0()	//Function which is run when wrahh has no shield equipped
	{
		wrahh.ShieldOn = false;			
		wrahh.ShieldMaxArmor = 0;
		wrahh.transform.FindChild("wrahh_arm_FRONT").transform.FindChild("shield_rotation").gameObject.SetActive(false);
	}

	void protectionLevelEquip() //Function which equips shield by setting maxArmor and current armor
	{
		wrahh.ShieldMaxArmor +=5;
		wrahh.ShieldArmor += 5;
		wrahh.transform.FindChild("wrahh_arm_FRONT").transform.FindChild("shield_rotation").gameObject.SetActive(true); //Graphically shows shield on screen
	}
	void protectionLevel1()	//The Remaining Increases maxArmor and currentArmor of shield according to upgradeLevel
	{
		wrahh.ShieldMaxArmor +=3;
		wrahh.ShieldArmor += 3;
	}

	void protectionLevel2()
	{
		wrahh.ShieldMaxArmor +=4;
		wrahh.ShieldArmor += 4;
	}
	void protectionLevel3()
	{
		wrahh.ShieldMaxArmor +=4;
		wrahh.ShieldArmor += 4;
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
