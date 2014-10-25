using UnityEngine;
using System.Collections;

public class color : MonoBehaviour {

	public Color c = new Color (1.0f, 1.0f, 0.0f, 1.0f);
	// Use this for initialization
	void Start () {

		renderer.material.color = c;
	//	gameObject.renderer.material.color = Color (1.0f, 1.0f, 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
