using UnityEngine;
using System.Collections;

public class HitProjectile : Projectile {

	float liveTime;
	float timeToDie;

	// Use this for initialization
	void Start () {
		speed = 0;
		damage = 1;
		liveTime = 1.0f;
		timeToDie = Time.time + liveTime;
	}

	// Update is called once per frame
	void Update () {

		if(timeToDie < Time.time)
		{
			Destroy(this.gameObject);
			Debug.Log("dawdwdwadawd");
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag == "Enemy")
		{
			c.gameObject.GetComponent<Dolphin>().hurt(this);
			Destroy (this.gameObject);
		}
	}
}
