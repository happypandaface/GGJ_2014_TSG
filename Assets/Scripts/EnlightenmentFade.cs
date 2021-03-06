using UnityEngine;
using System.Collections;

public class EnlightenmentFade : MonoBehaviour {
	public float start;
	public float end;
	public float speed;
	public string nextLevel;
	public string enlighteningText;
	public Font fontOfTheBhuddah;
	private float fontTime = 3;
	private float currFontTime = 3;
	private float alpha = 0;
	private Color color = Color.white;
	private float textAlpha = 0;
	// Use this for initialization
	void Start () {
		alpha = start;
		renderer.material.color = new Color(1, 1, 1, alpha);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(0, 0, -8.1f);
		if (nextLevel != null && !nextLevel.Equals("") && Mathf.Abs (end-renderer.material.color.a) < Time.deltaTime*speed)
			Application.LoadLevel(nextLevel);
		if (end < renderer.material.color.a)
			alpha -= Time.deltaTime*speed;
		else
			alpha += Time.deltaTime*speed;
		renderer.sharedMaterial.color = new Color(1, 1, 1, alpha);

		if (alpha >= 1)
		{
			print ("go to title menu");
			Application.LoadLevel("TitleMenu");
		}
	}
	
	void OnGUI ()
	{
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		GUI.color = new Color(1, 1, 1, textAlpha);
		textAlpha += Time.deltaTime;
		centeredStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "", centeredStyle);
	}
}