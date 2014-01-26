using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Vector2 minVals;
	public Vector2 maxVals;
	public float speed;
	public bool goingToMax;
	public AudioClip growl;
	private AudioSource growlSource;

	private bool alive = true;

	// Use this for initialization
	void Start () {
		growlSource = gameObject.AddComponent<AudioSource>();
		growlSource.clip = growl;
		growlSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive)
		{
			Vector2 dest;
			if (goingToMax)
				dest = minVals;
			else
				dest = maxVals;
			Vector2 curr = new Vector2(transform.position.x, transform.position.y);
			Vector2 diff = dest-curr;
			float speedThisFrame = Time.deltaTime*speed;
			if (diff.magnitude < speedThisFrame)
			{
				goingToMax = !goingToMax;
			}else
			{
				//rigidbody2D.velocity = diff.normalized*speedThisFrame;
				diff = diff.normalized*speedThisFrame;
				curr += diff;
				transform.position = new Vector3(curr.x, curr.y, 0);
				/*
					rigidbody2D.AddForce(diff.normalized*speedThisFrame);
					if (rigidbody2D.velocity.x > maxSpeed)
						rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
					if (rigidbody2D.velocity.x < -maxSpeed)
						rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
					if (rigidbody2D.velocity.y > maxSpeed)
						rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxSpeed);
					if (rigidbody2D.velocity.y < -maxSpeed)
						rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -maxSpeed);
					*/
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("guy"))
		{
			if (col.collider.transform.position.y > renderer.bounds.max.y)
			{
				//col.gameObject.GetComponent<Guy>().modKarma(-1);
			}else
			{
				if (alive)
				{
					growlSource.Play ();
					col.gameObject.GetComponent<Dies>().Die();
				}
			}
		}
		if (col.rigidbody.velocity.y < 0 && col.collider.transform.position.y > renderer.bounds.max.y)
		{
			GameObject.FindGameObjectWithTag("guy").GetComponent<Guy>().modKarma(-1);
			alive = false;
			rigidbody2D.drag = 2;
			rigidbody2D.isKinematic = false;
			rigidbody2D.isKinematic = false;
		}
	}
}
