using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string itemType;
	public bool canPickUp = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("guy") && canPickUp)
		{
			(col.gameObject.GetComponent(typeof(ItemUser)) as ItemUser).AddItem(itemType);
		}
	}
}
