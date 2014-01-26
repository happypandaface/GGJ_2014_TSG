using UnityEngine;
using System.Collections;

public class Levitate : MonoBehaviour
{

	public AudioClip risingAudClip;
	private AudioSource risingAudSource;
	public  AudioClip levitatingAudClip;
	private AudioSource levitatingAudSource;
	private bool bStarted;

	public float zenTime = 10f; //In seconds
	public float zenHeight = 4f;
	private float zenTimer;
	private float zenMoveSpeed = 0.01f;
	private float levitate = 0f;
	private bool doneRising = false;
	private bool isLevitating = false;

	public Transform fadeOut;
	public float fadeOutTime = 5;
	private float fadeOutTimer;
	private bool bCanMove;

	// Use this for initialization
	void Start ()
	{
		zenTimer = zenTime;
		fadeOutTimer = fadeOutTime;

		risingAudSource = AddAudio(risingAudClip, false, 1);
		levitatingAudSource = AddAudio(levitatingAudClip, true, 1);

		bCanMove = true;

	}

	// Update is called once per frame
	void Update ()
	{
		if (!transform.GetComponent<Guy>().messedWithFlower)
		{
			//Enlightenment timer count down
			zenTimer -= Time.deltaTime;

			
			if (zenTimer < 0)
			{
				
				//remove gravity
				rigidbody2D.gravityScale = 0;
				
				if ((transform.position.y < zenHeight) && !doneRising)
				{
					if (!bStarted)
					{
						risingAudSource.Play();
						bStarted = true;
						transform.GetComponent<Guy>().SitDown();
					}
					transform.Translate(0, zenMoveSpeed,0);
				}
				else
				{
					if (!doneRising && !isLevitating)
					{

					}
					fadeOutTimer -= Time.deltaTime;
					
					if (fadeOutTimer < 0)
					{
						bCanMove = false;

						fadeOutTimer = fadeOutTime;
						
						EnlightenmentFade f = ((Transform)Instantiate (fadeOut, Vector3.zero, Quaternion.identity)).GetComponent<EnlightenmentFade>();
						f.start = 0;
						f.end = 1;
						f.speed = 0.1f;

						bCanMove = false;
						transform.GetComponent<Guy>().isEnlightened = true;
					}
					isLevitating = true;
					doneRising = true;
					levitate += Time.deltaTime;
					transform.position = new Vector3(transform.position.x,Mathf.Sin(levitate) + zenHeight, 0);
				}
			}
		}
	}

	//Stop levatation and resets timer when player uses input
	public void checkLevatation()
	{
		//Reset zenTimer
		zenTimer = zenTime;

		print("can move" + bCanMove);
		if ((isLevitating || bStarted) && bCanMove)
		{

			rigidbody2D.gravityScale = 4;
			isLevitating = false;
			doneRising = false;
			bStarted = false;
			risingAudSource.Stop();
			transform.GetComponent<Guy>().Standup();
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

