using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : Dies, ItemUser {
	public bool isGrounded;	
	public bool hasFlower;
	public List<string> itemKeys = new List<string>();
	public List<UsedItem> itemTrans = new List<UsedItem>();
	public List<string> audioKeys = new List<string>();
	public List<AudioClip> audioClips = new List<AudioClip>();
	public UsedItem weapon;
	public Transform fadeOut;
	public Sprite hungryHungryGhost;
	public Sprite fightingFightingGod;

	private Levitate levitationScript;

	private float maxSpeed = 4;
	private List<string> itemsHeld = new List<string>();
	private Transform currentItem;
	private Dictionary<string, UsedItem> itemsDict;
	private Dictionary<string, AudioSource> audioDict;
	private HeldItem heldItem;
	private bool facingLeft;
	
	static int stuff = 0;
	static int karma = 0;
	static bool reincarnating;
	static int currentDeathQuote = 0;

	//Footstep stuff
	public AudioClip footStepAudClip;
	public float footStepPitchRand = 0.1f;
	private AudioSource footStepAudSource;
	public float footStepFrequency = 0.1f;
	private float footStepTimer;
	private bool isWalking;
	private bool inAir;
	private bool isGhost;
	private bool isGod;
	private bool isImageFacingLeft;
	private bool frozen;
	private float jumpForce = 17;

	private string[] deathStuff = {
		"We travel the six worlds, weary seekers on the path. What lies ahead, lies behind.",
		"The whole body is free of dust, who could believe in a means to brush it free.  Why go wandering on roads in dusty realms only to forsake the seat in your very home.",
		"The great teacher holds up a flower. In stillness you smile and a 1000 years of suffering fall away."
	};
	private string[] humanStuff = {
		"Move Forth with an Open hand, nothing you hold will set you free.",
		"Only that which we destroy can hope to harm us.",
		"Saving all sentient beings, we endlessly circle the six realms.",
		"When you fear nothing, not even death can touch you."
	};
	private string[] godStuff = {
		"With the world's power in my hand, I want for nothing.",
		"The level ground is filled with bodies, yet on the slope no one remains.",
		"Puny beings bow before me or suffer my mighty wrath.",
		"I hold dominion over all. Everything shrinks before me. Except the specter of death."
	};
	private string[] ghostStuff = {
		"I am lost in the world untethered. What I see means nothing but empty hands holds everything.",
		"I thirst for thirst, I long for hunger, the only thing worse than death, is this never ending life.",
		"Entering empty rooms hearing only the echoes of my long lost steps. I can trust nothing but this very breath.",
		"Stepping forth into an endless night, the day comes forth to fill these weary eyes."
	};
	private string[] roomLink = {"FlowerScene", "puzzle1", "rabbitLevel", "FatmanCaveJ"};

	int groundedCount = 0;

	// Use this for initialization
	void Start () {
		//reincarnating = true;
		//karma = 1;
		if (reincarnating)
		{
			print (karma);
			if (karma < 0)
			{
				GetComponent<SpriteRenderer>().sprite = hungryHungryGhost;
				jumpForce = 7;
				karma = -1;
				isGhost = true;
				rigidbody2D.isKinematic = true;
				rigidbody2D.drag = 4;
			}else
			if (karma > 0)
			{
				GetComponent<SpriteRenderer>().sprite = fightingFightingGod;
				jumpForce = 30;
				karma = 1;
				isGod = true;
				//rigidbody2D.isKinematic = true;
				//rigidbody2D.drag = 4;
			}
		}
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
		if (transform.position.y < -30)
			Die ();
		if (heldItem != null)
		{
			heldItem.transform.position = new Vector3(transform.position.x, transform.position.y+1.4f, transform.position.z);
		}
		isWalking = Mathf.Abs(rigidbody2D.velocity.x) < 1 ? false : true;
		if (!checkGrounded())
		{
			rigidbody2D.drag = 2;
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

			if (isGhost)
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

			if (levitationScript != null)
				levitationScript.checkLevatation();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			facingLeft = true;
			rigidbody2D.AddForce(new Vector2(-70, 0));
			if (rigidbody2D.velocity.x < -maxSpeed)
				rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
			
			if (isGhost)
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

			if (levitationScript != null)
				levitationScript.checkLevatation();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (heldItem != null)
			{
				if (heldItem.GetComponent<Bunny>() != null)
				{
					heldItem.GetComponent<Bunny>().PlayThrowSound();
				}
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
		if ((checkGrounded() || isGhost) && Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
			if (karma != -1)
				PlaySound("jump");

			levitationScript.checkLevatation();
		}
		if (Input.GetKey(KeyCode.DownArrow) && isGhost)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -jumpForce);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		//Flip direction
		if ((Input.GetKeyDown(KeyCode.LeftArrow)))
		{

			print (facingLeft);
			if (!isImageFacingLeft)
			{
				isImageFacingLeft = !isImageFacingLeft;
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
			}
		}
		if ((Input.GetKeyDown(KeyCode.RightArrow)))
		{

			print (!isImageFacingLeft);
			if (isImageFacingLeft)
			{
				isImageFacingLeft = !isImageFacingLeft;
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
			}
		}
		if (frozen == true)
		{
			rigidbody2D.velocity = Vector2.zero;
		}
		if (isGhost)
		{
			rigidbody2D.velocity *= .9f;
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
		return new Vector2(transform.position.x-GetComponent<BoxCollider2D>().size.x/2*transform.localScale.x, transform.position.y-GetComponent<BoxCollider2D>().size.y/2f*transform.localScale.y);
	}
	
	public Vector2 getPositionRight()
	{

		return new Vector2(transform.position.x+GetComponent<BoxCollider2D>().size.x/2f*transform.localScale.x, transform.position.y-GetComponent<BoxCollider2D>().size.y/2f*transform.localScale.y);
	}

	public Vector2 getSize()
	{
		return new Vector2(GetComponent<BoxCollider2D>().size.x*transform.localScale.x, GetComponent<BoxCollider2D>().size.y*transform.localScale.y);
	}

	public bool checkGrounded()
	{
		RaycastHit2D[] rhsRight = Physics2D.RaycastAll(getPositionRight(), -Vector2.up, .2f);
		foreach (RaycastHit2D rh in rhsRight)
		{
			if (rh.collider.CompareTag("floor"))
				return groundedCount > 0;
		}
		RaycastHit2D[] rhsLeft = Physics2D.RaycastAll(getPositionLeft(), -Vector2.up, .2f);
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

	public void modKarma(int i)
	{
		karma += i;
	}

	public string getText(string nextLevel)
	{
		int num = -1;
		for (int i = 0; i < 4; ++i)
		{
			if (roomLink[i] == Application.loadedLevelName)
			{
				num = i;
			}
		}
		if (num == -1)
		{
			return deathStuff[currentDeathQuote];
			currentDeathQuote++;
		}
		if (isGhost)
			return ghostStuff[num];
		else if (isGod)
			return godStuff[num];
		else
			return humanStuff[num];
	}

	public override void Die()
	{
		reincarnating = true;
		fade f = ((Transform)Instantiate (fadeOut, Vector3.zero, Quaternion.identity)).GetComponent<fade>();
		f.start = 0;
		f.end = 1;
		f.nextLevel = "FlowerScene";
		base.Die ();
	}

	public void reBirth()
	{
		reincarnating = true;
	}

	public void Freeze()
	{
		frozen = true;
	}
}
