using UnityEngine;
using System.Collections;

public class Telepuerto : MonoBehaviour {
	public string scene;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (scene != null && col.collider.CompareTag("guy"))
			Application.LoadLevel(scene);
	}
}
