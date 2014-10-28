using UnityEngine;
using System.Collections;

public class CrosshairDisplay : MonoBehaviour {

	public bool on = true;
	public Texture texture;
	public int height;
	public int width;
	public int xoffset;
	public int yoffset;
	private Rect location;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		location.Set((Screen.width - width) / 2 + xoffset, (Screen.height - height) /2 + yoffset, width, height);
	}
	
	void OnGUI(){
		if(on)
			GUI.DrawTexture(location,texture);
	}
}
