using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mover : MonoBehaviour {
	public Vector2 minVals;
	public Vector2 maxVals;
	public bool goingToMax;
	public float speed;
	public float maxSpeed;
	private Vector2 oriPos;
	private List<Collider2D> holding;
	// Use this for initialization
	void Start () {
		holding = new List<Collider2D>();
		oriPos = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		if (!GetComponent<Trigger>() || GetComponent<Trigger>().IsActive())
		{
			Vector2 dest;
			if (goingToMax)
				dest = minVals;
			else
				dest = maxVals;
			Vector2 curr = new Vector2(transform.position.x, transform.position.y);
			Vector2 diff = dest-curr;
			float speedThisFrame = Time.deltaTime*speed;
			if (diff.magnitude < speedThisFrame)
			{
				goingToMax = !goingToMax;
			}else
			{
				//rigidbody2D.velocity = diff.normalized*speedThisFrame;
				diff = diff.normalized*speedThisFrame;
				curr += diff;
				transform.position = new Vector3(curr.x, curr.y, 0);
				foreach (Collider2D col in holding)
				{

					col.transform.position = 
						new Vector3(
							col.transform.position.x + diff.x,
							col.transform.position.y + diff.y,
							col.transform.position.z);
				}
				/*
				rigidbody2D.AddForce(diff.normalized*speedThisFrame);
				if (rigidbody2D.velocity.x > maxSpeed)
					rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
				if (rigidbody2D.velocity.x < -maxSpeed)
					rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
				if (rigidbody2D.velocity.y > maxSpeed)
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeed);
				if (rigidbody2D.velocity.y < -maxSpeed)
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -maxSpeed);
				*/
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!holding.Find(comp => comp.gameObject.GetInstanceID() == col.gameObject.GetInstanceID()))
			holding.Add(col.collider);
	}
	
	void OnCollisionExit2D(Collision2D col)
	{
		for (int i = holding.Count-1; i >= 0; --i)
		{
			Collider2D check = holding[i];
			if (check.gameObject.GetInstanceID() == col.gameObject.GetInstanceID())
			{
				holding.RemoveAt(i);
			}
		}
	}
}
