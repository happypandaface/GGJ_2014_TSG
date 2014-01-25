using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		//Play button
		if (GUI.Button (new Rect (Screen.width/2,Screen.height/2,150,100), "Play")) {
			Application.LoadLevel("firstScene2");
		}
	}
}
