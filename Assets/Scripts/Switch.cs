﻿using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public Trigger trigger;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D()
	{
		trigger.toggle();
	}
}
