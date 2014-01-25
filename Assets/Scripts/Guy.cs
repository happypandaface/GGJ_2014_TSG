using UnityEngine;
using System.Collections;

public class Guy : MonoBehaviour {
	public bool isGrounded;	
	public bool hasFlower;

	//In seconds
	public float zenTime = 10f;
	public float zenHeight = 10f;
	private float zenTimer;
	private float zenMoveSpeed = 0.01f;
	private float levatate = 0f;
	private bool doneRising = false;
	private bool isLevatating = false;

	private float maxSpeed = 4;

	int groundedCount = 0;

	// Use this for initialization
	void Start () {
		zenTimer = zenTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody2D.AddForce(new Vector2(90, 0));
			if (rigidbody2D.velocity.x > maxSpeed)
				rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);

			checkLevatation();
		}else if (Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody2D.AddForce(new Vector2(-90, 0));
			if (rigidbody2D.velocity.x < -maxSpeed)
				rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);

			checkLevatation();
		}
		if (checkGrounded() && Input.GetKey(KeyCode.UpArrow))
		{
			print ("sdf");
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 8);

			checkLevatation();
		}

		//Enlightenment timer count down
		zenTimer -= Time.deltaTime;

		print (zenTimer);
		if (zenTimer < 0)
		{

			//remove gravity
			rigidbody2D.gravityScale = 0;

			if ((transform.position.y < zenHeight) && !doneRising)
			{
				transform.Translate(0, zenMoveSpeed,0);
			}
			else
			{
				isLevatating = true;
				doneRising = true;
				levatate += Time.deltaTime;
				transform.position = new Vector2(transform.position.x,Mathf.Sin(levatate) + zenHeight);
			}
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

	//Stop levatation and resets timer when player uses input
	void checkLevatation()
	{
		//Reset zenTimer
		zenTimer = zenTime;

		if (isLevatating)
		{
			rigidbody2D.gravityScale = 1;
			isLevatating = false;
			doneRising = false;
		}
	}
}
