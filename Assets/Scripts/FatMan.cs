using UnityEngine;
using System.Collections;

public class FatMan : MonoBehaviour {
	
	public Sprite spr;
	public Transform fatmanDead;

	private Sprite lastSpr;
	private float waveHands = 0;
	private bool waving;

	// Use this for initialization
	void Start () {
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
			Instantiate (fatmanDead, transform.position+new Vector3(-1, -1, 0), Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
