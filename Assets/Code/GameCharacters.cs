using UnityEngine;
using System.Collections;

public class GameCharacters : MonoBehaviour
{
	protected int health;
	protected int armor;
	protected float moveSpeed;
	protected bool facingRight;

	public static void die()
	{
		Debug.Log("A gameobject was killed..");
	}

	public static void flip(GameObject obj)
	{
		//isFacingRight = !isFacingRight;
		Vector3 direction = obj.transform.localScale;
		direction.x *= -1;
		obj.transform.localScale = direction;
		Debug.Log ("FLIPPED!");
	}
}
