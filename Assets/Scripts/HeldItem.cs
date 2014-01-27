using UnityEngine;
using System.Collections;

public class HeldItem : MonoBehaviour {
	private bool held;
	public bool canPickUp = true;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void unHold()
	{
		held = false;
	}

	public bool isHeld()
	{
		return held;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("guy") && canPickUp)
		{
			held = true;
			(col.gameObject.GetComponent(typeof(ItemUser)) as ItemUser).HoldItem(this as HeldItem);
		}
	}
}
