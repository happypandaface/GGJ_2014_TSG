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

	// Use this for initialization
	void Start ()
	{
		zenTimer = zenTime;

		risingAudSource = AddAudio(risingAudClip, false, 1);
		levitatingAudSource = AddAudio(levitatingAudClip, true, 1);

	}

	// Update is called once per frame
	void Update ()
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
				}
				transform.Translate(0, zenMoveSpeed,0);
			}
			else
			{
				if (!doneRising && !isLevitating)
				{
					EnlightenmentFade f = ((Transform)Instantiate (fadeOut, Vector3.zero, Quaternion.identity)).GetComponent<EnlightenmentFade>();
					f.start = 0;
					f.end = 1;
					f.speed = 0.1f;
				}
				isLevitating = true;
				doneRising = true;
				levitate += Time.deltaTime;
				transform.position = new Vector3(transform.position.x,Mathf.Sin(levitate) + zenHeight, 0);
			}
		}
	}

	//Stop levatation and resets timer when player uses input
	public void checkLevatation()
	{
		//Reset zenTimer
		zenTimer = zenTime;
		
		if (isLevitating || bStarted)
		{

			rigidbody2D.gravityScale = 4;
			isLevitating = false;
			doneRising = false;
			bStarted = false;
			risingAudSource.Stop();
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

