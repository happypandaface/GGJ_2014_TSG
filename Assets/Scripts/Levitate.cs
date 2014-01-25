using UnityEngine;
using System.Collections;

public class Levitate : MonoBehaviour
{
	public float zenTime = 10f; //In seconds
	public float zenHeight = 4f;
	private float zenTimer;
	private float zenMoveSpeed = 0.01f;
	private float levitate = 0f;
	private bool doneRising = false;
	private bool isLevitating = false;

	// Use this for initialization
	void Start ()
	{
		zenTimer = zenTime;
	}

	// Update is called once per frame
	void Update ()
	{
		//Enlightenment timer count down
		zenTimer -= Time.deltaTime;
		
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
				isLevitating = true;
				doneRising = true;
				levitate += Time.deltaTime;
				transform.position = new Vector2(transform.position.x,Mathf.Sin(levitate) + zenHeight);
			}
		}
	}

	//Stop levatation and resets timer when player uses input
	public void checkLevatation()
	{
		//Reset zenTimer
		zenTimer = zenTime;
		
		if (isLevitating)
		{
			rigidbody2D.gravityScale = 1;
			isLevitating = false;
			doneRising = false;
		}
	}
}

