using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string itemType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("guy"))
		{
			(col.gameObject.GetComponent(typeof(ItemUser)) as ItemUser).AddItem(itemType);
		}
	}
}
