using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public Trigger trigger;
	private bool triggered;
	public AudioClip aClip;
	public Sprite switchedOn;
	private AudioSource aSource;


	// Use this for initialization
	void Start () {
		aSource = gameObject.AddComponent<AudioSource>();
		aSource.clip = aClip;
		aSource.loop = false;
		aSource.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("guy"))
		{
			if (!triggered)
			{
				GetComponent<SpriteRenderer>().sprite = switchedOn;
				aSource.Play();
				trigger.toggle();
			}
			triggered = true;
		}
	}
}
