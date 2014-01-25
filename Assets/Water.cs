using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
	public float moveSpeed = 0.1f;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(0, moveSpeed, 0);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "guy")
		{
			print (col.transform.position.y - transform.position.y);
		}

	}
}

