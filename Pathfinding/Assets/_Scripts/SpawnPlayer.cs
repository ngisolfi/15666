using UnityEngine;
using System.Collections;

public class SpawnPlayer : SpawnShips {
	public Texture2D lifeIcon;

	// Use this for initialization
	void Start () 
	{
	}

	/*
	void OnGUI()
	{
		int numLife = (maxGenerated-numGenerated+numActive);
		Rect location = new Rect(20,Screen.height-lifeIcon.height-20,lifeIcon.width,lifeIcon.height);
		for(int i=0;i<numLife;i++){
			GUI.DrawTexture(location,lifeIcon);
			location.x += lifeIcon.width + 5;
		}
	}
	*/
}
