using UnityEngine;
using System.Collections;

public class KillsGuy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("guy") || col.collider.CompareTag("animal"))
		{
			if (transform.position.y > col.gameObject.transform.position.y+2)
				Destroy(col.gameObject);
		}
	}
}
