using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

	public GameObject creditTextSprite;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		//Play button
		if (GUI.Button (new Rect (Screen.width/2-75,Screen.height/2-120,100,50), "Play")) {
			Application.LoadLevel("FlowerScene");
		}

		//Creditz button
		if (GUI.Button (new Rect (Screen.width/2-75,Screen.height/2-50,100,50), "Credits")) {
			PlayCredits();
		}
	}

	void PlayCredits()
	{
		
	}
}
