using UnityEngine;
using System.Collections;

public class FatMan : MonoBehaviour {
	
	public Sprite spr;
	public Transform fatmanDead;
	public Transform dListener;

	private Sprite lastSpr;
	private float waveHands = 0;
	private bool waving;

	// Use this for initialization
	void Start ()
	{
		lastSpr = GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		waveHands += Time.deltaTime;
		if (waveHands > .7f)
		{
			if (!waving)
			{
				GetComponent<SpriteRenderer>().sprite = spr;
			}
			if (waveHands > 1f)
			{
				GetComponent<SpriteRenderer>().sprite = lastSpr;
				waveHands = 0f;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("floor"))
		{
			(dListener.GetComponent(typeof(DeathListener)) as DeathListener).thingKilled(this.gameObject);
			Instantiate (fatmanDead, transform.position+new Vector3(-1, -1.3f, 0), Quaternion.Euler(0, 0, 20));
			Destroy (this.gameObject);
		}
	}
}
