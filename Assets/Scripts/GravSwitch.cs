using UnityEngine;
using System.Collections;

public class GravSwitch : Switch {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (trigger.GetComponent<Rigidbody2D>().gravityScale == 0)
			trigger.GetComponent<Rigidbody2D>().gravityScale = 1;
		else
			trigger.GetComponent<Rigidbody2D>().gravityScale = 0;
	}
}
