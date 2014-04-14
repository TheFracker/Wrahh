using UnityEngine;
using System.Collections;

public class HitProjectile : Projectile {

	float liveTime;
	float timeToDie;
	bool start;

	// Use this for initialization
	void Start () {
		speed = 0;
		damage = 1;
		liveTime = 1.0f;
		start = false;
	}

	// Update is called once per frame
	void Update () {
		if (!start)
		{
			timeToDie = Time.time + liveTime;
			start = true;
		}

		Debug.Log (Time.time - timeToDie);

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
