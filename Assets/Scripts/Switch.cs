using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public Trigger trigger;
	private bool triggered;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("guy"))
		{
			if (!triggered)
				trigger.toggle();
			triggered = true;
		}
	}
}
