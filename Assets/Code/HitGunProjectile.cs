using UnityEngine;
using System.Collections;

public class HitGunProjectile : HitProjectile {

	// Use this for initialization
	void Start () {
		speed = 0;
		damage = 2;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
		criticalChange = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToDie < Time.time)
		{
			Destroy(this.gameObject);
		}
	}
}
