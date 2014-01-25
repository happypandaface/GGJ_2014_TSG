using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour, ItemUser {
	public bool isGrounded;	
	public bool hasFlower;
	public List<string> itemKeys = new List<string>();
	public List<UseItem> itemTrans = new List<UseItem>();
	public UseItem weapon;

	//In seconds
	public float zenTime = 10f;
	public float zenHeight = 10f;
	private float zenTimer;
	private float zenMoveSpeed = 0.01f;
	private float levatate = 0f;
	private bool doneRising = false;
	private bool isLevatating = false;

	private float maxSpeed = 4;
	private List<string> itemsHeld = new List<string>();
	private Transform currentItem;
	private Dictionary<string, UseItem> itemsDict;

	int groundedCount = 0;

	// Use this for initialization
	void Start () {

		zenTimer = zenTime;

		itemsDict = new Dictionary<string, UseItem>();
		for (int i = 0; i < itemKeys.Count && i < itemTrans.Count; ++i)
		{
			itemsDict.Add (itemKeys[i], itemTrans[i]);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody2D.AddForce(new Vector2(70, 0));
			if (rigidbody2D.velocity.x > maxSpeed)
				rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);

			checkLevatation();
		}else if (Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody2D.AddForce(new Vector2(-70, 0));
			if (rigidbody2D.velocity.x < -maxSpeed)
				rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);

			checkLevatation();
		}
		if (HasItem("weapon") && Input.GetKeyDown(KeyCode.Space))
		{
			UseItem item = itemsDict["weapon"];
			//itemsDict.TryGetValue("weapon", out item);
			UseItem usedItem = (UseItem)Instantiate(item, transform.position, Quaternion.identity);
			usedItem.SetItemUser(this);
		}
		if (checkGrounded() && Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 11);
			print ("sdf");
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 8);

			checkLevatation();
		}

		//Enlightenment timer count down
		zenTimer -= Time.deltaTime;

		print (zenTimer);
		if (zenTimer < 0)
		{

			//remove gravity
			rigidbody2D.gravityScale = 0;

			if ((transform.position.y < zenHeight) && !doneRising)
			{
				transform.Translate(0, zenMoveSpeed,0);
			}
			else
			{
				isLevatating = true;
				doneRising = true;
				levatate += Time.deltaTime;
				transform.position = new Vector2(transform.position.x,Mathf.Sin(levatate) + zenHeight);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
			groundedCount++;
	}
	
	void OnCollisionExit2D(Collision2D col2d)
	{
		if (col2d.gameObject.CompareTag("floor"))
			groundedCount--;
	}

	public Vector2 getPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	public bool checkGrounded()
	{
		RaycastHit2D[] rhs = Physics2D.RaycastAll(getPosition(), -Vector2.up, .6f);
		foreach (RaycastHit2D rh in rhs)
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

	public Vector3 GetPosition()
	{
		return new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
	}
	
	public void FinishedUsing()
	{
		print ("done");
	}

	//Stop levatation and resets timer when player uses input
	void checkLevatation()
	{
		//Reset zenTimer
		zenTimer = zenTime;

		if (isLevatating)
		{
			rigidbody2D.gravityScale = 1;
			isLevatating = false;
			doneRising = false;
		}
	}
}
