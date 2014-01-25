using UnityEngine;
using System.Collections;

public class Guy : MonoBehaviour {

	public bool hasFlower;

	private float maxSpeed = 4;

	int groundedCount = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody2D.AddForce(new Vector2(90, 0));
			if (rigidbody2D.velocity.x > maxSpeed)
				rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
		}else if (Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody2D.AddForce(new Vector2(-90, 0));
			if (rigidbody2D.velocity.x < -maxSpeed)
				rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
		}
		if (checkGrounded() && Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 8);
		}
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

	public Vector2 getPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	public bool checkGrounded()
	{
		RaycastHit2D rh = Physics2D.Raycast(getPosition(), -Vector2.up, Mathf.Infinity);
		if (rh.rigidbody.CompareTag("floor"))
		{
			print("down");
		}
		return groundedCount > 0;
	}
}
