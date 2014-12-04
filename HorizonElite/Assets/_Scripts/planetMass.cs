using UnityEngine;
using System.Collections;

public class planetMass : MonoBehaviour {

	public float density = 1000.0f;
	
	[HideInInspector]
	public float mass;
	[HideInInspector]
	public float volume;
	
	void Start() {
		float r = gameObject.transform.lossyScale.x;
		volume = 4.0f / 3.0f * Mathf.PI * r * r * r;
		mass = density * volume;
	}
	
}
