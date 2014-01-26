using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour, ItemUser {
	public bool isGrounded;	
	public bool hasFlower;
	public List<string> itemKeys = new List<string>();
	public List<UsedItem> itemTrans = new List<UsedItem>();
	public List<string> audioKeys = new List<string>();
	public List<AudioClip> audioClips = new List<AudioClip>();
	public UsedItem weapon;

	private Levitate levitationScript;

	private float maxSpeed = 4;
	private List<string> itemsHeld = new List<string>();
	private Transform currentItem;
	private Dictionary<string, UsedItem> itemsDict;
	private Dictionary<string, AudioSource> audioDict;
	private HeldItem heldItem;
	private bool facingLeft;
	
	static int stuff = 0;

	//Footstep stuff
	public AudioClip footStepAudClip;
	public float footStepPitchRand = 0.1f;
	private AudioSource footStepAudSource;
	public float footStepFrequency = 0.1f;
	private float footStepTimer;
	private bool isWalking;
	private bool inAir;

	int groundedCount = 0;

	// Use this for initialization
	void Start () {
		stuff++;
		print(stuff);
		itemsDict = new Dictionary<string, UsedItem>();

		//Get levitate script for reference
		levitationScript = GetComponent<Levitate>() as Levitate;

		//Set footstep timer
		footStepTimer = footStepFrequency;
		//Set up audio
		footStepAudSource = AddAudio(footStepAudClip, false, 0.5f);

		itemsDict = new Dictionary<string, UsedItem>();
		for (int i = 0; i < itemKeys.Count && i < itemTrans.Count; ++i)
		{
			itemsDict.Add (itemKeys[i], itemTrans[i]);
		}
		audioDict = new Dictionary<string, AudioSource>();
		for (int i = 0; i < audioKeys.Count && i < audioClips.Count; ++i)
		{
			audioDict.Add (audioKeys[i], AddAudio(audioClips[i], false, 1f));
		}

	}

	void Update ()
	{
		if (heldItem != null)
		{
			heldItem.transform.position = new Vector3(transform.position.x, transform.position.y+1.4f, transform.position.z);
		}
		isWalking = Mathf.Abs(rigidbody2D.velocity.x) < 1 ? false : true;
		if (!checkGrounded())
		{
			rigidbody2D.drag = 0;
		}else
		{
			rigidbody2D.drag = 10;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			facingLeft = false;
			rigidbody2D.AddForce(new Vector2(70, 0));
			if (rigidbody2D.velocity.x > maxSpeed)
				rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);

			if (checkGrounded())
			{
				if (isWalking)
				{
					UpdateFootstep();
				}
				else
				{
					footStepAudSource.Play();
				}
			}

			levitationScript.checkLevatation();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			facingLeft = true;
			rigidbody2D.AddForce(new Vector2(-70, 0));
			if (rigidbody2D.velocity.x < -maxSpeed)
				rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);

			if (checkGrounded())
			{
				if (isWalking)
				{
					UpdateFootstep();
				}
				else
				{
					footStepAudSource.Play();
				}
			}

			levitationScript.checkLevatation();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (heldItem != null)
			{
				float horizOffset = 1.8f;
				if (facingLeft)
					horizOffset *= -1;
				heldItem.rigidbody2D.velocity = new Vector2();
				heldItem.transform.position = new Vector3(transform.position.x+horizOffset, transform.position.y, transform.position.z);
				heldItem.unHold ();
				heldItem = null;
			}else
			if (HasItem("weapon"))
			{
				if (heldItem != null)
				{
					float horizOffset = 0;
					if (facingLeft)
						horizOffset = -1.4f;
					else
						horizOffset = 1.4f;
					heldItem.transform.position = new Vector3(transform.position.x+horizOffset, transform.position.y, transform.position.z);
					heldItem = null;
				}else
				{
					UsedItem item = itemsDict["weapon"];
					//itemsDict.TryGetValue("weapon", out item);
					UsedItem usedItem = (UsedItem)Instantiate(item, transform.position, Quaternion.identity);
					usedItem.SetItemUser(this);
				}
			}
		}
		if (checkGrounded() && Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 14);
			PlaySound("jump");

			levitationScript.checkLevatation();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
		{
			groundedCount++;
			if (inAir && checkGrounded())
			{
				inAir = false;
				PlaySound ("land");
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
		{
			groundedCount--;
		}
		if (!checkGrounded())
			inAir = true;
	}

	public Vector2 getPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}
	
	public Vector2 getPositionLeft()
	{
		return new Vector2(GetComponent<SpriteRenderer>().bounds.min.x, GetComponent<SpriteRenderer>().bounds.min.y);
	}
	
	public Vector2 getPositionRight()
	{

		return new Vector2(GetComponent<SpriteRenderer>().bounds.max.x, GetComponent<SpriteRenderer>().bounds.min.y);
	}

	public bool checkGrounded()
	{
		RaycastHit2D[] rhsRight = Physics2D.RaycastAll(getPositionRight(), -Vector2.up, .4f);
		foreach (RaycastHit2D rh in rhsRight)
		{
			if (rh.collider.CompareTag("floor"))
				return groundedCount > 0;
		}
		RaycastHit2D[] rhsLeft = Physics2D.RaycastAll(getPositionLeft(), -Vector2.up, .4f);
		foreach (RaycastHit2D rh in rhsLeft)
		{
			if (rh.collider.CompareTag("floor"))
				return groundedCount > 0;
		}
		return false;
	}

	public void AddItem(string type)
	{
		if (!HasItem(type))
			itemsHeld.Add(type);
	}
	
	public bool HasItem(string type)
	{
		return itemsHeld.Find(itemStr => itemStr.Equals(type)) != null;
	}
	
	public void HoldItem(HeldItem hi)
	{
		heldItem = hi;
	}

	public Vector3 GetPosition()
	{
		return new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
	}
	
	public void FinishedUsing()
	{
		//print ("done");
	}

	void UpdateFootstep()
	{

		if (footStepTimer <= 0)
		{
			footStepAudSource.pitch = Random.Range(1.0f - footStepPitchRand, 1.0f + footStepPitchRand);
			footStepAudSource.Play();
			footStepTimer = footStepFrequency;
		}

		footStepTimer -= Time.deltaTime;
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
	
	public void PlaySound(string s)
	{
		AudioSource audioToPlay = audioDict[s];
		audioToPlay.Play ();
	}
}
