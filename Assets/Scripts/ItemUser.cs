using UnityEngine;
using System.Collections;

public interface ItemUser
{
	bool HasItem(string s);
	void AddItem(string s);
	Vector3 GetPosition();
	void FinishedUsing();
}
