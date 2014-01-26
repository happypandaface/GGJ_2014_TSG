using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour, DeathListener
{
	public float moveSpeed = 0.1f;
	public Transform fadeOut;

	private float sinStuff = 0;
	private float waterLevel = 0;
	private float breath = 0;
	private float startY;
	private bool fading = false;
	private bool fatManKilled;

	// Use this for initialization
	void Start ()
	{
		startY = transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		float f = 8;
		sinStuff += Time.deltaTime;
		waterLevel += Time.deltaTime;
		//print (""+Mathf.Cos (sinStuff)*10);
		transform.position = new Vector3(0, waterLevel*.03f*f+Mathf.Sin (sinStuff*1.4f)*.04f*f+startY, 0);
		//if (transform.position.y < -14)
		//	transform.position = new Vector3(0, -14, 0);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "guy")
		{
			float waterLevel = transform.position.y+GetComponent<BoxCollider2D>().size.y/2f*transform.localScale.y;
			float guyBottomPos = col.GetComponent<Guy>().getPositionLeft().y;
			float guyHeight = col.GetComponent<Guy>().getSize().y;
			if (waterLevel > guyBottomPos+guyHeight)
			{
				breath -= Time.deltaTime;
				if (breath <= 0)
				{
					if (!fading)
					{
						//if (!fatManKilled)
						col.GetComponent<Guy>().modKarma(1);
						col.GetComponent<Guy>().reBirth();

						col.GetComponent<Guy>().Freeze();
						
						fade f = ((Transform)Instantiate(fadeOut, Vector3.zero, Quaternion.identity)).GetComponent<fade>();
						f.nextLevel = "FlowerScene";
						f.start = 0;
						f.end = 1;
						f.speed = .25f;
						fading = true;
					}

					//Application.LoadLevel("FlowerScene");
				}
			}else
				breath = 14;
			//print (col.transform.position.y - transform.position.y);
		}

	}

	public void thingKilled(GameObject go)
	{
		fatManKilled = true;
	}

}

