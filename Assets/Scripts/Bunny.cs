using UnityEngine;
using System.Collections;

public class Bunny : Dies {
	public Transform guy;
	private float jumpCount;

	public AudioClip throwAudClip;
	private AudioSource throwAudSource; 
	// Use this for initialization
	void Start () {
		throwAudSource = AddAudio(throwAudClip, false, 1f);

	}
	
	// Update is called once per frame
	void Update () {
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
}
