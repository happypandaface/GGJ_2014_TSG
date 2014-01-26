using UnityEngine;
using System.Collections;

public class Telepuerto : MonoBehaviour {
	public string scene;
	public Transform fadeOut;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (scene != null && col.collider.CompareTag("guy"))
		{
			fade f = ((Transform)Instantiate(fadeOut, Vector3.zero, Quaternion.identity)).GetComponent<fade>();
			f.nextLevel = scene;
			f.start = 0;
			f.end = 1;
			f.speed = .25f;
			//Application.LoadLevel(scene);
		}
	}
}
