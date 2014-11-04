using UnityEngine;
using System.Collections;

public class SpawnPlayer : SpawnShips {

	// Use this for initialization
	void Start () {
		maxActive = 1;
	}

	protected override GameObject spawn ()
	{
		GameObject newShip = base.spawn ();
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
		return null;
	}
}
