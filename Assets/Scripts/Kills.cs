using UnityEngine;
using System.Collections;

public class Kills : MonoBehaviour {
	public AudioClip deathSound;
	public AudioSource deathSource;
	// Use this for initialization
	void Start ()
	{
		deathSource = gameObject.AddComponent<AudioSource>();
		deathSource.clip = deathSound;
		deathSource.loop = false;
		deathSource.playOnAwake = false;
		deathSource.volume = 1f;
		deathSource.Play();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Die()
	{
		print ("death sound");
		deathSource.Play();
	}
}
