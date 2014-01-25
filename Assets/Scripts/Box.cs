using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour, Grounder {
	private bool isGrounded;
	private int groundedCount = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	void Update ()
	{
		if (CheckGrounded())
			rigidbody2D.drag = 2;
		else
			rigidbody2D.drag = 0;
	}

	public Vector2 GetPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	public bool CheckGrounded()
	{
		RaycastHit2D[] rhs = Physics2D.RaycastAll(GetPosition(), -Vector2.up, 1.2f);
		foreach (RaycastHit2D rh in rhs)
		{
			if (rh.collider.CompareTag("floor"))
				return groundedCount > 0;
		}
		return false;
	}
	
	void OnCollisionEnter2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
			groundedCount++;
	}
	
	void OnCollisionExit2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
			groundedCount--;
	}
}
