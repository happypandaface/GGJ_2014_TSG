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
		if (!col.GetComponent<Guy>().isGrounded)
		{
			print ("not grounded");
		}
		else
		{
			print ("grounded");

			col.GetComponent<Guy>().hasFlower = true;

			Destroy(gameObject);
		}
	}
}

