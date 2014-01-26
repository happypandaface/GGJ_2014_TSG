using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {
	public float start;
	public float end;
	public float speed;
	public string nextLevel;
	private float alpha = 0;
	// Use this for initialization
	void Start () {
		alpha = start;
		renderer.material.color = new Color(1, 1, 1, alpha);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(0, 0, -8);
		if (nextLevel != null && !nextLevel.Equals("") && Mathf.Abs (end-renderer.material.color.a) < Time.deltaTime*speed)
			Application.LoadLevel(nextLevel);
		if (end < renderer.material.color.a)
			alpha -= Time.deltaTime*speed;
		else
			alpha += Time.deltaTime*speed;
		renderer.sharedMaterial.color = new Color(1, 1, 1, alpha);
	}
}
