using UnityEngine;
using System.Collections;

public class Bunny : Dies {
	public Transform guy;
	public bool isLeader;
	private float jumpCount;
	private bool leaving;

	public AudioClip throwAudClip;
	private AudioSource throwAudSource; 
	// Use this for initialization
	void Start () {
		throwAudSource = AddAudio(throwAudClip, false, 1f);

	}
	
	// Update is called once per frame
	void Update () {
		if (leaving)
		{
			rigidbody2D.velocity = new Vector2(2, 0);
		}
		if (!GetComponent<HeldItem>().isHeld() && GetComponent<Box>().CheckGrounded())
		{
			jumpCount += Time.deltaTime;
			if (jumpCount > 1)
			{
				jumpCount = 0;
				rigidbody2D.velocity = new Vector2(0, 5);
			}
		}
	}

	public void PlayThrowSound()
	{
		throwAudSource.Play ();
	}

	public override void Die()
	{
		guy.gameObject.GetComponent<Guy>().modKarma(-1);
		base.Die();
	}

	AudioSource AddAudio(AudioClip clip, bool loop, float vol)
	{
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = false;
		newAudio.volume = vol;
		
		return newAudio;
	}

	public void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.GetComponent<Bunny>() != null)
		{
			GameObject.FindGameObjectWithTag("guy").GetComponent<Guy>().modKarma(1);
			Bunny b = col.collider.GetComponent<Bunny>();
			if (isLeader)
			{
				GetComponent<HeldItem>().canPickUp = false;
				leaving = true;
				Destroy (b.gameObject);
			}
		}
	}
}
