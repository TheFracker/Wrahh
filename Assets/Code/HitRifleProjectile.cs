using UnityEngine;
using System.Collections;

public class HitRifleProjectile : HitProjectile {
	
	// This projectile will not move, so its speed is set to 0
	// It will die after a small amout of time (liveTime) if it does not hit an enemy
	void Awake () {
		speed = 0;
		damage = 3;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
		criticalChange = 5;
	}
}
