using UnityEngine;
using System.Collections;

public class Guy : MonoBehaviour {

	float groundedCount = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody2D.velocity = new Vector2(7, rigidbody2D.velocity.y);
		}else if (Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody2D.velocity = new Vector2(-7, rigidbody2D.velocity.y);
		}
		if (groundedCount > 0 && Input.GetKey(KeyCode.UpArrow))
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
}
