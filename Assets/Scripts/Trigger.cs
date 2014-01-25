using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	private bool active;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool toggle()
	{
		active = !active;
		return IsActive();
	}

	public bool IsActive()
	{
		return active;
	}
}
