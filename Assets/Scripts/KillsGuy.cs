using UnityEngine;
using System.Collections;

public class KillsGuy : MonoBehaviour {
	public AudioClip deathSound;
	// Use this for initialization
	public AudioSource deathSource;
	void Start () {
		deathSource = gameObject.AddComponent<AudioSource>();
		deathSource.clip = deathSound;
		deathSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("guy") || col.collider.CompareTag("animal"))
		{
			if (transform.position.y > col.gameObject.transform.position.y)
			{
				if (col.collider.CompareTag("guy"))
				{
					col.collider.GetComponent<Guy>().modKarma(1);
				}
				deathSource.Play ();
				col.gameObject.GetComponent<Dies>().Die();
			}
		}
	}
}
