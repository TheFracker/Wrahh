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
}
