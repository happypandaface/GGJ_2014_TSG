using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	public AudioClip crushAudClip;
	private AudioSource crushAudSource;

	public AudioClip pickupAudClip;
	private AudioSource pickupAudSource; 

	public GameObject pickupParticle;

	private bool isCrushed;	
	private bool isPickedUp;

	// Use this for initialization
	void Start ()
	{
		//DontDestroyOnLoad(transform.gameObject);
		crushAudSource = AddAudio(crushAudClip, false, 0.5f);
		pickupAudSource = AddAudio(pickupAudClip, false, 1f);
	}

	// Update is called once per frame
	void Update ()
	{

	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (!col.GetComponent<Guy>().checkGrounded())
		{
			if ((col.rigidbody2D.velocity.y < 0) && !col.GetComponent<Guy>().isGrounded)
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
			if ((!isCrushed) && !isPickedUp )
			{
				isPickedUp = true; 

				col.GetComponent<Guy>().hasFlower = true;

				pickupAudSource.Play(); 

				transform.GetComponent<SpriteRenderer>().enabled = false;

				GameObject particle = Instantiate(pickupParticle, new Vector3(transform.position.x, -transform.position.y, -6),  Quaternion.Euler(0,0,0)) as GameObject;
				print (particle);
				//delay so sound will play
				Destroy(gameObject, 2);
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

