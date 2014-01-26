using UnityEngine;
using System.Collections;

public class Dies : MonoBehaviour {
	public Transform spatterTrans;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Die()
	{
		for (int i = 0; i < 10; ++i)
		{
			Transform spatter = (Transform)Instantiate(spatterTrans, transform.position, Quaternion.Euler(0, 0, Random.value*Mathf.PI*2));
			spatter.rigidbody2D.velocity = Random.insideUnitCircle*5;
			spatter.rigidbody2D.angularVelocity = Random.value;
		}
	}
}
