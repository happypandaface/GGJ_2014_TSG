using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (!col.GetComponent<Guy>().checkGrounded())
		{
			if (col.rigidbody2D.velocity.y < 0)
				;//print("crush");
		}
		else
		{
			//print ("grounded");

			col.GetComponent<Guy>().hasFlower = true;

			Destroy(gameObject);
		}
	}
}

