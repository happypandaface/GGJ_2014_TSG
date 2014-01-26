using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {
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
	private float textCount = 7;
	// Use this for initialization
	void Start () {
		string s = ((GameObject)GameObject.FindGameObjectWithTag("guy")).GetComponent<Guy>().getText(nextLevel);
		print (s);
		enlighteningText = s;
		alpha = start;
		renderer.material.color = new Color(1, 1, 1, alpha);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
			textCount = 0;
		transform.position = new Vector3(0, 0, -8.1f);
		if (nextLevel != null && !nextLevel.Equals("") && Mathf.Abs (end-renderer.material.color.a) < Time.deltaTime*speed)
		{
			if (enlighteningText != "" && textCount > 0)
			{
				textCount -= Time.deltaTime;
			}else
				Application.LoadLevel(nextLevel);
		}
		if (end < renderer.material.color.a)
			alpha -= Time.deltaTime*speed;
		else
			alpha += Time.deltaTime*speed;
		renderer.sharedMaterial.color = new Color(1, 1, 1, alpha);
	}
	
	void OnGUI ()
	{
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		GUI.color = new Color(1, 1, 1, textAlpha);
		centeredStyle.font = fontOfTheBhuddah;
		centeredStyle.fontSize = 17;
		textAlpha += Time.deltaTime;
		centeredStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label (new Rect (Screen.width/2-100, Screen.height/2-75, 200, 150), enlighteningText, centeredStyle);
	}
}
