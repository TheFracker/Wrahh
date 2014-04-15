using UnityEngine;
using System.Collections;

public class GameCharacters : MonoBehaviour
{
	protected int health;
	protected int armor;
	protected float moveSpeed;
	protected bool facingRight;
	protected float standardGravity = 7.42f; 
	protected float MAX_MOVE_SPEED; 



	protected void die(GameObject obj)
	{
		Destroy(obj);
		Debug.Log("A gameobject was killed..");
	}

	protected void flip(GameObject obj)
	{
		//facingRight = !facingRight;
		Vector3 direction = obj.transform.localScale;
		direction.x *= -1;
		obj.transform.localScale = direction;
		Debug.Log ("FLIPPED!");
	}

	protected void setStandardPhysics (GameObject obj)
	{
		obj.rigidbody2D.gravityScale = standardGravity;
	}

	public bool isFacingRight()
	{
		return isFacingRight;
	}
}