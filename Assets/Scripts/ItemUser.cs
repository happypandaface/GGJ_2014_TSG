using UnityEngine;
using System.Collections;

public interface ItemUser
{
	bool HasItem(string s);
	void AddItem(string s);
	void HoldItem(HeldItem t);
	Vector3 GetPosition();
	void FinishedUsing();
}
