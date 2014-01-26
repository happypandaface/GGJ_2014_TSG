using UnityEngine;
using System.Collections;

public class Bunny : Dies {
	public Transform guy;
	private float jumpCount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<HeldItem>().isHeld() && GetComponent<Box>().CheckGrounded())
		{
			jumpCount += Time.deltaTime;
			if (jumpCount > 1)
			{
				jumpCount = 0;
				rigidbody2D.velocity = new Vector2(0, 5);
			}
		}
	}

	public override void Die()
	{
		guy.gameObject.GetComponent<Guy>().modKarma(-1);
		base.Die();
	}
}
