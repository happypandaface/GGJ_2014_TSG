using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
	public float moveSpeed = 0.1f;

	private float sinStuff = 0;
	private float waterLevel = 0;

	private float startY;

	// Use this for initialization
	void Start ()
	{
		startY = transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		float f = 8;
		sinStuff += Time.deltaTime;
		waterLevel += Time.deltaTime;
		//print (""+Mathf.Cos (sinStuff)*10);
		transform.position = new Vector3(0, waterLevel*.03f*f+Mathf.Sin (sinStuff*1.4f)*.04f*f+startY, 0);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "guy")
		{
			print (col.transform.position.y - transform.position.y);
		}

	}
}

