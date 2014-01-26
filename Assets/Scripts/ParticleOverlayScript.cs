using UnityEngine;
using System.Collections;


public class ParticleOverlayScript : MonoBehaviour {
	
	void Start ()
	{
		// Set the sorting layer of the particle system.
		particleSystem.renderer.sortingLayerName = "foreground";
		particleSystem.renderer.sortingOrder = 2;
		print ("particle");
	}
}