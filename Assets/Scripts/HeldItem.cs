﻿using UnityEngine;
using System.Collections;

public class HeldItem : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("guy"))
		{
			(col.gameObject.GetComponent(typeof(ItemUser)) as ItemUser).HoldItem(this as HeldItem);
		}
	}
}
