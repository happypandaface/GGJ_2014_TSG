using UnityEngine;
using System.Collections;

public class UsedItem : MonoBehaviour {

	private ItemUser user;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = user.GetPosition();
	}

	public void SetItemUser(ItemUser iu)
	{
		user = iu;
	}
}
