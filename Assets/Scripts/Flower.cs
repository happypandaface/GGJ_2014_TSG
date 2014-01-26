using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	public AudioClip crushAudClip;
	private AudioSource crushAudSource;

	private bool isCrushed;	

	// Use this for initialization
	void Start ()
	{
		crushAudSource = AddAudio(crushAudClip, false, 0.5f);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (!col.GetComponent<Guy>().checkGrounded())
		{
			if (col.rigidbody2D.velocity.y < 0)
			{
				crushAudSource.Play ();
				print ("crush");
				//Crush the flower
				transform.localScale = new Vector3(0.25f,0.05f,0.2f);
				transform.Translate(0, -0.05f, 0);

				//Make is so you can't pick up flower
				isCrushed = true;
			}

		}
		else
		{
			if (!isCrushed)
			{
				col.GetComponent<Guy>().hasFlower = true;

				Destroy(gameObject);
			}
		}
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

