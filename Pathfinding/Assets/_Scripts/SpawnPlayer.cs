using UnityEngine;
using System.Collections;

public class SpawnPlayer : SpawnShips {
	public Texture2D lifeIcon;

	// Use this for initialization
	void Start () {
		maxActive = 1;
	}

	void OnGUI()
	{
		int numLife = (maxGenerated-numGenerated+numActive);
//		GUILayout.BeginArea(new Rect(20,Screen.height-lifeIcon.height-20,20+(lifeIcon.width+5)*numLife,Screen.height-20));
		Rect location = new Rect(20,Screen.height-lifeIcon.height-20,lifeIcon.width,lifeIcon.height);
		for(int i=0;i<numLife;i++){
			GUI.DrawTexture(location,lifeIcon);
			location.x += lifeIcon.width + 5;
		}
//		GUILayout.EndArea();
	}

	[RPC]
	protected override GameObject spawn (Transform location)
	{
		GameObject newShip = base.spawn (location);
		if(networkView.isMine){
			if(newShip){
				if(Camera.main)
					Camera.main.enabled = false;
				Transform cam = transform.Find("Camera");
				if(cam){
					cam.camera.GetComponent<SmoothFollowCSharp>().target = newShip.transform;
					cam.camera.enabled = true;
				}
				return newShip;
			}
		}
		return null;
	}
}
