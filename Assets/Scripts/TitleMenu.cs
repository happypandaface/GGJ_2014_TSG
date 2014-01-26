using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

	public GameObject creditTextSprite;
	public float moveSpeed = 0.1f;
	public float creditsEndYPos = 0;
	public Texture playTexture;
	public Texture creditsTexture;


	private bool bPlayCredits;
	private Vector3 creditsStartPos;

	// Use this for initialization
	void Start () {
		creditsStartPos = creditTextSprite.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (bPlayCredits)
		{
			print (creditTextSprite.transform.position.y);
			if (creditTextSprite.transform.position.y > creditsEndYPos)
			{
				creditTextSprite.transform.Translate(0, -moveSpeed, 0);
			}
		}

		if (Input.anyKey)
		{
			if (bPlayCredits)
			{
				Reset();
			}
		}
	}

	void OnGUI () {
		if (!bPlayCredits)
		{
			//Play button
			if (GUI.Button (new Rect (Screen.width/2-75,Screen.height/2,100,50), playTexture, "Label")) {
				Application.LoadLevel("FlowerScene");
			}

			//Creditz button
			if (GUI.Button (new Rect (Screen.width/2-400,Screen.height/2 + 200,100,50), creditsTexture, "label")) {
				PlayCredits();
			}
		}
	}

	void PlayCredits()
	{
		bPlayCredits = true;
	}

	void Reset()
	{
		bPlayCredits = false;
		print (creditsStartPos);
		creditTextSprite.transform.position = new Vector3(creditsStartPos.x, creditsStartPos.y, 0);

	}
}
