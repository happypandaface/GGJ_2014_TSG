using UnityEngine;
using System.Collections;

public class GravBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Trigger>().IsActive())
			GetComponent<Rigidbody2D>().gravityScale = 1;
		else
			GetComponent<Rigidbody2D>().gravityScale = 0;
	}
}
